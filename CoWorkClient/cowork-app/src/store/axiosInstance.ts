// axiosInstance.ts
import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse } from 'axios';
import store from '@/store';
import auth from './modules/auth';


class TokenRefresher {
  private isRefreshing = false;
  private pendingRequests: Record<string, Array<{ config: AxiosRequestConfig, resolve: (value: AxiosResponse<any>) => void, reject: (error: any) => void }>> = {};

  async interceptResponseError(error: any): Promise<AxiosResponse> {
    if (error.response?.status === 401) {
      const originalRequest = error.config;
      const baseURL = originalRequest.baseURL || '';

      if (!this.isRefreshing) {
        this.isRefreshing = true;
        try {
          await store.dispatch('getRefreshToken'); // Ovde pozivamo refreshToken akciju
          this.isRefreshing = false;
          const newAccessToken = auth.state.accessToken;
          if(newAccessToken) // Pretpostavimo da accessToken getter vraća novi token
            this.onAccessTokenRefreshed(newAccessToken, baseURL);
        } catch (refreshError) {
          this.isRefreshing = false;
          this.onAccessTokenRefreshFailed(baseURL);
          throw refreshError;
        }
      }

      return new Promise<AxiosResponse>((resolve, reject) => {
        if (!this.pendingRequests[baseURL]) {
          this.pendingRequests[baseURL] = [];
        }
        this.pendingRequests[baseURL].push({ config: originalRequest, resolve, reject });
      });
    }

    return Promise.reject(error);
  }

  async onAccessTokenRefreshed(newAccessToken: string, baseURL: string) {
    const instance = createAxiosInstance(baseURL);

    if (instance) {
      instance.defaults.headers.common['Authorization'] = `Bearer ${newAccessToken}`;
      store.commit('setAccessToken', newAccessToken);
      this.sendPendingRequests(baseURL, newAccessToken);
    }
  }

  onAccessTokenRefreshFailed(baseURL: string) {
    if (this.pendingRequests[baseURL]) {
      this.pendingRequests[baseURL].forEach(request => request.reject(new Error('Failed to refresh access token.')));
      this.pendingRequests[baseURL] = [];
    }
  }

  async sendPendingRequests(baseURL: string, newAccessToken: string) {
    if (this.pendingRequests[baseURL]) {
      this.pendingRequests[baseURL].forEach(request => {
        const newConfig = { ...request.config };
        newConfig.headers = { ...newConfig.headers, Authorization: `Bearer ${newAccessToken}` };
        createAxiosInstance(baseURL)
          .request(newConfig)
          .then((response: AxiosResponse<any>) => request.resolve(response))
          .catch((error: any) => request.reject(error));
      });
      this.pendingRequests[baseURL] = [];
    }
  }
}

const createAxiosInstance = (baseURL: string): AxiosInstance => {
  const instance = axios.create({
    baseURL: baseURL || 'https://api.example.com', // Postavite osnovnu URL adresu API-ja
  });

  const tokenRefresher = new TokenRefresher();

  // Interceptor za odgovor
  instance.interceptors.response.use(
    (response) => response,
    (error) => tokenRefresher.interceptResponseError(error)
  );

  return instance;
};

const axiosInstance = createAxiosInstance('');

export default axiosInstance;

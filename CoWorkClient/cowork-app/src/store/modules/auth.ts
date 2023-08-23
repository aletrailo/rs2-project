import { Module } from 'vuex';
import { RootState } from '../index';

const baseUrl = process.env.NODE_ENV === 'development' ? 'http://localhost:4000/' : '/';
const headers = { "Content-Type": "application/json" }


interface AuthState {
    accessToken: string | null;
    refreshToken: string | null;
}

const authModule: Module<AuthState, RootState> = {
    namespaced: true,
    state: {
        accessToken: null,
        refreshToken: null,
    },
    mutations: {
        setAccessToken(state, token: string) {
            console.log('aaaaaaaaa')
            state.accessToken = token;
        },
        setRefreshToken(state, token: string) {
            state.refreshToken = token;
        },
        initializeTokensFromLocalStorage(state) {
            const accessToken = localStorage.getItem('access_token');
            const refreshToken = localStorage.getItem('refresh_token');
            if (accessToken) {
                state.accessToken = accessToken;
            }
            if (refreshToken) {
                state.refreshToken = refreshToken;
            }
        },

    },
    actions: {
        initializeAuth({ commit }) {
            commit('initializeTokensFromLocalStorage');
        },
        setTokens({ commit }, { accessToken, refreshToken }) {
            commit('setAccessToken', accessToken);
            commit('setRefreshToken', refreshToken);
            localStorage.setItem('access_token', accessToken);
            localStorage.setItem('refresh_token', refreshToken);
        },
        logTokens({ state }) {
            console.log('Access Token:', state.accessToken);
            console.log('Refresh Token:', state.refreshToken);
        },
        async logIn({ commit }, { username, password }) {
            const url = baseUrl + 'api/v1/Authentication/LogIn'
            const loginData = {
                userName: username,
                password: password
            }
            try {
                const response = await fetch(url, { method: 'POST', body: JSON.stringify(loginData), headers: headers });
                if (response.ok) {
                    const data = await response.json();
                    const { accessToken, refreshToken } = data;
                    // Save tokens in the store
                    this.dispatch('auth/setTokens', { accessToken, refreshToken });

                    // Display tokens in console
                    console.log('Access Token:', accessToken);
                    console.log('Refresh Token:', refreshToken);
                } else {
                    console.error('Login failed.');
                }
            } catch (error) {
                console.error('An error occurred during login:', error);
            }
        },
        async singIn({ commit }, { firstName, lastName, userNeme, password, email, phoneNumber }) {
            const url = baseUrl + 'api/v1/Authentication/RegisterUser'
            const singInData =
            {
              firstName: firstName,
              lastName: lastName,
              userNeme: userNeme,
              password: password,
              email: email,
              phoneNumber: phoneNumber
            }
            try {
                const response = await fetch(url, {method: 'POST',body: JSON.stringify(singInData),headers: headers});

                if (response.ok) {
                    const data = await response.json();
                    console.log(data)
                    console.log("Uspesno ste se registovali")
                } else {
                    console.error('Registration failed.');
                }
            } catch (error) {
                console.error('An error occurred during registration:', error);
            }
        },
    },
};

export default authModule;




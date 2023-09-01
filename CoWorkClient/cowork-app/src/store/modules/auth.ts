import { Commit, Dispatch } from 'vuex';
import { JwtPayloadKeys } from '../shared/jwt-payload-keys';
import router from '@/router';
import { IJwtPayload } from '../shared/jwt-payload';
import { Role } from '../shared/role';
import axiosInstance from '../axiosInstance';

const baseUrl = process.env.NODE_ENV === 'development' ? 'http://localhost:4000/' : '/';
const headers = { "Content-Type": "application/json" }

interface Auth {
    userName?: string,
    email?: string,
    roles?: Role | Role[];
}

interface AuthState {
    accessToken?: string;
    refreshToken?: string;
    auth: Auth,
}

function parsePayload(jwtString: string): IJwtPayload {
    const jwtStringParts: string[] = jwtString.split('.');
    const payloadString: string = jwtStringParts[1];
    return JSON.parse(atob(payloadString)) as IJwtPayload;
}

const state: AuthState = {
    auth: {} as Auth
}

const mutations = {
    setAccessToken(state: AuthState, token: string) {
        state.accessToken = token;
        localStorage.setItem('access_token', state.accessToken);
        const payload = parsePayload(state.accessToken)
        state.auth.userName = payload[JwtPayloadKeys.Username]
        state.auth.email = payload[JwtPayloadKeys.Email];
        state.auth.roles = payload[JwtPayloadKeys.Role];

        localStorage.setItem('username', state.auth.userName)
        localStorage.setItem('email', state.auth.email)
        localStorage.setItem('role', JSON.stringify(state.auth.roles))
    },
    setRefreshToken(state: AuthState, token: string) {
        state.refreshToken = token;
    },
    initializeTokensFromLocalStorage(state: AuthState) {
        const accessToken = localStorage.getItem('access_token');
        const refreshToken = localStorage.getItem('refresh_token');
        const username = localStorage.getItem('username')
        const email = localStorage.getItem('email')
        const role = localStorage.getItem('role')
        if (accessToken) {
            state.accessToken = accessToken;
        }
        if (refreshToken) {
            state.refreshToken = refreshToken;
        }
        if (username) {
            state.auth.userName = username;
        }
        if (email) {
            state.auth.email = email;
        }
        if (role && role !== null) {
            state.auth.roles = JSON.parse(role)
        }
    }
}

const getters = {
    hasRole: (state: AuthState) => (role: Role) => {
        if (!state.auth.roles)
            return false
        if (typeof state.auth.roles === 'string') {
            return state.auth.roles === role
        }
        return state.auth.roles.find((registeredRole: Role) => registeredRole === role) !== undefined;
    },
    isAuthenticated(): boolean {
        return state.accessToken !== undefined && state.refreshToken !== undefined && state.auth.userName !== undefined && state.auth.roles !== undefined && state.auth.email !== undefined
    }
}

const actions = {
    initializeAuth({ commit }: { commit: Commit }) {
        commit('initializeTokensFromLocalStorage');
    },
    setTokens({ commit }: { commit: Commit }, { accessToken, refreshToken }: any) {
        commit('setAccessToken', accessToken);
        commit('setRefreshToken', refreshToken);
        localStorage.setItem('access_token', accessToken);
        localStorage.setItem('refresh_token', refreshToken);
    },
    async logIn({ dispatch }: { dispatch: Dispatch }, { username, password }: any) {
        const url = baseUrl + 'api/v1/Authentication/LogIn'
        const loginData = {
            userName: username,
            password: password
        }
        try {
            const response = await axiosInstance.post(url, JSON.stringify(loginData), { headers: headers })
            if (response.status === 200) {
                const data = await response.data;
                const { accessToken, refreshToken } = data;
                dispatch('setTokens', { accessToken, refreshToken });
                router.push({ name: 'CoWorkHome' })
            } else {
                console.error('Login failed.');
            }
        } catch (error) {
            console.error('An error occurred during login:', error);
        }
    },
    async getRefreshToken({ state, dispatch }: { state: AuthState, dispatch: Dispatch }) {
        const url = baseUrl + 'api/v1/Authentication/Refresh'
        const refreshTokenData = {
            userName: state.auth.userName,
            refreshToken: state.refreshToken
        }

        try {
            const response = await axiosInstance.post(url, JSON.stringify(refreshTokenData), { headers: headers })
            if (response.status === 200) {
                const data = await response.data;
                const { accessToken, refreshToken } = data;
                dispatch('setTokens', { accessToken, refreshToken });

                console.log("Uspesno postavljen REFRESH token")
            } else {
                console.error('Login failed.');
            }
        } catch (error) {
            console.error('An error occurred during registration:', error);
        }
    },
    async logOut({ state }: { state: AuthState }) {
        const url = baseUrl + 'api/v1/Authentication/Logout'

        const logOutData = {
            "userName": state.auth.userName,
            "refreshToken": state.refreshToken
        }

        const headers = {
            "Content-Type": "application/json",
            Authorization: `Bearer ${state.accessToken}`
        }


        try {
            const response = await axiosInstance.post(url, JSON.stringify(logOutData), { headers: headers });
            if (response.status === 202) {
                const keys = Object.keys(localStorage)
                for (const key of keys) {
                    localStorage.removeItem(key);
                }
                router.push({ name: 'LogIn' })
            } else {
                console.error('Neuspesno  odjavljivanje.');
            }
        } catch (error) {
            console.error('An error occurred during logout:', error);
        }
    },
}
export default {
    namespace: true,
    state,
    mutations,
    actions,
    getters,
}




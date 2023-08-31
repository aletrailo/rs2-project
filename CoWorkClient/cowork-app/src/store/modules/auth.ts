import { Commit, Dispatch, Module } from 'vuex';
import { JwtPayloadKeys } from '../shared/jwt-payload-keys';
import router from '@/router';
const baseUrl = process.env.NODE_ENV === 'development' ? 'http://localhost:4000/' : '/';
const headers = { "Content-Type": "application/json" }
import { IJwtPayload } from '../shared/jwt-payload';
import { Role } from '../shared/role';
import axiosInstance from '../axiosInstance';


interface User {
    id: string | null,
    firstName: string | null,
    lastName: string | null,
    email: string | null,
}

interface Auth {
    username: string,
    email: string,
    role: Role | Role[];
}

interface AuthState {
    accessToken: string | null;
    refreshToken: string | null;
    user: User,
    auth: Auth,
    users: User[]
}

function parsePayload(jwtString: string): IJwtPayload {
    const jwtStringParts: string[] = jwtString.split('.');
    const payloadString: string = jwtStringParts[1];
    return JSON.parse(atob(payloadString)) as IJwtPayload;
}

const state: AuthState = {

    accessToken: null,
    refreshToken: null,
    user: {} as User,
    auth: {} as Auth,
    users: [] as User[]

}
const mutations = {
    setAccessToken(state: AuthState, token: string) {
        state.accessToken = token;
        console.log(state.accessToken)
        localStorage.setItem('access_token', state.accessToken);
        const payload = parsePayload(state.accessToken)
        state.auth.username = payload[JwtPayloadKeys.Username]
        state.auth.email = payload[JwtPayloadKeys.Email];
        state.auth.role = payload[JwtPayloadKeys.Role];

        localStorage.setItem('username', state.auth.username)
        localStorage.setItem('email', state.auth.email)
        localStorage.setItem('role', JSON.stringify(state.auth.role))
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
            state.auth.username = username;
        }
        if (email) {
            state.auth.email = email;
        }
        if (role && role !== null) {
            state.auth.role = JSON.parse(role)
        }
    },
    SET_DATA(state: AuthState, data: any) {
        state.user = data
    },
    SET_USERS(state: AuthState, data: any) {
        state.users = data
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
    async logIn({ commit, state, dispatch }: { commit: Commit, state: AuthState, dispatch: Dispatch }, { username, password }: any) {
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
    async getRefreshToken({ commit, state, dispatch }: { commit: Commit, state: AuthState, dispatch: Dispatch }){
        const url = baseUrl + 'api/v1/Authentication/Refresh'
        const refreshTokenData = {
            userName: state.auth.username,
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
    async singIn({ commit, state, dispatch }: { commit: Commit, state: AuthState, dispatch: Dispatch }, { firstName, lastName, userName, password, email, phoneNumber }: any) {
        const url = baseUrl + 'api/v1/Authentication/RegisterUser'
        const singInData =
        {
            firstName: firstName,
            lastName: lastName,
            userName: userName,
            password: password,
            email: email,
            phoneNumber: phoneNumber
        }
        try {
            const response = await axiosInstance.post(url, JSON.stringify(singInData), {headers: headers });
            if (response.status === 201) {
                dispatch('logIn', { username: userName, password: password })
            } else {
                console.error('Registration failed.');
            }
        } catch (error) {
            console.error('An error occurred during registration:', error);
        }
    },
    async getUser({ commit, state }: { commit: Commit, state: AuthState }) {
        const url = baseUrl + 'api/v1/User/' + state.auth.username

        const headers = { Authorization: `Bearer ${state.accessToken}`, }


        try {
            const response = await axiosInstance.get(url, {headers: headers });
            if (response.status === 200) {
                const data = await response.data;
                commit('SET_DATA', data)
            } else {
                console.error('Registration failed.');
            }
        } catch (error) {
            console.error('An error occurred during registration:', error);
        }
    },
    async logOut({ commit, state }: { commit: Commit, state: AuthState }) {
        const url = baseUrl + 'api/v1/Authentication/Logout'

        const logOutData = {
            "userName": state.auth.username,
            "refreshToken": state.refreshToken
        }

        const headers = {
            "Content-Type": "application/json",
            Authorization: `Bearer ${state.accessToken}`
        }


        try {
            const response = await axiosInstance.post(url, JSON.stringify(logOutData),{headers: headers});
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
    async getAllUsers({ commit, state }: { commit: Commit, state: AuthState }) {
        const url = baseUrl + 'api/v1/User'
        const headers = { Authorization: `Bearer ${state.accessToken}`}
        try {
            const response = await axiosInstance.get(url, {headers: headers});
            if (response.status === 200) {
                const data = await response.data;
                commit('SET_USERS', data)
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
    actions
}




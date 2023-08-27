import { Module } from 'vuex';
import { RootState } from '../index';
import { JwtPayloadKeys } from '../shared/jwt-payload-keys';
import router from '@/router';
const baseUrl = process.env.NODE_ENV === 'development' ? 'http://localhost:4000/' : '/';
const headers = { "Content-Type": "application/json" }
import { IJwtPayload } from '../shared/jwt-payload';
import { Role } from '../shared/role';


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

const authModule: Module<AuthState, RootState> = {
    namespaced: true,
    state: {
        accessToken: null,
        refreshToken: null,
        user: {} as User,
        auth: {} as Auth,
        users: [] as User[]
    },
    mutations: {
        setAccessToken(state, token: string) {
            state.accessToken = token;
            const payload = parsePayload(state.accessToken)
            state.auth.username = payload[JwtPayloadKeys.Username]
            state.auth.email = payload[JwtPayloadKeys.Email];
            state.auth.role = payload[JwtPayloadKeys.Role];

            localStorage.setItem('username', state.auth.username)
            localStorage.setItem('email', state.auth.email)
            localStorage.setItem('role', JSON.stringify(state.auth.role))
        },
        setRefreshToken(state, token: string) {
            state.refreshToken = token;
        },
        initializeTokensFromLocalStorage(state) {
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
        SET_DATA(state, data) {
            state.user = data
        },
        SET_USERS(state, data) {
            state.users = data
        }

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
                    router.push({ name: 'CoWorkHome' })

                } else {
                    console.error('Login failed.');
                }
            } catch (error) {
                console.error('An error occurred during login:', error);
            }
        },
        async singIn({ commit }, { firstName, lastName, userName, password, email, phoneNumber }) {
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
                const response = await fetch(url, { method: 'POST', body: JSON.stringify(singInData), headers: headers });
                console.log(response)
                if (response.ok) {
                    this.dispatch('auth/logIn', { username: userName, password: password })
                } else {
                    console.error('Registration failed.');
                }
            } catch (error) {
                console.error('An error occurred during registration:', error);
            }
        },
        getUser({ commit, state }) {
            const url = baseUrl + 'api/v1/User/' + state.auth.username

            const headers = { Authorization: `Bearer ${state.accessToken}`, }

            fetch(url, {
                method: 'GET', mode: 'cors', headers: headers
            })
                .then((response) => {
                    if (!response.ok)
                        throw response
                    return response.json()
                })
                .then(data => { commit('SET_DATA', data) })
                .catch(error => {
                    console.error(error)
                })
        },
        async logOut({ commit, state }) {
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
                const response = await fetch(url, { method: 'POST', body: JSON.stringify(logOutData), headers: headers });
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
        getAllUsers({ commit, state }) {
            console.log('getAllUsers')
            const url = baseUrl + 'api/v1/User'
            const headers = { Authorization: `Bearer ${state.accessToken}`, }
            fetch(url, {
                method: 'GET', mode: 'cors', headers: headers
            })
                .then((response) => {
                    if (!response.ok)
                        throw response
                    return response.json()
                })
                .then(data => { commit('SET_USERS', data) })
                .catch(error => {
                    console.error(error)
                })
        }
    }

};

export default authModule;




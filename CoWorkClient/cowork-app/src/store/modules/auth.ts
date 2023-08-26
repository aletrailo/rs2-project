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
    auth: Auth
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
        auth: {} as Auth
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
            //if (role) {
             //   state.auth.role = JSON.parse(role)
           // }
        },
        SET_DATA(state, data) {
            state.user = data
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
                const response = await fetch(url, { method: 'POST', body: JSON.stringify(singInData), headers: headers });

                if (response.ok) {
                    const data = await response.json();
                    console.log("Uspesno ste se registovali")
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
        }
    },
};

export default authModule;




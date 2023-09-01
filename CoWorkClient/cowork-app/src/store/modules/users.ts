import { Commit } from 'vuex';
const baseUrl = process.env.NODE_ENV === 'development' ? 'http://localhost:4000/' : '/';
import axiosInstance from '../axiosInstance';
import auth from './auth';

interface User {
    id?: string,
    firstName?: string,
    lastName?: string,
    email?: string,
}

interface UserState{
    user: User,
    users: User[]
}

const state: UserState = {
    user: {} as User,
    users: [] as  User[]
} 

const mutations = {
    SET_DATA(state: UserState, data: any) {
        state.user = data
    },
    SET_USERS(state: UserState, data: any) {
        state.users = data
    }

}

const actions = {
    async getUser({ commit }: { commit: Commit}) {
        const headers = { Authorization: `Bearer ${auth.state.accessToken}`, }
        const url = baseUrl + 'api/v1/User/' + auth.state.auth.userName

        try {
            const response = await axiosInstance.get(url, { headers: headers });
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
    async getAllUsers({ commit }: { commit: Commit}) {
        const headers = { Authorization: `Bearer ${auth.state.accessToken}` }
        const url = baseUrl + 'api/v1/User'

        try {
            const response = await axiosInstance.get(url, { headers: headers });
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


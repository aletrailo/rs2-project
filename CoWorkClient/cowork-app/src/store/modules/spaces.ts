import { Commit } from 'vuex';
import auth from './auth';
import router from '@/router';


const baseUrl = process.env.NODE_ENV === 'development' ? 'http://localhost:8001/' : '/';
const headers = { "Content-Type": "application/json" }


interface ad {
    name: string,
    address: string,
    description: string,
    image: string,
    isFree: boolean,
    pricePerHour: number,
}

interface spaces {

    spaceId: string,
    name: string,
    address: string,
    description: string,
    image: string,
    isFree: boolean,
    pricePerHour: number,
    owner: string

}


interface SpaceState {
    spaces: spaces[],
    reservedByMe: spaces[]
}

const state: SpaceState = {
    spaces: [] as spaces[],
    reservedByMe: [] as spaces[]

}

const mutations = {
    'SET_USER_SPACES'( state: SpaceState , data: spaces[]){
        state.spaces = data
    },
    'SET_RESERVED_BY_ME'( state: SpaceState , data: spaces[]){
        state.reservedByMe = data
    },

}

const actions = {

    async getMySpaces({ commit, state }: { commit: Commit, state: SpaceState }) {
        const username = auth.state.auth.username
        const url = baseUrl + 'api/Spaces/GetAllOwnedBy?' + new URLSearchParams({ username: username })
        console.log(url)

        const headers = {
            "Content-Type": "application/json",
            Authorization: `Bearer ${auth.state.accessToken}`
        }

        try {
            const response = await fetch(url, { method: 'GET', headers: headers });
            if (response.status === 200) {
                const data = await response.json();
                commit('SET_USER_SPACES', data)

            } else {
                console.error('Neuspesno ucitavanje svih oglasa.');
            }
        } catch (error) {
            console.error('An error occurred during adding ad:', error);
        }


    },
    async getAllReservedBy({ commit, state }: { commit: Commit, state: SpaceState }) {
        const username = auth.state.auth.username
        const url = baseUrl + 'api/Spaces/GetAllReservedBy?' + new URLSearchParams({ username: username })
        console.log(url)

        const headers = {
            "Content-Type": "application/json",
            Authorization: `Bearer ${auth.state.accessToken}`
        }

        try {
            const response = await fetch(url, { method: 'GET', headers: headers });
            if (response.status === 200) {
                const data = await response.json();
                commit('SET_RESERVED_BY_ME', data)

            } else {
                console.error('Neuspesno ucitavanje svih oglasa.');
            }
        } catch (error) {
            console.error('An error occurred during adding ad:', error);
        }


    },
}

export default {
    namespace: true,
    state,
    mutations,
    actions
}
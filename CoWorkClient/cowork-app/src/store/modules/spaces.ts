import { Commit, Dispatch } from 'vuex';
import auth from './auth';
import axiosInstance from '../axiosInstance';
import { notify } from "@kyvg/vue3-notification";


const baseUrl = process.env.NODE_ENV === 'development' ? 'http://localhost:8001/' : '/';

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
    'SET_USER_SPACES'(state: SpaceState, data: spaces[]) {
        state.spaces = data
    },
    'SET_RESERVED_BY_ME'(state: SpaceState, data: spaces[]) {
        state.reservedByMe = data
    },

}

const actions = {

    async getMySpaces({ commit }: { commit: Commit }) {
        const username = auth.state.auth.userName
        const url = baseUrl + 'api/Spaces/GetAllOwnedBy?' + new URLSearchParams({ username: username ? username : '' })

        const headers = {
            "Content-Type": "application/json",
            Authorization: `Bearer ${auth.state.accessToken}`
        }

        try {
            const response = await axiosInstance.get(url, { headers: headers });
            if (response.status === 200) {
                const data = await response.data;
                commit('SET_USER_SPACES', data)

            } else {
                notify({
                    title: "Greška!",
                    text: 'Neuspesno ucitavanje svih oglasa.',
                    duration: 2000,
                    type: 'error'
                });
            }
        } catch (error) {
            notify({
                title: "Greška!",
                text: String(error),
                duration: 2000,
                type: 'error'
            });
        }


    },
    async getAllReservedBy({ commit }: { commit: Commit }) {
        const username = auth.state.auth.userName
        const url = baseUrl + 'api/Spaces/GetAllReservedBy?' + new URLSearchParams({ username: username ? username : '' })

        const headers = {
            "Content-Type": "application/json",
            Authorization: `Bearer ${auth.state.accessToken}`
        }

        try {
            const response = await axiosInstance.get(url, { headers: headers });
            if (response.status === 200) {
                const data = await response.data;
                commit('SET_RESERVED_BY_ME', data)

            } else {
                notify({
                    title: "Greška!",
                    text: 'Neuspesno ucitavanje svih oglasa.',
                    duration: 2000,
                    type: 'error'
                });

            }
        } catch (error) {
            notify({
                title: "Greška!",
                text: String(error),
                duration: 2000,
                type: 'error'
            });
        }


    },
    async deleteSpace({ dispatch }: { dispatch: Dispatch }, { spaceId }: any) {
        const url = 'http://localhost:8000/' + 'api/Advertising/DeleteAd?' + new URLSearchParams({ spaceId: spaceId })

        const headers = {
            "Content-Type": "application/json",
            Authorization: `Bearer ${auth.state.accessToken}`
        }

        try {
            const response = await axiosInstance.delete(url, { headers: headers });
            if (response.status === 200) {
                dispatch('getMySpaces');
                notify({
                    title: "",
                    text: 'Uspesno obrisan prostor!',
                    duration: 2000,
                    type: 'success'
                });
            } else {
                notify({
                    title: "Greška!",
                    text: 'Problem sa brisanjem prostora',
                    duration: 2000,
                    type: 'error'
                });
            }
        } catch (error) {
            notify({
                title: "Greška!",
                text: String(error),
                duration: 2000,
                type: 'error'
            });
        }
    },
}

export default {
    namespace: true,
    state,
    mutations,
    actions
}
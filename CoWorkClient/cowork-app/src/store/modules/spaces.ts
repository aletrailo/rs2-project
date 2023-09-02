import { Commit, Dispatch } from 'vuex';
import auth from './auth';
import axiosInstance from '../axiosInstance';
import { notify } from "@kyvg/vue3-notification";


const baseUrl = process.env.NODE_ENV === 'development' ? 'http://localhost:8001/' : '/';

interface space {
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
    space: space
    spaces: space[],
    reservedByMe: space[]
}

const state: SpaceState = {
    space: {} as space,
    spaces: [] as space[],
    reservedByMe: [] as space[]
}

const mutations = {
    'SET_USER_SPACES'(state: SpaceState, data: space[]) {
        state.spaces = data
    },
    'SET_RESERVED_BY_ME'(state: SpaceState, data: space[]) {
        state.reservedByMe = data
    },
    'SET_SPACE'( state: SpaceState, data: space){
        state.space = data
    }
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
    async getAddById( { commit, state }: { commit: Commit , state: any}, {id}: {id: string}){
      
        const url = baseUrl + 'api/Spaces/GetById?' + new URLSearchParams({ id: id })

        const headers = {
            "Content-Type": "application/json",
            Authorization: `Bearer ${auth.state.accessToken}`
        }

        try {
            const response = await axiosInstance.get(url, { headers: headers });
            if (response.status === 200) {
                const data = await response.data;
                commit('SET_SPACE', data)

            } else {
                notify({
                    title: "Greška!",
                    text: 'Neuspesno ucitavanje ovog oglasa.',
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
    async bookSpace( { commit }: { commit: Commit}, {id}: {id: string}){
      
        const url = 'http://localhost:8000/' + 'api/Advertising/BookASpace?' + new URLSearchParams({ spaceId: id })

        const headers = {
            "Content-Type": "application/json",
            Authorization: `Bearer ${auth.state.accessToken}`
        }

        try {
            const response = await axiosInstance.post(url, {}, { headers: headers });
            if (response.status === 200) {
                notify({
                    title: "",
                    text: 'Uspesno rezervisan prostor!',
                    duration: 2000,
                    type: 'success'
                })

            } else {
                notify({
                    title: "Greška!",
                    text: 'Neuspesno rezervisanje prostora.',
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
    async editSpace( { dispatch }: { dispatch: Dispatch}, {space}: {space: space}){
      
        const url = baseUrl + 'api/Spaces/Update'

        const headers = {
            "Content-Type": "application/json",
            Authorization: `Bearer ${auth.state.accessToken}`
        }

        try {
            const response = await axiosInstance.put(url,JSON.stringify(space), { headers: headers });
            if (response.status === 200) {
                dispatch('getMySpaces')
                notify({
                    title: "",
                    text: 'Izmena je uspesna!',
                    duration: 2000,
                    type: 'success'
                })
            

            } else {
                notify({
                    title: "Greška!",
                    text: 'Neuspesna izmena.',
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
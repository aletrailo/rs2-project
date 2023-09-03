import { Commit, Dispatch } from 'vuex';
import auth from './auth';
import axiosInstance from '../axiosInstance';
import { successMessage, errorMessage } from '../shared/message'
import router from '@/router';

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
    'SET_SPACE'(state: SpaceState, data: space) {
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
                errorMessage('Neuspesno ucitavanje svih oglasa.')
            }
        } catch (error) {
            errorMessage(String(error))
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
                errorMessage('Neuspesno ucitavanje svih oglasa.')
            }
        } catch (error) {
            errorMessage(String(error))
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
                successMessage('Uspesno obrisan prostor!')
            } else {
                errorMessage('Problem sa brisanjem prostora')
            }
        } catch (error) {
            errorMessage(String(error))
        }
    },
    async getAddById({ commit }: { commit: Commit }, { id }: { id: string }) {

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
                errorMessage( 'Neuspesno ucitavanje ovog oglasa.');
            }
        } catch (error) {
            errorMessage(String(error))
        }
    },
    async bookSpace({ commit }: { commit: Commit }, { id }: { id: string }) {

        const url = 'http://localhost:8000/' + 'api/Advertising/BookASpace?' + new URLSearchParams({ spaceId: id })

        const headers = {
            "Content-Type": "application/json",
            Authorization: `Bearer ${auth.state.accessToken}`
        }

        try {
            const response = await axiosInstance.post(url, {}, { headers: headers });
            if (response.status === 200) {
                successMessage('Uspesno rezervisan prostor!');
                router.push({name: 'UserProfile'})
            } else {
                errorMessage('Neuspesno rezervisanje prostora.')
            }
        } catch (error) {
            errorMessage(String(error))
        }
    },
    async cancelReservation({ dispatch }: { dispatch: Dispatch }, { spaceId }: { spaceId: string }) {
        const url = 'http://localhost:8000/' + 'api/Advertising/EndUpUsingSpace?' + new URLSearchParams({ spaceId: spaceId })

        const headers = {
            "Content-Type": "application/json",
            Authorization: `Bearer ${auth.state.accessToken}`
        }

        try {
            const response = await axiosInstance.post(url, {} , { headers: headers });
            if (response.status === 200) {
                dispatch('getAllReservedBy')
                successMessage('Prostor je uspesno otkazan!')
            } else {
                errorMessage('Neuspesna izmena.')
            }
        } catch (error) {
            errorMessage(String(error))
        }
    },
    async editSpace({ dispatch }: { dispatch: Dispatch }, { space }: { space: space }) {

        const url = baseUrl + 'api/Spaces/Update'

        const headers = {
            "Content-Type": "application/json",
            Authorization: `Bearer ${auth.state.accessToken}`
        }

        try {
            const response = await axiosInstance.put(url, JSON.stringify(space), { headers: headers });
            if (response.status === 200) {
                dispatch('getMySpaces')
                successMessage('Izmena je uspesna!')
            } else {
                errorMessage('Neuspesna izmena.')
            }
        } catch (error) {
            errorMessage(String(error))
        }
    },
}

export default {
    namespace: true,
    state,
    mutations,
    actions
}
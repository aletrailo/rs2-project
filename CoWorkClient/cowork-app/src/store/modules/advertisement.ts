import { Commit } from 'vuex';
import auth from './auth';
import router from '@/router';
import axiosInstance from '../axiosInstance';
import { notify } from "@kyvg/vue3-notification";


const baseUrl = process.env.NODE_ENV === 'development' ? 'http://localhost:8000/' : '/';


interface ad {
    name: string,
    address: string,
    description: string,
    image: string,
    isFree: boolean,
    pricePerHour: number,
}

interface advertisement {

    spaceId: string,
    name: string,
    address: string,
    description: string,
    image: string,
    isFree: boolean,
    pricePerHour: number,
    owner: string

}


interface AdState {
    ad: ad,
    advertisements: advertisement[]
}

const state: AdState = {
    ad: {
        name: "",
        address: "",
        description: "",
        image: "",
        isFree: true,
        pricePerHour: 0,
    },
    advertisements: [] as advertisement[]
}

const mutations = {
    'SET_IMAGE'(state: AdState, base64stringImage: string) {
        state.ad.image = base64stringImage;
    },
    'SET_ADVERTISEMENTS'(state: AdState, data: any) {
        state.advertisements = data
    }

}

const actions = {
    async addAd({ state }: {  state: AdState }) {
        const url = baseUrl + 'api/Advertising/AddAnAd'

        const headers = {
            "Content-Type": "application/json",
            Authorization: `Bearer ${auth.state.accessToken}`
        }

        try {
            const response = await axiosInstance.post(url, JSON.stringify(state.ad), {headers: headers });
            if (response.status === 200) {
                router.push({ name: 'CoWorkHome' })
                notify({
                    title: "",
                    text: "Uspesno dodat oglas",
                    duration: 2000,
                    type: 'success'
                });
            } else {
                notify({
                    title: "Greška!",
                    text: "Problem prilikom dodavanja oglasa",
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
    async getAllAd({ commit }: { commit: Commit }) {
        const url = baseUrl + 'api/Advertising/all'

        const headers = {
            "Content-Type": "application/json",
            Authorization: `Bearer ${auth.state.accessToken}`
        }

        try {
            const response = await axiosInstance.get(url, {headers: headers });
            if (response.status === 200) {
                const data = await response.data
                commit('SET_ADVERTISEMENTS', data)

            } else {
                notify({
                    title: "Greška!",
                    text: "Neuspesno ucitavanje svih oglasa",
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
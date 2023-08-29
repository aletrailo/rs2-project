import { Commit } from 'vuex';
import auth from './auth';
import router from '@/router';


const baseUrl = process.env.NODE_ENV === 'development' ? 'http://localhost:8000/' : '/';
const headers = { "Content-Type": "application/json" }


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
    async addAd({ commit, state }: { commit: Commit, state: AdState }) {
        const url = baseUrl + 'api/Advertising/AddAnAd'

        console.log(state.ad.image)

        const headers = {
            "Content-Type": "application/json",
            Authorization: `Bearer ${auth.state.accessToken}`
        }

        try {
            const response = await fetch(url, { method: 'POST', body: JSON.stringify(state.ad), headers: headers });
            if (response.status === 200) {
                console.log('usepesno')
                router.push({ name: 'CoWorkHome' })
            } else {
                console.error('Neuspesno  dodavanje oglasa.');
            }
        } catch (error) {
            console.error('An error occurred during adding ad:', error);
        }
    },
    async getAllAd({ commit, state }: { commit: Commit, state: AdState }) {
        const url = baseUrl + 'api/Advertising/all'

        const headers = {
            "Content-Type": "application/json",
            Authorization: `Bearer ${auth.state.accessToken}`
        }

        try {
            const response = await fetch(url, { method: 'GET', headers: headers });
            if (response.status === 200) {
                const data = await response.json();
                commit('SET_ADVERTISEMENTS', data)

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
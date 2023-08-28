import { Module, useStore } from 'vuex';
import { RootState } from '../index';
import { Commit, Dispatch } from 'vuex';
import auth from './auth';

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

interface AdState {
    ad: ad,
}

const state: AdState = {
        ad: {
            name: "",
            address: "",
            description: "",
            image: "",
            isFree: true,
            pricePerHour: 0,
        }
}

const mutations = {
        'SET_IMAGE'(state: AdState, formData:any) {
            const url = baseUrl + 'api/v1/Authentication/RegisterUser'
            state.ad.image = JSON.stringify(formData)
        }

    }

const actions= {
        async addAd({ commit, state }: {commit: Commit, state: AdState}){
            const url = baseUrl +'api/Advertising/AddAnAd'
            console.log(auth.state.accessToken)
            const headers = {
                "Content-Type": "application/json",
                Authorization: `Bearer ${auth.state.accessToken}`
            }
            const dataToAdd = state.ad
            try {
                const response = await fetch(url, { method: 'POST', body: JSON.stringify(state.ad), headers: headers });
                if (response.status === 200) {
                    console.log('usepesno')
                } else {
                    console.error('Neuspesno  dodavanje oglasa.');
                }
            } catch (error) {
                console.error('An error occurred during adding ad:', error);
            }
        
       }
}

export default {
    namespace : true,
    state,
    mutations,
    actions
  }
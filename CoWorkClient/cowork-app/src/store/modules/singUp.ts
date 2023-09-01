import { Dispatch } from 'vuex';
const baseUrl = process.env.NODE_ENV === 'development' ? 'http://localhost:4000/' : '/';
import axiosInstance from '../axiosInstance';
import auth from './auth';
import { notify } from "@kyvg/vue3-notification";

const headers = { Authorization: `Bearer ${auth.state.accessToken}`, }

interface SingUp {
    firstName?: string,
    lastName?: string,
    userName?: string,
    password?: string,
    email?: string,
    phoneNumber?: string
}

interface SingUpData {
    singUpData: SingUp
}

const state: SingUpData = {
    singUpData: {} as SingUp
}

const actions = {
    async singUp({ state, dispatch }: { state: SingUpData, dispatch: Dispatch }) {
        const url = baseUrl + 'api/v1/Authentication/RegisterUser'

        try {
            const response = await axiosInstance.post(url, JSON.stringify(state.singUpData), { headers: headers });
            if (response.status === 201) {
                dispatch('logIn', { username: state.singUpData.userName, password: state.singUpData.password })
            } else {
                notify({
                    title: "Greška!",
                    text: 'Neuspesna regisracija.',
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
    actions
}


import { createStore } from 'vuex'
const baseUrl = process.env.NODE_ENV === 'development' ? 'http://localhost:4000/' : '/';
const headers = { "Content-Type": "application/json" }
export default createStore({
  state: {
  },
  getters: {
  },
  mutations: {
  },
  actions: {
    logIn({ commit, state }, { username, password }) {
      const url = baseUrl + 'api/v1/Authentication/LogIn'
      const data_to_post = {
        userName: username,
        password: password
      }

      fetch(url, {
        method: 'POST', mode: 'cors', headers: headers, body: JSON.stringify(data_to_post)
      })
        .then((response) => {
          if (!response.ok)
            throw response
          return response.json()
        })
        .then(data => { console.log(data) })
        .catch(error => {
          console.error(error)
        })
    },
    singIn({ commit, state }, { firstName, lastName, userNeme, password, email, phoneNumber }) {
      const url = baseUrl + 'api/v1/Authentication/RegisterUser'

      const data_to_post =
      {
        firstName: firstName,
        lastName: lastName,
        userNeme: userNeme,
        password: password,
        email: email,
        phoneNumber: phoneNumber
      }


      fetch(url, {
        method: 'POST', mode: 'cors', headers: headers, body: JSON.stringify(data_to_post)
      })
        .then((response) => {
          if (!response.ok)
            throw response
          return response.json()
        })
        .then(data => { console.log(data) })
        .catch(error => {
          console.error(error)
        })
    }
  },
  modules: {
  }
})

import { createStore } from 'vuex'
const baseUrl = process.env.NODE_ENV === 'development' ? 'http://localhost:4000/' : '/';
export default createStore({
  state: {
  },
  getters: {
  },
  mutations: {
  },
  actions: {
    logIn({ commit, state }, { username, password }) {
      const url = baseUrl + ''
      const data_to_post = {
        username: username,
        password: password
      }

      fetch(url, {
        method: 'POST', credentials: 'include', mode: 'cors', body: JSON.stringify(data_to_post)
      })
        .then((response) => {
          if (!response.ok)
            throw response
          return response.json()
        })
        .then(data => { console.log(data) })
        .catch(error => {
          error.json().then((data: any) => { console.log(data) })
        })
    }
  },
  modules: {
  }
})

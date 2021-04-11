import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)
axios.defaults.baseURL = 'https://localhost:44323/api/Accounts'

export const store = new Vuex.Store({
  state: {
    token: localStorage.getItem('token') || null,
    role: localStorage.getItem('role') || null,
    fullName: localStorage.getItem('fullName') || null,
    userID: localStorage.getItem('userID') || null,
  },
  getters: {
    loggedIn(state) {
      return state.token !== null
    },
    currentRole(state) {
      return state.role
    },
    currentName(state) {
      return state.fullName
    },
    currentToken(state) {
      return state.token
    },
    currentID(state) {
      return state.userID
    }
  },
  mutations: {
    retrieveToken(state, data) {
      state.token = data.token,
        state.role = data.role,
        state.fullName = data.fullName,
        state.userID = data.userID
    },
    destroyToken(state) {
      state.token = null,
        state.role = null,
        state.fullName = null,
        state.userID = null
    }
  },
  actions: {
    destroyToken(context) {
      axios.defaults.headers.common['Authorization'] = 'Bearer ' + context.state.token

      if (context.getters.loggedIn) {
        return new Promise((resolve, reject) => {
          axios.post('/logout')
            .then(response => {
              localStorage.removeItem('token')
              context.commit('destroyToken')
              resolve(response)
            })
            .catch(error => {
              localStorage.removeItem('token')
              context.commit('destroyToken')
              reject(error)
            })
        })
      }
    },
    retrieveToken(context, credentials) {
      return new Promise((resolve, reject) => {
        axios.post('/Login', {
          UserName: credentials.UserName,
          Password: credentials.Password,
        })
          .then(response => {
            const token = response.data.token
            const role = response.data.role
            const fullName = response.data.fullName
            const userID = response.data.userID
            localStorage.setItem('token', token)
            localStorage.setItem('role', role)
            localStorage.setItem('fullName', fullName)
            localStorage.setItem('userID', userID)
            context.commit('retrieveToken', response.data)
            resolve(response)
            // console.log(response);
            // context.commit('addTodo', response.data)
          })
          .catch(error => {
            console.log(error)
            reject(error)
          })
      })
    },

  }
})

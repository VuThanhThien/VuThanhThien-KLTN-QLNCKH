
import Notifications from 'vue-notification'
import Vue from 'vue'
import VueRouter from 'vue-router'
import routes from './routers/routes'
import Header from './components/layouts/Header'
import { store } from './store/store'
import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.light.css';
import VueSession from 'vue-session';
import moment from 'moment'
import VueConfirmDialog from 'vue-confirm-dialog'
import { Datetime } from 'vue-datetime'
import 'vue-datetime/dist/vue-datetime.css'
import InputTag from 'vue-input-tag'

Vue.component('input-tag', InputTag)
Vue.use(Datetime) 
Vue.use(VueConfirmDialog)
Vue.component('vue-confirm-dialog', VueConfirmDialog.default)
Vue.prototype.moment = moment
window.eventBus = new Vue()
Vue.use(VueSession)
Vue.config.productionTip = false
Vue.use(VueRouter)
Vue.use(Notifications)
const router = new VueRouter({
  routes,
  mode: 'history'
})

router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (!store.getters.loggedIn) {
      next({
        name: 'Login',
      })
    } else {
      next()
    }
  } else if (to.matched.some(record => record.meta.requiresVisitor)) {
    if (store.getters.loggedIn) {
      next()
      // next({
      //   name: 'todo',
      // })
    } else {
      next()
    }
  } else {
    next()
  }
})

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router: router,
  store: store,
  components: { Header },
  template: '<Header/>'
})

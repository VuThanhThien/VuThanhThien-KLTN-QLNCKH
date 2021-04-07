import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.light.css';
import Notifications from 'vue-notification'
import Vue from 'vue'
import VueRouter from 'vue-router'
import routes from './routers/routes'
import Header from './components/layouts/Header'
import { store } from './store/store'
import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.light.css';
window.eventBus = new Vue()

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

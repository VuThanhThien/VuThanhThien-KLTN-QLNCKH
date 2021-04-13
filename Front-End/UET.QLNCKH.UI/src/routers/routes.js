import App from '@/App'
import About from '@/components/marketing/About'
import Login from '@/components/auth/Login'
import Logout from '@/components/auth/Logout'
import GridTopic from '@/components/grid/GridTopic.vue'
import GridUser from '@/components/grid/GridUser.vue'
import MyTopic from '@/components/grid/MyTopic.vue'
import MyInfo from '@/components/grid/MyInfo.vue'
import ChartExpire from '@/components/chart/ChartExpire.vue'
const routes = [
  
  {
    path: '/ListTopic',
    name: 'ListTopic',
    component: GridTopic,
    meta: {
      requiresAuth: false,
    }
  },
  {
    path: '/MyTopic',
    name: 'MyTopic',
    component: MyTopic,
    meta: {
      requiresAuth: false,
    }
  },
  {
    path: '/ListAuthor',
    name: 'ListAuthor',
    component: GridUser,
    meta: {
      requiresAuth: true,
    }
  },
  {
    path: '/Home',
    name: 'Home',
    component: App,
    meta: {
      requiresAuth: true,
    }
  },
  {
    path: '/about',
    name: 'about',
    component: About
  },
  {
    path: '/Login',
    name: 'Login',
    component: Login,
    meta: {
      requiresVisitor: true,
    }
  },
  {
    path: '/logout',
    name: 'logout',
    component: Logout
  },
  {
    path: '/ChartExpire',
    name: 'ChartExpire',
    component: ChartExpire,
    meta: {
      requiresAuth: true,
    }
  },
  {
    path: '/MyInfo',
    name: 'MyInfo',
    component: MyInfo,
    meta: {
      requiresAuth: true,
    }
  },
]

export default routes

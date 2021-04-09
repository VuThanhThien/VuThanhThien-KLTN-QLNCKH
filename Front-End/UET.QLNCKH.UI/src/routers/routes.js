import App from '@/App'
import About from '@/components/marketing/About'
import Login from '@/components/auth/Login'
import Logout from '@/components/auth/Logout'
import Register from '@/components/auth/Register'
import GridTopic from '@/components/grid/GridTopic.vue'
import GridUser from '@/components/grid/GridUser.vue'
import MyTopic from '@/components/grid/MyTopic.vue'
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
    path: '/register',
    name: 'register',
    component: Register,
    meta: {
      requiresVisitor: true,
    }
  },
  {
    path: '/logout',
    name: 'logout',
    component: Logout
  }
]

export default routes

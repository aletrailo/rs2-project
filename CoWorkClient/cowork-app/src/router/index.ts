import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import LogIn from '../views/LogIn.vue'
import SingUp from '../views/SingUp.vue'
import CoWorkHome from '../views/CoWorkHome.vue'
import UsersList from '../views/UsersList.vue'
import UserProfile from '../views/UserProfile.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'LogIn',
    component: LogIn
  },
  {
    path: '/sing-up',
    name: 'SingUp',
    component: SingUp
  },
  {
    path: '/home',
    name: 'CoWorkHome',
    component: CoWorkHome
  },
  {
    path: '/users',
    name: 'UsersList',
    component: UsersList
  },
  {
    path: '/user',
    name: 'UserProfile',
    component: UserProfile
  },

]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router

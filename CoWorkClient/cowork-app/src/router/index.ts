import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import LogIn from '../views/LogIn.vue'
import SingUp from '../views/SingUp.vue'

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

]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router

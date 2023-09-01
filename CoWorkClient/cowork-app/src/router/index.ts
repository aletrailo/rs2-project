import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import LogIn from '../views/LogIn.vue'
import SingUp from '../views/SingUp.vue'
import CoWorkHome from '../views/CoWorkHome.vue'
import UsersList from '../views/UsersList.vue'
import UserProfile from '../views/UserProfile.vue'
import SpaceAdvertisement from '../views/SpaceAdvertisement.vue'
import NotFound from '../views/NotFound.vue'
import store from '@/store'
import { Role } from '@/store/shared/role'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'CoWorkHome',
    component: CoWorkHome
  },
  {
    path: '/login',
    name: 'LogIn',
    component: LogIn
  },
  {
    path: '/sing-up',
    name: 'SingUp',
    component: SingUp
  },
  {
    path: '/users',
    name: 'UsersList',
    component: UsersList,
    beforeEnter: (to, from, next) => {
      if (store.getters.hasRole(Role.Admin)) {
        next();
      } else {
        next({ path: String(from.path) });
      }
    },
  },
  {
    path: '/user',
    name: 'UserProfile',
    component: UserProfile
  },
  {
    path: '/space-advertisement',
    name: 'SpaceAdvertisement',
    component: SpaceAdvertisement
  },
  { path: '/:pathMatch(.*)*', 
    name: 'NotFound', 
    component: NotFound 
  },

]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

router.beforeEach(async (to, from, next) => {
  const isAuthenticated =store.getters.isAuthenticated
  
  if(to.name === 'SingUp'){
    next()
  }
  else if ( !isAuthenticated && to.name !== 'LogIn') {
    next({ name: 'LogIn' });
  }
  else{
    next()
  }
})

export default router

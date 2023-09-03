import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import LogIn from '../views/LogIn.vue'
import SingUp from '../views/SingUp.vue'
import CoWorkHome from '../views/CoWorkHome.vue'
import UsersList from '../views/UsersList.vue'
import UserProfile from '../views/UserProfile.vue'
import SpaceAdvertisement from '../views/SpaceAdvertisement.vue'
import AdInfo from '../views/AdInfo.vue'
import NotFound from '../views/NotFound.vue'
import store from '@/store'
import { Role } from '@/store/shared/role'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'CoWorkHome',
    component: CoWorkHome,
    meta: {
      title: 'CoWrok'
    }
  },
  {
    path: '/login',
    name: 'LogIn',
    component: LogIn,
    meta: {
      title: 'CoWrok pijava'
    }
  },
  {
    path: '/sing-up',
    name: 'SingUp',
    component: SingUp,
    meta: {
      title: 'CoWrok registracija'
    }
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
    meta: {
      title: 'Spisak korisnika'
    }
  },
  {
    path: '/user',
    name: 'UserProfile',
    component: UserProfile,
    meta: {
      title: 'Profil'
    }
  },
  {
    path: '/space-advertisement',
    name: 'SpaceAdvertisement',
    component: SpaceAdvertisement,
    meta: {
      title: 'Postavljanje oglasa'
    }
  },
  {
    path: '/space-details/:id',
    name: 'AdInfo',
    component: AdInfo,
    meta: {
      title: 'Informacije o prostoru'
    }
  },
  {
    path: '/:pathMatch(.*)*',
    name: 'NotFound',
    component: NotFound,
    meta: {
      title: '404'
    }
  },

]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

router.beforeEach(async (to, from, next) => {
  const title = to.meta.title
  if (title) {
    document.title = String(title)
  }

  if (localStorage.getItem('access_token') && localStorage.getItem('refresh_token') && localStorage.getItem('username') &&
    localStorage.getItem('email') && localStorage.getItem('role')) {
    next()
  }
  else if (to.name === 'SingUp') {
    next()
  }
  else if (!store.getters.isAuthenticated && to.name !== 'LogIn') {
    next({ name: 'LogIn' });
  }
  else {
    next()
  }
})

export default router

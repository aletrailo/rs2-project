
import { createStore } from 'vuex';
import auth from './modules/auth';
import advertisement from './modules/advertisement';
import spaces from './modules/spaces';
import users from './modules/users';
import singUp from './modules/singUp';

const store = createStore({
  modules: {
    auth,
    advertisement,
    spaces,
    users,
    singUp
  },
});

export default store;

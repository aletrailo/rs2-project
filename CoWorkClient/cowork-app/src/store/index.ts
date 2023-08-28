
import { createStore, Store } from 'vuex';
import authModule from './modules/auth';
import advertisement from './modules/advertisement';
const baseUrl = process.env.NODE_ENV === 'development' ? 'http://localhost:4000/' : '/';

export interface RootState {
  user: string
}

const store: Store<RootState> = createStore({
  modules: {
    auth: authModule,
    advertisement: advertisement
  },

});

export default store;

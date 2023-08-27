/* eslint-disable */
import { createStore, Store } from 'vuex';
import authModule from './modules/auth';
const baseUrl = process.env.NODE_ENV === 'development' ? 'http://localhost:4000/' : '/';

export interface RootState {
  User: string
}

const store: Store<RootState> = createStore({
  modules: {
    auth: authModule,
  },
  actions: {
  
  }

});

export default store;

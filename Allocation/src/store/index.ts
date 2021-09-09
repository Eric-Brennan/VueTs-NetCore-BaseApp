import Vue from "vue";
import Vuex from "vuex";
import createPersistedState from "vuex-persistedstate";
import Profile from "@/store/modules/profile";
import { getModule } from "vuex-module-decorators";

Vue.use(Vuex);

const store = new Vuex.Store({
  plugins: [
    createPersistedState({
      storage: window.sessionStorage
    })
  ],
  modules: {
    Profile
  }
});

export default store;

export const ProfileModule = getModule(Profile, store);

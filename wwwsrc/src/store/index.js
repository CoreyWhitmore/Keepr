import Vue from "vue";
import Vuex from "vuex";
import { api } from "../services/AxiosService.js";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    profile: {},
    keeps: [],
  },
  mutations: {
    setProfile(state, profile) {
      state.profile = profile;
    },
    setPublicKeeps(state, keeps) {
      state.keeps = keeps;
    }
  },
  actions: {
    async getProfile({ commit }) {
      try {
        let res = await api.get("profiles");
        commit("setProfile", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async getPublicKeeps({ commit, dispatch }) {
      try {
        let res = await api.get("keeps")
        commit("setPublicKeeps", res.data);
      } catch (error) {
        console.error(error);
      }
    },
  },
});

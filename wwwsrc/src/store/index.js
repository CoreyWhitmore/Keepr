import Vue from "vue";
import Vuex from "vuex";
import { api } from "../services/AxiosService.js";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    profile: {},
    foreignProfile: {},
    keeps: [],
    vaults: [],
    activeVaultKeeps: [],
  },
  mutations: {
    setProfile(state, profile) {
      state.profile = profile;
    },
    setForeignProfile(state, foreignProfile) {
      state.foreignProfile = foreignProfile;
    },
    setPublicKeeps(state, keeps) {
      state.keeps = keeps;
    },
    setPublicVaults(state, vaults) {
      state.vaults = vaults;
    },
    setActiveVaultKeeps(state, keeps) {
      state.activeVaultKeeps = keeps;
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
    async getForeignProfile({ commit }, id) {
      try {
        let res = await api.get("profiles/" + id);
        commit("setForeignProfile", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async getPublicKeeps({ commit }) {
      try {
        let res = await api.get("keeps")
        commit("setPublicKeeps", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async getProfileKeeps({ commit, state }) {
      try {
        let id = state.foreignProfile.id
        let res = await api.get("profiles/" + id + "/keeps")
        commit("setPublicKeeps", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async getProfileVaults({ commit, state }) {
      try {
        let id = state.foreignProfile.id
        let res = await api.get("profiles/" + id + "/vaults")
        commit("setPublicVaults", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async getVaultKeeps({ commit, dispatch }, vaultId) {
      try {
        let res = await api.get("vaults/" + vaultId + "/keeps")
        commit("setActiveVaultKeeps", res.data);
      } catch (error) {
        console.error(error);
      }
    }
  },
});

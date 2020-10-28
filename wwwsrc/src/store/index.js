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
    myVaults: [],
    activeVault: {},
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
    setMyVaults(state, vaults) {
      state.myVaults = vaults;
    },
    setActiveVaultKeeps(state, activeVaultKeeps) {
      state.activeVaultKeeps = activeVaultKeeps;
    },
    setActiveVault(state, vault) {
      state.activeVault = vault;
    },
    updateKeep(state, newKeep) {
      let index = state.keeps.findIndex(k => k.id = newKeep.id)
      state.keeps[index] = newKeep
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
    async getMyVaults({ commit, state }) {
      try {
        let id = state.profile.id
        let res = await api.get("profiles/" + id + "/vaults")
        commit("setMyVaults", res.data);
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
    },
    async createVault({ commit, dispatch }, newVault) {
      try {
        let res = await api.post("vaults", newVault)
        dispatch("getProfileVaults")
      } catch (error) {
        console.error(error);
      }
    },
    async getActiveVault({ commit }, vaultId) {
      try {
        let res = await api.get("vaults/" + vaultId)
        commit("setActiveVault", res.data);
      } catch (error) {
        console.error(error);
      }
    },
    async createKeep({ commit, dispatch }, newKeep) {
      try {
        let res = await api.post("keeps", newKeep)
        dispatch("getProfileKeeps")
      } catch (error) {
        console.error(error);
      }
    },
    async editKeep({ commit, dispatch }, newKeep) {
      try {
        let res = await api.put("keeps/" + newKeep.id, newKeep)
        commit("updateKeep", newKeep)
      } catch (error) {
        console.error(error);
      }
    },
    async createVaultKeep({ commit, dispatch }, vaultKeep) {
      try {
        await api.post("vaultkeeps", vaultKeep)
      } catch (error) {
        console.error(error);
      }
    }
  },
});

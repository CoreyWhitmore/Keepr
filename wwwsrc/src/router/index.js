import Vue from "vue";
import VueRouter from "vue-router";
// @ts-ignore
import Home from "../pages/Home.vue";
// @ts-ignore
import Profile from "../pages/Profile.vue";
// @ts-ignore
import Vault from "../pages/Vault.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home,
  },
  {
    path: "/profile/:profileId",
    name: "Profile",
    component: Profile,
  },
  {
    path: "/vault/:vaultId",
    name: "Vault",
    component: Vault,
  },
  {
    path: "*",
    redirect: '/'
  }
];

const router = new VueRouter({
  routes,
});

export default router;

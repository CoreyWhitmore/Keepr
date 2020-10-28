<template>
  <div class="home">
    <h1>{{activeVault.name}}</h1>
    <h6>{{activeVault.description}}</h6>
    <h6>Keeps: {{keepsLength}}</h6>
    <div class="card-columns">
      <keep-component v-for="keep in activeVaultKeeps" :key="keep.id" :keepProp="keep"></keep-component>
    </div>
  </div>
</template>

<script>
  import keepComponent from "../components/keepComponent.vue"
  export default {
    name: "home",
    mounted() {
      this.$store.dispatch("getActiveVault", this.$route.params.vaultId)
      this.$store.dispatch("getVaultKeeps", this.$route.params.vaultId)
      console.log(this.activeVaultKeeps);
    },
    computed: {
      activeVaultKeeps() {
        return this.$store.state.activeVaultKeeps
      },
      activeVault() {
        return this.$store.state.activeVault
      },
      keepsLength() {
        if (this.activeVaultKeeps) {
          return this.activeVaultKeeps.length
        }
        return 0
      }
    },
    components: {
      keepComponent
    },
  };
</script>
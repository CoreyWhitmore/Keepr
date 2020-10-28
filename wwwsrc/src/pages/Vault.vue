<template>
  <div class="home">
    <h1>{{activeVault.name}}</h1>
    <h6>{{activeVault.description}}</h6>
    <h6>Keeps: {{keepsLength}}</h6>
    <div class="card-columns">
      <keep-component v-for="keep in keeps" :key="keep.id" :keepProp="keep"></keep-component>
    </div>
  </div>
</template>

<script>
  export default {
    name: "home",
    mounted() {
      this.$store.dispatch("getActiveVault", this.$route.params.vaultId)
      this.$store.dispatch("getVaultKeeps", this.$route.params.vaultId)
      console.log(this.keeps);
    },
    computed: {
      keeps() {
        return this.$store.state.activeVaultkeeps
      },
      activeVault() {
        return this.$store.state.activeVault
      },
      keepsLength() {
        if (this.keeps) {
          return keeps.length
        }
        return 0
      }
    },
  };
</script>
<template>
  <div class="home">
    <div class="card-columns">
      <keep-component v-for="keep in keeps" :key="keep.id" :keepProp="keep"></keep-component>
    </div>
  </div>
</template>

<script>
  export default {
    name: "home",
    mounted() {
      this.$store.dispatch("getVaultKeeps", this.$route.params.vaultId)
    },
    computed: {

      keeps() {
        return this.$store.state.activeVaultkeeps
      }
    },
  };
</script>
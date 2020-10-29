<template>
  <div class="home container-fluid py-3">
    <div class="card-columns">
      <keep-component v-for="keep in keeps" :key="keep.id" :keepProp="keep"></keep-component>
    </div>
  </div>
</template>

<script>
  import keepComponent from "../components/keepComponent.vue"
  export default {
    name: "home",
    mounted() {
      this.$store.dispatch("getPublicKeeps")
      this.$store.dispatch("getMyVaults")
    },
    data() {
      return {

      }
    },
    computed: {
      keeps() {
        return this.$store.state.keeps
      }
    },
    components: {
      keepComponent
    },
  };
</script>
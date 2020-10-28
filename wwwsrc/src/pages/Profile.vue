<template>
  <div class="profile container-fluid">
    <div class="row p-4">
      <img :src="profile.picture" alt="" class="mr-3">
      <div class="col">
        <h1>{{profile.name}}</h1>
        <h3>Vaults: {{vaults.length}}</h3>
        <h3>Keeps: {{keeps.length}}</h3>
      </div>
    </div>
    <div class="row flex-column p-3">
      <h2>My Vaults</h2>
      <div class="card-columns">
        <vault-component v-for="vault in vaults" :key="vault.id" :vaultProp="vault"></vault-component>
      </div>
    </div>
    <div class="row flex-column p-3">
      <h2>My Keeps</h2>
      <div class="card-columns">
        <keep-component v-for="keep in keeps" :key="keep.id" :keepProp="keep"></keep-component>
      </div>
    </div>
  </div>
</template>

<script>
  import keepComponent from "../components/keepComponent.vue"
  import vaultComponent from "../components/vaultComponent.vue"
  export default {
    name: "profile",
    mounted() {
      this.$store.dispatch("getForeignProfile", this.$route.params.profileId)
      this.$store.dispatch("getProfileKeeps")
      this.$store.dispatch("getProfileVaults")
    },
    computed: {
      profile() {
        return this.$store.state.foreignProfile
      },
      keeps() {
        return this.$store.state.keeps
      },
      vaults() {
        return this.$store.state.vaults
      }
    },
    components: {
      keepComponent,
      vaultComponent
    },
  };
</script>
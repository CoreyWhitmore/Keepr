<template>
  <div class="home">
    <div class="d-flex align-items-center">
      <h1>{{activeVault.name}}</h1>
      <i v-if="isOwner" class="fa fa-trash ml-3 text-danger" aria-hidden="true"
        @click="deleteVault(activeVault.id)"></i>
    </div>
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
      this.$store.dispatch("getProfile")
      this.$store.dispatch("getActiveVault", this.$route.params.vaultId)
      this.checkPermissions()
    },
    data() {
      return {
        count: 0
      }
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
      },
      isOwner() {
        let result = false
        try {
          result = this.$store.state.activeVault.creator.id == this.$store.state.profile.id
        } catch (error) {
        }
        return result
      },
      profile() {
        return this.$store.state.profile
      },
    },
    methods: {
      deleteVault(id) {
        if (confirm("Do you really want to delete this vault?")) {
          this.$store.dispatch("deleteVault", id)
          this.$router.push({ name: 'Profile', params: { profileId: this.profile.id } })
        }
      },
      checkPermissions() {
        if (this.activeVault.isPrivate == undefined && this.count < 150) {
          setTimeout(() => {
            this.count++
            this.checkPermissions()
          }, 200);
        }
        else if (!this.isOwner && this.activeVault.isPrivate != false) {
          this.$router.push({ name: 'Home' })
        }
        else {
          this.$store.dispatch("getVaultKeeps", this.$route.params.vaultId)
        }

      }
    },
    components: {
      keepComponent
    },
  };
</script>
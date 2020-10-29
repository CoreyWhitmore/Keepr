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
    <div class="row p-3">

      <div class="col-12 d-flex align-items-center">
        <h2>My Vaults</h2>
        <div type="button" data-toggle="modal" data-target="#createVaultModal">
          <i v-if="isOwner" class="fa fa-plus pl-3 text-success" aria-hidden="true"></i>
        </div>
      </div>
      <div class="col-12">
        <div class="card-columns">
          <vault-component v-for="vault in vaults" :key="vault.id" :vaultProp="vault"></vault-component>
        </div>
      </div>
    </div>
    <div class="row flex-column p-3">
      <div class="d-flex align-items-center">
        <h2>My Keeps</h2>
        <div type="button" data-toggle="modal" data-target="#createKeepModal">
          <i v-if="isOwner" class="fa fa-plus pl-3 text-success" aria-hidden="true"></i>
        </div>
      </div>
      <div class="card-columns">
        <keep-component v-for="keep in keeps" :key="keep.id" :keepProp="keep"></keep-component>
      </div>
    </div>


    <!-- Create Vault Modal -->
    <div class="modal fade" id="createVaultModal" data-backdrop="static" data-keyboard="false" tabindex="-1"
      aria-labelledby="createVaultModalLabel" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered modal">
        <div class="modal-content">
          <div class="modal-body">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">X</button>
            <!-- Create Vault Form -->
            <form @submit.prevent="createVault">
              <div class="input-group mb-3">
                <input type="text" class="form-control bg-light" v-model="newVault.name" placeholder="Vault Name">
                <input type="text" class="form-control bg-light" v-model="newVault.description"
                  placeholder="Description">
                <div class="form-check">
                  <input class="form-check-input" type="checkbox" v-model="newVault.isPrivate" id="defaultCheck1">
                  <label class="form-check-label" for="defaultCheck1">
                    Private?
                  </label>
                </div>
                <div class="input-group-append">
                  <button v-if="newVault.name" class="btn btn-secondary" type="submit">Create Vault</button>
                </div>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>

    <!-- Create Keep Modal -->
    <div class="modal fade" id="createKeepModal" data-backdrop="static" data-keyboard="false" tabindex="-1"
      aria-labelledby="createKeepModalLabel" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered modal">
        <div class="modal-content">
          <div class="modal-body">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">X</button>
            <!-- Create Keep Form -->
            <form @submit.prevent="createKeep">
              <div class="input-group mb-3">
                <input type="text" class="form-control bg-light" v-model="newKeep.name" placeholder="Keep Name">
                <input type="text" class="form-control bg-light" v-model="newKeep.img" placeholder="Image URL">
                <input type="text" class="form-control bg-light" v-model="newKeep.description"
                  placeholder="Description">
                <div class="input-group-append">
                  <button v-if="newKeep.name && newKeep.img" class="btn btn-secondary" type="submit">Create
                    Keep</button>
                </div>
              </div>
            </form>
          </div>
        </div>
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
      this.$store.dispatch("getProfile")
      this.$store.dispatch("getForeignProfile", this.$route.params.profileId)
      this.$store.dispatch("getProfileVaults", this.$route.params.profileId)
      this.$store.dispatch("getProfileKeeps", this.$route.params.profileId)
      console.log(this.$store.state.foreignProfile);
    },
    data() {
      return {
        newVault: {
          isPrivate: false,
        },
        newKeep: {},
      }
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
      },
      isOwner() {
        return this.profile.profileId == this.$store.state.foreignProfile.profileId
      }
    },
    methods: {
      createVault() {
        if (this.newVault.name) {
          this.$store.dispatch("createVault", this.newVault)
        }
        else {
          console.log("Error");
        }
      },
      createKeep() {
        if (this.newKeep.name && this.newKeep.img) {
          this.$store.dispatch("createKeep", this.newKeep)
        }
        else {
          console.log("Error");
        }
      }
    },
    components: {
      keepComponent,
      vaultComponent
    },
  };
</script>
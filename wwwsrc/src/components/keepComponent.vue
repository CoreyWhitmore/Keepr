<template>
    <div class="keep-component">
        <div class="card" type="button" data-toggle="modal" :data-target="'#staticBackdrop' + keepProp.id"
            @click="incrementKeepViews">
            <img :src="keepProp.img" class="card-img-top" alt="...">
            <div class="bottom-right">
                {{keepProp.name}}
                <router-link :to="{name: 'Profile', params: {profileId: keepProp.creatorId}}">
                    <img class="img-small ml-2" :src="keepProp.creator.picture" alt="">
                </router-link>
            </div>
        </div>

        <!-- Modal that displays on click -->
        <div class="modal fade" :id="'staticBackdrop' + keepProp.id" tabindex="-1"
            :aria-labelledby="'staticBackdropLabel' + keepProp.id" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-xl">
                <div class="modal-content">
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-6">
                                <img :src="this.keepProp.img" class="card-img" alt="...">
                            </div>
                            <div class="col-6 d-flex flex-column justify-content-between">
                                <div class="row justify-content-end pr-2">
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">X</button>
                                </div>
                                <div>
                                    <p>Views: {{keepProp.views}}</p>
                                    <p>Keeps: {{keepProp.keeps}}</p>
                                </div>
                                <div class="row flex-column text-center">
                                    <h3>{{keepProp.name}}</h3>
                                    <p>{{keepProp.description}}</p>
                                </div>
                                <div class="row justify-content-between align-items-center pr-3">
                                    <div class="dropdown">
                                        <button class="btn btn-secondary dropdown-toggle" type="button"
                                            id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true"
                                            aria-expanded="false">
                                            Add to Vault
                                        </button>
                                        <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                            <button v-for="vault in myVaults" class="dropdown-item" type="button"
                                                @click="addKeepTo(vault)">{{vault.name}}</button>
                                        </div>
                                    </div>
                                    <i class="fa fa-trash" aria-hidden="true"></i>
                                    <h5>
                                        <img class="img-small" :src="keepProp.creator.picture" alt="">
                                        {{keepProp.creator.email}}
                                    </h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: "keep-component",
        props: ["keepProp"],
        mounted() {
            this.$store.dispatch("getMyVaults")
        },
        computed: {
            myVaults() {
                return this.$store.state.myVaults
            }
        },
        methods: {
            incrementKeepViews() {
                this.keepProp.views++
                this.$store.dispatch("editKeep", this.newKeep)
            },
            addKeepTo(vault) {
                let vaultKeep = {
                    vaultId: vault.id,
                    keepId: this.keepProp.id
                }
                this.$store.dispatch("createVaultKeep", vaultKeep)
            }
        }
    };
</script>
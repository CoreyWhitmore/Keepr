<template>
    <div class="vault-component">
        <div class="card">
            <router-link class="navbar-brand d-flex card-img-top"
                :to="{ name: 'Vault', params: {vaultId: vaultProp.id} }">
                <img src="//placehold.it/200x200" class="card-img-top" alt="...">
                <div class="bottom-right">{{vaultProp.name}}</div>
            </router-link>
        </div>
    </div>
</template>

<script>
    export default {
        name: "vault-component",
        props: ["vaultProp"],
    };
</script>
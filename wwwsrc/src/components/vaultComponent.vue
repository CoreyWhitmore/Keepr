<template>
    <div class="vault-component">
        <router-link class="navbar-brand d-flex" :to="{ name: 'Vault', params: {vaultId: vaultProp.id} }">
            <div class="card">
                <img src="//placehold.it/100x100" class="card-img-top" alt="...">
                <div class="bottom-right">{{vaultProp.name}}</div>
            </div>
        </router-link>
    </div>
</template>

<script>
    export default {
        name: "vault-component",
        props: ["vaultProp"],
    };
</script>
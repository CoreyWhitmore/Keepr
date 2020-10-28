<template>
    <div class="keep-component">
        <div class="card" type="button" data-toggle="modal" data-target="#staticBackdrop">
            <img :src="keepProp.img" class="card-img-top" alt="...">
            <div class="bottom-right">{{keepProp.name}}</div>
        </div>

        <!-- Modal that displays on click -->
        <div class="modal fade" id="staticBackdrop" data-backdrop="static" data-keyboard="false" tabindex="-1"
            aria-labelledby="staticBackdropLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-xl">
                <div class="modal-content">
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-6">
                                <img :src="keepProp.img" class="card-img" alt="...">
                            </div>
                            <div class="col-6 d-flex flex-column justify-content-between">
                                <div class="row justify-content-end pr-2">
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">X</button>
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
                                            <button class="dropdown-item" type="button">List of vaults will go
                                                here</button>
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
        methods: {
            keepModal() {

            }
        },
        props: ["keepProp"],
    };
</script>
<template>
<MenuOptions />
<section class="text-center" style="background-color: #f4f4f4;">
    <div class="row">
        <div class="col-lg-6 col-md-8 mx-auto py-2">
            <h1 class="fw-light">Oglasite Vas prostor</h1>
        </div>
    </div>
</section>
<div class="container">
    <div class="row justify-content-md-center">
        <div class="col col-md-8 py-2">
            <form class="row" @submit.prevent="addAd" style="height: 100%;">
                <div class="col-sm-6">
                    <div v-if="this.previewImage"  class="mb-2"  style="width: 300px;">
                        <img :src="this.previewImage" style="max-width: 300px;"/>
                    </div>
                    <div v-else class="mb-2" style="width: 300px; height: 300px; background-color: gray;">
                    </div>
                    <input type="file" @change="handleFileChange" required/>
                </div>
                <div class="col-sm-6">
                    <h5 class="card-title fw-bolder mb-2">
                        <label for="naziv" class="form-label">Naziv prostora</label>
                        <input type="text" v-model="ad.name" class="form-control" id="naziv" required/>
                    </h5>
                    <div class="card-text">
                        <div class="fw-bold mb-2"> <i class="bi bi-geo-alt-fill"></i>
                            <label for="address" class="form-label">Adresa</label>
                            <input type="text" v-model="ad.address" class="form-control" id="address" required/>
                        </div>
                        <div class="fw-bold mb-2"><i class="bi bi-card-text"></i>
                            <label for="inputAddress2" class="form-label">Opis</label>
                            <input type="text" v-model="ad.description" class="form-control" id="inputAddress2" required/>
                        </div>
                        <div class="fw-bold mb-2"> <i class="bi bi-cash"></i>
                            <label for="price" class="form-label">Cena</label>
                            <div style="display: flex;">
                                <input type="text" v-model="ad.pricePerHour" class="form-control" id="price" style="width: 200px;" required/>
                                <div style=" margin-left: 5px;">rsd po satu</div>
                            </div>
                        </div>
                        <div> <button type="submit" class="btn btn-primary" style="float: right;">Dodaj</button></div>
                    </div>
                </div>

            </form>
        </div>
    </div>
</div>
</template>

<script>
import { convertBase64 } from '@/store/shared/convertBase64'
import {
    defineComponent
} from 'vue';

import MenuOptions from "@/components/MenuOptions.vue";

export default defineComponent({
    name: "SpaceAdvertisement",
    components: {
        MenuOptions
    },
    data() {
        return {
            selectedImage: null,
            previewImage: null,
            storedImages: [],
            imageUrl: ''
        }
    },
    methods: {
        async handleFileChange(event) {

            this.selectedImage = event.target.files[0];
            const base64stringImage = await convertBase64(this.selectedImage)

            this.$store.commit('SET_IMAGE', base64stringImage)
            this.previewImage = base64stringImage

        },
        addAd() {
            this.$store.dispatch('addAd')
        },
    },
    computed: {
        ad() {
            return this.$store.state.advertisement.ad
        }
    }

});
</script>

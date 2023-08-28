<template>
    {{ad}}
<div class="container">
    <form class="row g-3" style="margin-top: 40px;">
        <div class="col-md-12">
            <label for="naziv" class="form-label">Naziv prostora</label>
            <input type="text" v-model="ad.name" class="form-control" id="naziv">
        </div>
        <div class="col-12">
            <label for="address" class="form-label">Adresa</label>
            <input type="text" v-model="ad.address" class="form-control" id="address" placeholder="1234 Main St">
        </div>
        <div class="col-12">
            <label for="inputAddress2" class="form-label">Opis</label>
            <input type="text" v-model="ad.description" class="form-control" id="inputAddress2" placeholder="Apartment, studio, or floor">
        </div>
        <div class="col-md-6">
            <label for="inputCity" class="form-label">Slika</label>
            <input type="file" @change="handleFileChange" />
            <img :src="previewImage" v-if="previewImage" style="max-width: 300px;" />
            <div v-for="image in storedImages" :key="image.id">
                <img :src="getImageUrl(image.filename)" style="max-width: 300px;" />
            </div>
        </div>
        <div class="col-12">
            <button @click="addAd" type="button" class="btn btn-primary">Dodaj</button>
        </div>
    </form>
</div>
</template>

<script>
import {
    defineComponent
} from 'vue';

export default defineComponent({
    name: "SpaceAdvertisement",
    created() {
        //this.fetchStoredImages();
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
        handleFileChange(event) {
            this.selectedImage = event.target.files[0];
            this.previewImage = URL.createObjectURL(this.selectedImage);
            const formData = new FormData();
            formData.append('image', this.selectedImage);
            this.$store.commit('SET_IMAGE',formData)
        },
        addAd(){
            this.$store.dispatch('addAd')
        },
       /* async fetchStoredImages() {
            try {
                //const response = await axios.get('/api/images');
                this.storedImages = response.data;
            } catch (error) {
                console.error('Error fetching images:', error);
            }
        },*/
        getImageUrl(filename) {
            return `/api/images/${filename}`;
        },
    },
    computed: {
        ad(){
            return this.$store.state.advertisement.ad
        }
    }

});
</script>

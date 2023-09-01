<template>
<MenuOptions />

<section class="text-center" style="background-color: #f4f4f4;">
    <div class="row">
        <div class="col-lg-6 col-md-8 mx-auto py-2">
            <h1 class="fw-light"> <span style="color: yellow;">Co</span>working Space</h1>
            <p class="lead text-muted">Make work more enjoyable</p>
            <p>
                <router-link :to="{ name: 'SpaceAdvertisement' }"><button type="button" class="btn btn-outline-secondary">Posedujete prostor? Oglasite ga!</button></router-link>
            </p>
        </div>
    </div>
</section>
<div class="container">

    <div class="row row-cols-1 row-cols-md-4 g-8 py-4">
        <div class="col py-2" v-for="ad in advertisements" :key="ad">
            <div class="card" style="height: 100%;">
                <img v-if="ad.image!==null" :src="ad.image" class="card-img-top" alt="...">
                <div class="card-body">
                    <h5 class="card-title fw-bolder">{{ ad.name }}</h5>
                    <div class="card-text">
                        <div class="fw-bold"> <i class="bi bi-geo-alt-fill"></i> {{ ad.address }}</div>
                        <div> {{ ad.description }}</div>
                        <div> Trenutno: {{ ad.isFree? 'slobodan' : 'zauzet' }}</div>
                        <div style="color: gray;"> <i class="bi bi-cash"></i> {{ ad.pricePerHour }} po satu</div>
                    </div>
                </div>
                <div class="card-footer">
                    <button type="button" class="btn btn-outline-secondary">pogledajte detalje</button>
                </div>
            </div>
        </div>
    </div>
</div>
</template>

<script>
import MenuOptions from "@/components/MenuOptions.vue";
export default {
    name: 'CoWorkHome',
    components: {
        MenuOptions
    },
    created() {
        this.$store.dispatch('getUser')
        this.$store.dispatch('getAllAd')
    },
    methods: {

    },
    computed: {
        user() {
            return this.$store.state.users.user
        },
        advertisements() {
            return this.$store.state.advertisement.advertisements
        }
    }
}
</script>

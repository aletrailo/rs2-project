<template>
<MenuOptions />
<div>POCETNA STRANICA</div>
WELCOME {{user.firstName}}

<router-link :to="{name: 'SpaceAdvertisement'}"><button type="button">Dodajte oglas</button></router-link>
SVi oglasi
<div v-for="ad in advertisements" :key="ad" style="border: 1px solid black; border-radius: 4px; margin-top:10px">

        naziv: {{ad.name}}<br>
        adresa: {{ad.address}}<br>
        opis: {{ad.description}}<br>
        slobodan: {{ad.isFree}}<br>
        cenaposatu: {{ad.pricePerHour}}<br>
        slika: <img :src="ad.image" v-if="ad.image" style="max-width: 300px;" />
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
            return this.$store.state.auth.user
        },
        advertisements(){
            return this.$store.state.advertisement.advertisements
        }
    }
}
</script>

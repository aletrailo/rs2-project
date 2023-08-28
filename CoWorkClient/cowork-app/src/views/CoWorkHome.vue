<template>
<MenuOptions />
<div>POCETNA STRANICA</div>
WELCOME {{user.firstName}}

<router-link :to="{name: 'SpaceAdvertisement'}"><button type="button">Dodajte oglas</button></router-link>
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
    },
    computed: {
        user() {
            return this.$store.state.auth.user
        }
    }
}
</script>

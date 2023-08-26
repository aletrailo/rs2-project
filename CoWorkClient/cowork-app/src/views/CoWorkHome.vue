<template>
    <MenuOptions />
    <div>POCETNA STRANICA</div>
    WELCOME {{user.firstName}}
</template>


<script>
import MenuOptions from  "@/components/MenuOptions.vue";
export default {
    name: 'CoWorkHome',
    components: {
        MenuOptions
    },
    created(){
        this.$store.dispatch('auth/getUser')
    },
    computed: {
        user(){
            return this.$store.state.auth.user
        }
    }
}
</script>
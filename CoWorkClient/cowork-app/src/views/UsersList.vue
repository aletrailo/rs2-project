<template>
<MenuOptions />
<div class="container">
    <table class="table mt-5">
        <thead>
            <tr>
                <th scope="col"></th>
                <th scope="col">Username</th>
                <th scope="col">Ime</th>
                <th scope="col">Prezime</th>
                <th scope="col">Email</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="(user, index) in users" :key="user.id">
                <th scope="row">{{index+1}}</th>
                <td>{{user.userName}}</td>
                <td>{{user.firstName}}</td>
                <td>{{user.lastName}}</td>
                <td>{{user.email}}</td>
            </tr>
        </tbody>
    </table>

</div>
</template>

<script>
import {
    defineComponent
} from 'vue';
import MenuOptions from "@/components/MenuOptions.vue";

export default defineComponent({
    name: "UsersList",
    components: {
        MenuOptions
    },
    created() {
        this.$store.dispatch('auth/getAllUsers')
    },
    computed: {
        users() {
            return this.$store.state.auth.users
        }
    }

});
</script>

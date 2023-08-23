<template>
<div class="container mt-5">
    <div class="row justify-content-center">
        <div class="col-md-6">
            <div class="card">
                <div class="card-header">LogIn strana</div>
                <div class="card-body">
                    <form @submit.prevent="login">
                        <div class="mb-3">
                            <label for="username" class="form-label">Username</label>
                            <input v-model="username" type="text" id="username" class="form-control" required />
                        </div>
                        <div class="mb-3">
                            <label for="password" class="form-label">Password</label>
                            <input v-model="password" type="password" id="password" class="form-control" required />
                        </div>
                        <button type="submit" class="btn btn-primary">Login</button>
                    </form>
                    nemate nalog?
                    <button class="btn btn-default"><router-link :to="{ name: 'SingUp'}">KREIRAJ NALOG</router-link></button>
                </div>
            </div>
        </div>
    </div>
</div>
</template>

<script>

export default {
    name: 'LogIn',
    data() {
        return {
            username: 'AdminAdmin123',
            password: 'AdminAdmin123'
        }

    },
    methods: {
        login() {
            this.$store.dispatch('auth/logIn', {
                username: this.username,
                password: this.password
            })
        }
    },
    computed:{
    }

}
</script>

<style scoped>

</style>

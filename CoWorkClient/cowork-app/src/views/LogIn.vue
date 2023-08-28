<template>
<div class="background-container">
    <div class="container mt-5">
        <div class="title-container">
            <h1 class="title">
                Tražite poslovni prostor? Na pravom ste mestu.
            </h1>
            <p class="subtitle">
                Ulogujte se i pronađite savrsen prostor za Vase potrebe!
            </p>
        </div>
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-body">
                        <form @submit.prevent="login">
                            <div class="mb-3">
                                <label for="username" class="form-label"></label>
                                <input v-model="username" type="text" id="username" placeholder="Korisnicko ime" class="form-control" required />
                            </div>
                            <div class="mb-3">
                                <label for="password" class="form-label"></label>
                                <input v-model="password" type="password" id="password" placeholder="Lozinka" class="form-control" required />
                            </div>
                            <button type="submit" class="btn btn-primary">Prijavi se</button>
                        </form>
                        Nemate nalog?
                        <button class="btn btn-default">
                            <router-link :to="{ name: 'SingUp' }" class="text-warning" style="font-weight: bolder;">KREIRAJ
                                NALOG</router-link>
                        </button>
                    </div>
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
            username: '',
            password: ''
        }
    },
    methods: {
        login() {
            this.$store.dispatch('logIn', {
                username: this.username,
                password: this.password
            })
        }
    },
    computed: {}

}
</script>

<style scoped>
.background-container {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-image: url('@/assets/novo.png');
    background-size: 100% 100%;
    z-index: -1;
}

.card {
    background-size: 160%;
    background-position: center;
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%) scale(1);
    text-align: center;
}

.username,
.password {
    display: block;
    margin-bottom: 10px;
    padding: 5px;
}

.btn.btn-primary {
    background-color: #f4d706;
    color: #fff;
    border: none;
    cursor: pointer;
    /*border-radius: 30px; */
    width: 130px;
    height: 45px;
}

.text-warning {
    color: #f4d706;
}

.card-body {
    transform: scale(0.75);
}
</style>

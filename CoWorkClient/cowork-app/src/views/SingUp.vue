<template>
    <div class="background-container">
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-6">
                    <div class="card bg-dark">
                        <div class="card-header custom-bg-warning">Registracija</div>
                        <div class="card-body">
                            <form @submit.prevent="singUp">
                                <div class="mb-3">
                                    <label for="firstName" class="form-label text-white">Ime *</label>
                                    <input v-model="data.firstName" type="text" id="firstName" class="form-control" required />
                                </div>
                                <div class="mb-3">
                                    <label for="lastName" class="form-label text-white">Prezime *</label>
                                    <input v-model="data.lastName" type="text" id="lastName" class="form-control" required />
                                </div>
                                <div class="mb-3">
                                    <label for="userName" class="form-label text-white">Korisničko ime *</label>
                                    <input v-model="data.userName" type="text" id="userName" class="form-control" required />
                                </div>
                                <div class="mb-3">
                                    <label for="password" class="form-label text-white">Lozinka *</label>
                                    <input v-model="data.password" type="password" id="password" class="form-control" placeholder="min duzina 8 karaktera" required />
                                </div>
                                <div class="mb-3">
                                    <label for="email" class="form-label text-white">Email *</label>
                                    <input v-model="data.email" type="text" id="email" class="form-control" required />
                                </div>
                                <div class="mb-3">
                                    <label for="phoneNumber" class="form-label text-white">Broj telefona *</label>
                                    <input v-model="data.phoneNumber" type="text" id="phoneNumber" class="form-control"
                                        required />
                                </div>
                                <button type="submit" class="btn btn-primary custom-bg-warning custom-btn">Kreiraj
                                    nalog</button>
                            </form>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'SingUp',
    methods: {
        singUp() {
            this.$store.dispatch('singUp')
        }
    },
    computed: {
        data(){
            return this.$store.state.singUp.singUpData
        }
    }

}
</script>


<style scoped>

.background-container {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-image: url('@/assets/singup-background.png');
    background-size: 100% 100%;
    z-index: -1;
}
.custom-bg-warning {
    background-color: #f4d706;
    color: #fff;
}

.custom-btn {
    border-color: #f4d706;
    ;
    /* HEX kod za žutu boju */
}
</style>

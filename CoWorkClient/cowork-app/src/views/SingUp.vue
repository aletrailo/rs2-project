<template>
    <div class="background-container">
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-6">
                    <div class="card bg-dark">
                        <div class="card-header custom-bg-warning">Registracija</div>
                        <div class="card-body">
                            <form @submit.prevent="singIn">
                                <div class="mb-3">
                                    <label for="firstName" class="form-label text-white">Ime</label>
                                    <input v-model="firstName" type="text" id="firstName" class="form-control" required />
                                </div>
                                <div class="mb-3">
                                    <label for="lastName" class="form-label text-white">Prezime</label>
                                    <input v-model="lastName" type="text" id="lastName" class="form-control" required />
                                </div>
                                <div class="mb-3">
                                    <label for="userName" class="form-label text-white">Korisnicko ime</label>
                                    <input v-model="userName" type="text" id="userName" class="form-control" required />
                                </div>
                                <div class="mb-3">
                                    <label for="password" class="form-label text-white">Lozinka</label>
                                    <input v-model="password" type="password" id="password" class="form-control" required />
                                </div>
                                <div class="mb-3">
                                    <label for="email" class="form-label text-white">Email</label>
                                    <input v-model="email" type="text" id="email" class="form-control" required />
                                </div>
                                <div class="mb-3">
                                    <label for="phoneNumber" class="form-label text-white">Broj telefona</label>
                                    <input v-model="phoneNumber" type="text" id="phoneNumber" class="form-control"
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
    data() {
        return {
            firstName: "",
            lastName: "",
            userName: "",
            password: "",
            email: "",
            phoneNumber: ""
        }
    },
    methods: {

        singIn() {
            this.$store.dispatch('auth/singIn', {
                firstName: this.firstName,
                lastName: this.lastName,
                userName: this.userName,
                password: this.password,
                email: this.email,
                phoneNumber: this.phoneNumber
            })
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
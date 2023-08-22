<template>
<div class="container mt-5">
    <div class="row justify-content-center">
        <div class="col-md-6">
            <div class="card">
                <div class="card-header">Registracija</div>
                <div class="card-body">
                    <form @submit.prevent="singIn">
                        <div class="mb-3">
                            <label for="firstName" class="form-label">Ime</label>
                            <input v-model="firstName" type="text" id="firstName" class="form-control" required />
                        </div>
                        <div class="mb-3">
                            <label for="lastName" class="form-label">Prezime</label>
                            <input v-model="lastName" type="text" id="lastName" class="form-control" required />
                        </div>
                        <div class="mb-3">
                            <label for="userNeme" class="form-label">Korisnicko ime</label>
                            <input v-model="userNeme" type="text" id="userNeme" class="form-control" required />
                        </div>
                        <div class="mb-3">
                            <label for="password" class="form-label">Lozinka</label>
                            <input v-model="password" type="password" id="password" class="form-control" required />
                        </div>
                        <div class="mb-3">
                            <label for="email" class="form-label">Email</label>
                            <input v-model="email" type="text" id="email" class="form-control" required />
                        </div>
                        <div class="mb-3">
                            <label for="phoneNumber" class="form-label">Broj telefona</label>
                            <input v-model="phoneNumber" type="text" id="phoneNumber" class="form-control" required />
                        </div>
                        <button type="submit" class="btn btn-primary">Kreiraj nalog</button>
                    </form>
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
            userNeme: "",
            password: "",
            email: "",
            phoneNumber: ""
        }
    },
    methods: {
        singIn() {
            this.$store.dispatch('singIn', {
                firstName: this.firstName,
                lastName: this.lastName,
                userNeme: this.userNeme,
                password: this.password,
                email: this.email,
                phoneNumber: this.phoneNumber
            })
        }
    }

}
</script>

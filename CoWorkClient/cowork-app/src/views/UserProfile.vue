<template>
<MenuOptions />
<div class="container" style="padding-top: 40px;">

    <h3>Moj profil</h3>
    <div style="border: 1px solid gray; border-radius: 4px; padding: 10px;">
        <div class="mb-3 row">
            <label class="col-sm-2 col-form-label">Korisnicko ime</label>
            <div class="col-sm-10">
                {{user.userName}}
            </div>
        </div>
        <div class="mb-3 row">
            <label class="col-sm-2 col-form-label">Ime</label>
            <div class="col-sm-10">
                {{user.firstName}}
            </div>
        </div>
        <div class="mb-3 row">
            <label class="col-sm-2 col-form-label">Prezime</label>
            <div class="col-sm-10">
                {{user.lastName}}
            </div>
        </div>
        <div class="mb-3 row">
            <label class="col-sm-2 col-form-label">Email</label>
            <div class="col-sm-10">
                {{user.email}}
            </div>
        </div>
        <div class="mb-3 row">
            <label class="col-sm-2 col-form-label">Moji oglasi</label>
            <div class="col-sm-10">
              OGLASI
            </div>
        </div>
    </div>
</div>
</template>

<script>
import {
    defineComponent
} from 'vue';
import MenuOptions from "@/components/MenuOptions.vue";

export default defineComponent({
    name: "UserProfile",
    components: {
        MenuOptions
    },
    created() {
        this.$store.dispatch('auth/getUser');
    },
    computed: {
        user() {
            return this.$store.state.auth.user
        }
    }

});
</script>

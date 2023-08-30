<template>
<MenuOptions />
<section class="text-center" style="background-color: #f4f4f4;">
    <div class="row">
        <div class="col-lg-6 col-md-8 mx-auto py-2">
            <h1 class="fw-light">Moj profil</h1>
        </div>
    </div>
</section>
<div class="container" style="padding-top: 40px;">
    <div class="row  justify-content-md-center">
        <div class="col-4">
            <h3>Licni podaci</h3>
            <div style="border-radius: 4px; padding: 10px; background-color: #f1eeea;">
                <div class="mb-3 row">
                    <div class="col-sm-4" style="font-weight: lighter;">Korisnicko ime</div>
                    <div class="col-sm-8">
                        {{user.userName}}
                    </div>
                </div>
                <div class="mb-3 row">
                    <div class="col-sm-4"  style="font-weight: lighter;">Ime</div>
                    <div class="col-sm-8">
                        {{user.firstName}}
                    </div>
                </div>
                <div class="mb-3 row">
                    <div class="col-sm-4" style="font-weight: lighter;">Prezime</div>
                    <div class="col-sm-8">
                        {{user.lastName}}
                    </div>
                </div>
                <div class="mb-3 row">
                    <div class="col-sm-4"  style="font-weight: lighter;">Email</div>
                    <div class="col-sm-8">
                        {{user.email}}
                    </div>
                </div>
            </div>
        </div>
        <div class="col-4">
            <h3>Moji oglasi</h3>
            <div style="border-radius: 4px; padding: 10px; background-color: #e9ecec;">
            <ListSpaces />
            </div>

        </div>
        <div class="col-4">
            <h3>Prostori koji koristim</h3>
            <div style="border-radius: 4px; padding: 10px; background-color: #f2ede9;">
                <ReservedByMe />
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
import ListSpaces from "@/components/ListSpaces.vue";
import ReservedByMe from "@/components/ReservedByMe.vue";

export default defineComponent({
    name: "UserProfile",
    components: {
        MenuOptions,
        ListSpaces,
        ReservedByMe
    },
    created() {
        this.$store.dispatch('getUser');
    },
    computed: {
        user() {
            return this.$store.state.auth.user
        }
    }

});
</script>

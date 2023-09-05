<template>
<MenuOptions />
<section class="text-center" style="background-color: #f4f4f4;">
    <div class="row">
        <div class="col-lg-6 col-md-8 mx-auto py-2">
            <h1 class="fw-light">Spisak svih korisnika</h1>
        </div>
    </div>
</section>
<div class="container pt-5">
    <div class="row">
        <div class="col-2"></div>
        <div class="col-8">
            <div style="display:flex;">
                <div class="search"><i class="bi bi-search"></i></div> <input style="border-radius: 0px 4px 4px 0px;" class="form-control" placeholder="Pretraga" v-model="this.query">
             </div>
        </div>
    </div>
    <div class="row">
        <div class="col-2">
        </div>
        <div class="col-8">
            <table class="table table-striped mt-5">
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
                        <td v-html="highlightWord(user.userName)"></td>
                        <td v-html="highlightWord(user.firstName)"></td>
                        <td v-html="highlightWord(user.lastName)"></td>
                        <td v-html="highlightWord(user.email)"></td>
                    </tr>
                </tbody>
            </table>
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
    name: "UsersList",
    components: {
        MenuOptions
    },
    data(){
        return {
            query: '' 
        }
    },
    methods: {
        highlightWord(text) {
            const regex = new RegExp(this.query, "gi");
            return text.replace(regex, function (str) {
                return "<b>" + str + "</b>"
            });
        }
    },
    created() {
        this.$store.dispatch('getAllUsers')
    },
    computed: {
        users() {
            return this.$store.state.users.users.filter((user) =>{
                for (const i in user) {
                    if (user[i].toString().toLowerCase().indexOf(this.query.toLowerCase()) >= 0)
                        return true
                }
                return false
            })
        
        }
    }

});
</script>

<style scoped>
.search {
    background-color: #dddddd;
    text-align: center;
    padding: 5px 10px;
    border-radius: 4px 0px 0px 4px;
}
</style>

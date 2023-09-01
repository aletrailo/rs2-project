<template>
<MenuOptions />
<section class="text-center" style="background-color: #f4f4f4;">
    <div class="row">
        <div class="col-lg-6 col-md-8 mx-auto py-2">
            <h1 class="fw-light">Informacije o poslovnom prostoru</h1>
        </div>
    </div>
</section>
<div class="container">

    <div class="row justify-content-md-center">
        <div class="col col-md-8 py-2">
            <div class="row">
                <div class="col-sm-6" style="height: 100%;margin-top: 20px;">
                    <img :src="space.image" class="card-img-top" alt="...">
                </div>
                <div class="col-sm-6">
                    <div class="card-body">
                        <h5 class="card-title fw-bolder">{{ space.name }}</h5>
                        <div class="card-text">
                            <div class="fw-bold"> <i class="bi bi-geo-alt-fill"></i> {{ space.address }}</div>
                            <div> {{ space.description }}</div>
                            <div> Trenutno: {{ space.isFree? 'slobodan' : 'zauzet' }}</div>
                            <div style="color: gray;"> <i class="bi bi-cash"></i> {{ space.pricePerHour }} po satu</div>
                            <div>Oglas postavio:  {{space.owner}}</div>
                            <div v-if="!space.isFree">Oglas rezervisao:  {{space.reservedBy}}</div>
                        </div>
                    </div>
                    <div class="card-footer" v-if="space.isFree">
                        <button @click="bookSpace" type="button" class="btn btn-warning" style="width: 100%; color: white;">Rezervisi</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</template>

<script>
import MenuOptions from "@/components/MenuOptions.vue";
export default {
    name: 'AdInfo',
    components: {
        MenuOptions
    },
    created() {
        const id = this.$route.params.id
        this.$store.dispatch('getAddById', {
            id: id
        })
    },
    methods: {
        bookSpace() {
            this.$store.dispatch('bookSpace', {
                id: this.space.id
            })
        }
    },
    computed: {
        space() {
            return this.$store.state.spaces.space
        }
    }

}
</script>

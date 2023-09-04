<template>
<MenuOptions />
<section class="text-center" style="background-color: #f4f4f4;">
    <div class="row">
        <div class="col-lg-6 col-md-8 mx-auto py-2">
            <h1 class="fw-light"> Co<span style="color: #b4b4b4;">work</span> Space</h1>
            <p class="lead text-muted">Make work more enjoyable</p>
            <p>
                <router-link :to="{ name: 'SpaceAdvertisement' }"><button type="button" class="btn btn-outline-secondary">Posedujete prostor? Oglasite ga!</button></router-link>
            </p>
        </div>
    </div>
</section>
<section class="text-center" style="background-color: gainsboro; padding: 10px; display: flex;justify-content: center;">
    <div>
        <div><b>Cena do: {{this.rangeValue}} rsd</b></div>
        <div>
            <span>{{min_price}} rsd</span>
            <input type="range" :min="min_price" :max="max_price" v-model="this.rangeValue" step="1" />
            <span>{{max_price}} rsd</span>
        </div>
    </div>
    <div style="width: 20px; border-right: 1px solid gray;"></div>
    <div style="width: 20px;"></div>
    <div>
        <div><b>Prikazi samo slobodne prostore?</b></div>
        <div><input type="checkbox" v-model="onlyFree" style="display: block;margin: auto;height: 20px;width: 20px;" />
        </div>
    </div>
</section>
<div class="container">
    <div class="row row-cols-1 row-cols-md-4 g-8 py-4">
        <div class="col py-2" v-for="ad in filter_advertisements" :key="ad">
            <div class="card" style="height: 100%;">
                <img v-if="ad.image!==null" :src="ad.image" class="card-img-top" alt="...">
                <div class="card-body">
                    <h5 class="card-title fw-bolder">{{ ad.name }}</h5>
                    <div class="card-text">
                        <div class="fw-bold"> <i class="bi bi-geo-alt-fill"></i> {{ ad.address }}</div>
                        <div> {{ ad.description }}</div>
                        <div :style="{'color': ad.isFree? 'green' : 'red'}"> Trenutno: {{ ad.isFree? 'slobodan' : 'zauzet' }}</div>
                        <div style="color: gray;"> <i class="bi bi-cash"></i> {{ ad.pricePerHour }} po satu</div>
                    </div>
                </div>
                <div class="card-footer">
                    <router-link :to="{name: 'AdInfo', params: {id:ad.spaceId  }}"><button type="button" class="btn btn-outline-secondary">pogledajte detalje</button></router-link>
                </div>
            </div>
        </div>
    </div>
</div>
</template>

<script>
import MenuOptions from "@/components/MenuOptions.vue";
export default {
    name: 'CoWorkHome',
    components: {
        MenuOptions
    },
    data() {
        return {
            rangeValue: null,
            onlyFree: false
        }
    },
    created() {
        this.$store.dispatch('getUser')
        this.$store.dispatch('getAllAd')
    },
    computed: {
        user() {
            return this.$store.state.users.user
        },
        advertisements() {
            return this.$store.state.advertisement.advertisements
        },
        min_price() {
            return Math.min(...this.advertisements.map(ad => ad.pricePerHour))
        },
        max_price() {
            return  Math.max(...this.advertisements.map(ad => ad.pricePerHour))
        },
        filter_advertisements() {
            let ads= this.$store.state.advertisement.advertisements.filter(ad => this.onlyFree ? ad.isFree : true)
            if(this.rangeValue !== null && this.rangeValue >= 0){
                ads= ads.filter(ad => {
                if (ad.pricePerHour <= this.rangeValue)
                    return true
                return false
                })
            }
            return ads
        }
    },
    watch: {
        max_price: {
            handler: function (newV, oldV) {
                if(oldV <=0 && newV >0 && this.rangeValue === null) {
                   this.rangeValue = newV
                }
            },
            deep: true
        },
    }
}
</script>

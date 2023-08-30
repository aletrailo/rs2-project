<template>
<div class="col py-2" v-for="space in spaces" :key="space.spaceId">
    <div class="card" style="height: 100%;">
        <img v-if="space.image!==null" :src="space.image" class="card-img-top" alt="...">
        <div class="card-body">
            <h5 class="card-title fw-bolder">{{ space.name }}</h5>
            <div class="card-text">
                <div class="fw-bold"> <i class="bi bi-geo-alt-fill"></i> {{ space.address }}</div>
                <div> {{ space.description }}</div>
                <div> Trenutno: {{ space.isFree? 'slobodan' : 'zauzet' }}</div>
                <div style="color: gray;"> <i class="bi bi-cash"></i> {{ space.pricePerHour }} po satu</div>
            </div>
        </div>
        <div class="card-footer">
            <button type="button" class="btn btn-outline-secondary">Izmeni</button>
        </div>
    </div>
</div>
</template>

<script>
import {
    defineComponent
} from 'vue';

export default defineComponent({
    name: "ListSpaces",

    created() {
        this.$store.dispatch('getMySpaces')
    },
    computed:{
        spaces(){
            return this.$store.state.spaces.spaces
        }
    }
})
</script>

<template>
<div v-if="reservedByMe.length">
    <div class="col py-2" v-for="space in reservedByMe" :key="space.spaceId">
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
                DUGME OTKAZI?
            </div>
        </div>
    </div>
</div>
<div v-else>
    Ne protoji prostor koji ste vi rezervisali za koriscenje
</div>
</template>

<script>
import {
    defineComponent
} from 'vue';

export default defineComponent({
    name: "ListSpaces",

    created() {
        this.$store.dispatch('getAllReservedBy')
    },
    computed: {
        reservedByMe() {
            return this.$store.state.spaces.reservedByMe
        }
    }
})
</script>

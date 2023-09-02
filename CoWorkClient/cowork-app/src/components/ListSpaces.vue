<template>
<EditModal v-if="modalVisible" :ad="selectedAd" @close="closeModal" @save="saveChanges"/>
<div v-if="spaces.length">
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
                <button type="button"  @click="openModal(space)" class="btn btn-outline-secondary">Detaljnije/Izmena</button>
                <button type="button" class="btn  btn-danger" @click="deleteSpace(space.id)" style="float: right;">Obrisi</button>
            </div>
        </div>
    </div>
</div>
<div v-else>
    Nema prostora koji vi oglasavate
</div>
</template>

<script>
import {
    defineComponent
} from 'vue';
import EditModal from '@/components/EditModal.vue'
export default defineComponent({
    name: "ListSpaces",
    components: {
        EditModal
    },
    data(){
        return {
            modalVisible: false,
            selectedAd: null
        }
    },
    created() {
        this.$store.dispatch('getMySpaces')
    },
    methods: {
        deleteSpace(spaceId) {
            this.$store.dispatch('deleteSpace', {
                spaceId: spaceId
            })
        },
        openModal(object) {
            this.selectedAd = object;
            this.modalVisible = true;
        },
        closeModal() {
            this.modalVisible = false;
        },
        saveChanges(editedAd) {
            this.modalVisible = false;
            this.$store.dispatch('editSpace', {space: editedAd})
        },
    },
    computed: {
        spaces() {
            return this.$store.state.spaces.spaces
        },
    }
})
</script>

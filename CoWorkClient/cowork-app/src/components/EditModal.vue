<template>
<div class="modal" tabindex="-1" role="dialog" style="display: block; background-color: #808080bf;">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="fw-bold">{{this.edit? 'Izmena' : 'Pregled'}}</h5>
                <span>
                    <button v-if="!this.edit" type="button" class="btn"><i @click="this.edit=!this.edit" class="bi bi-pen"></i></button>
                    <button type="button" class="btn-close" @click="closeModal" style="padding: 0; margin:0"></button>
                </span>
            </div>
            <div class="modal-body">
                <div class="card-text">
                    <div style="display: flex; margin-bottom: 10px;">
                        <span class="fw-bold" style="width: 80px;">Slika:</span>
                        <div>
                            <div v-if="!this.edit" style="width: 310px;">
                                <img :src="ad.image" style="width: 300px;" />
                            </div>
                            <div v-else>
                                <div v-if="this.previewImage" class="mb-2" style="width: 310px; max-height: 300px;overflow: scroll;">
                                    <img :src="this.previewImage" style="max-width: 300px;" />
                                </div>
                                <input type="file" @change="handleFileChange" />
                            </div>
                        </div>
                    </div>
                    <div style="display: flex;margin-bottom: 10px;">
                        <span class="fw-bold" style="width: 80px;">Naziv:</span>
                        <div v-if="!this.edit">{{ad.name}}</div>
                        <div v-else><input class="form-control" v-model="editedAd.name" type="text"></div>
                    </div>
                    <div style="display: flex;margin-bottom: 10px;">
                        <span class="fw-bold" style="width: 80px;">Adresa<i class="bi bi-geo-alt-fill"></i>:</span>
                        <div v-if="!this.edit" class="fst-normal">  {{ ad.address }}</div>
                        <div v-else><input class="form-control" v-model="editedAd.address" type="text"></div>
                    </div>
                    <div style="display: flex;margin-bottom: 10px;">
                        <span class="fw-bold" style="width: 80px;">Opis:</span>
                        <div v-if="!this.edit"> {{ ad.description }}</div>
                        <div v-else><input class="form-control" v-model="editedAd.description" type="text"></div>
                    </div>
                    <div style="display: flex;margin-bottom: 10px;">
                        <span class="fw-bold" style="width: 80px;">Status:</span>
                        <div> {{ ad.isFree? 'slobodan' : 'zauzet' }}</div>
                    </div>
                    <div style="display: flex;margin-bottom: 10px;">
                        <span class="fw-bold" style="width: 80px;">Cena <i class="bi bi-cash"></i>:</span>
                        <div v-if="!this.edit" style="color: gray;"> {{ ad.pricePerHour }} po satu</div>
                        <div v-else><input class="form-control" type="text" v-model="editedAd.pricePerHour" style="width:80px;display: inline; margin-right: 5px;" />po satu</div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" @click="closeModal">Odustani</button>
                <button type="button" class="btn btn-primary" @click="saveChanges">Sacuvaj izmene</button>
            </div>
        </div>
    </div>
</div>
</template>

<script>
import {
    convertBase64
} from '@/store/shared/convertBase64'

export default {
    props: {
        ad: Object,
    },
    data() {
        return {
            editedAd: {
                ...this.ad
            },
            previewImage: {
                ...this.ad
            }.image,
            edit: false
        };
    },
    methods: {
        async handleFileChange(event) {

            this.selectedImage = event.target.files[0];
            const base64stringImage = await convertBase64(this.selectedImage)

            this.editedAd.image = base64stringImage
            this.previewImage = base64stringImage

        },
        closeModal() {
            this.$emit('close');
        },
        saveChanges() {
            this.$emit('save', this.editedAd);
        },
    },
};
</script>

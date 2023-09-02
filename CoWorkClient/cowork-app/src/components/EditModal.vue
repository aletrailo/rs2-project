<template>
<div class="modal" tabindex="-1" role="dialog" style="display: block; background-color: #808080bf;">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">{{this.edit? 'Izmena' : 'Pregled'}}</h5>
                <span>
                    <button v-if="!this.edit" type="button" class="btn"><i @click="this.edit=!this.edit" class="bi bi-pen"></i></button>
                    <button type="button" class="btn-close" @click="closeModal" style="padding: 0; margin:0"></button>
                </span>
            </div>
            <div class="modal-body">
                <div class="card-text">
                    <div style="display: flex; margin-bottom: 10px;">
                        <span style="width: 80px;">slika</span>
                        <div style="width: 310px; max-height: 300px;overflow: scroll;">
                        <div v-if="!this.edit"><img :src="ad.image" style="width: 300px;" /></div>
                        <div v-else>
                            <div v-if="this.previewImage"  class="mb-2"  style="width: 300px;">
                                <img :src="this.previewImage" style="max-width: 300px;"/>
                            </div>
                            <input type="file" @change="handleFileChange"/>
                        </div>
                        </div>
                    </div>
                    <div style="display: flex;margin-bottom: 10px;">
                        <span style="width: 80px;">Naziv:</span>
                        <div v-if="!this.edit">{{ad.name}}</div>
                        <div v-else><input class="form-control" v-model="editedAd.name" type="text"></div>
                    </div>
                    <div style="display: flex;margin-bottom: 10px;">
                        <span style="width: 80px;">Adresa:</span>
                        <div v-if="!this.edit" class="fw-bold"> <i class="bi bi-geo-alt-fill"></i> {{ ad.address }}</div>
                        <div v-else><input class="form-control" v-model="editedAd.address" type="text"></div>
                    </div>
                    <div style="display: flex;margin-bottom: 10px;">
                        <span style="width: 80px;">Opis:</span>
                        <div v-if="!this.edit"> {{ ad.description }}</div>
                        <div v-else><input class="form-control" v-model="editedAd.description" type="text"></div>
                    </div>
                    <div style="display: flex;margin-bottom: 10px;">
                        <span style="width: 80px;">Status:</span>
                        <div> {{ ad.isFree? 'slobodan' : 'zauzet' }}</div>
                    </div>
                    <div style="display: flex;margin-bottom: 10px;">
                        <span style="width: 80px;">Cena <i class="bi bi-cash"></i>:</span>
                    <div v-if="!this.edit" style="color: gray;"> {{ ad.pricePerHour }} po satu</div>
                    <div v-else><input class="form-control" type="text" v-model="editedAd.pricePerHour" style="width:80px;display: inline; margin-right: 5px;"/>po satu</div>
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
const convertBase64 = (file) => {
    return new Promise((resolve, reject) => {
        const fileReader = new FileReader();
        fileReader.readAsDataURL(file);

        fileReader.onload = () => {
            resolve(fileReader.result);
        };

        fileReader.onerror = (error) => {
            reject(error);
        };
    });
};
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

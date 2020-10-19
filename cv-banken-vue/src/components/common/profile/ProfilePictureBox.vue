<template>
<section>
  <figure class="image is-4by4">
    <div></div>
      <img class="" :src="profilePicture || require('@/assets/stockPhoto.jpg')" alt="Profilbild" @click="pictureClicked">
  </figure>
  <b-modal
      v-model="isShowing"
      :destroy-on-hide="false"
      aria-modal
      aria-role="dialog"
      has-modal-card
      trap-focus class="is-8">
    <div class="modal-card">
      <header class="modal-card-head" style="background-color: #693250; color:white; ">
        <p class="modal-card-title" style="color: white">Profilbild</p>
        <button
            class="delete"
            type="button"
            @click="isShowing= false"/>
      </header>
      <section class="modal-card-body">
        <b-field class="detail-container">
          <b-field class="file is-primary" :class="{'has-name': !!file}">
            <b-upload v-model="file" class="file-label">
            <span class="file-cta">
                <b-icon class="file-icon" icon="upload"></b-icon>
                <span class="file-label">Ladda upp</span>
            </span>

            </b-upload>
            <span class="file-name" v-if="file">
                {{ file.name }}
            </span>
            <b-button label="Ladda upp" @click="upload">
            </b-button>
          </b-field>
        </b-field>
      </section>

    </div>

  </b-modal>
</section>
</template>

<script>

export default {
  name: "ProfilePictureBox",
  props: {profilePicture: String, canEdit: Boolean, onReload: Function},
  data() {
    return {
      picture: String,
      file: {},
      isShowing: false,
    };
  },
  computed:{
  },
  created() {
  },
  methods: {
    pictureClicked(){
      this.isShowing = true
    },
    async upload(){
      if (this.file !== null && this.file.name !== undefined){
        let formData = new FormData();
        formData.append("image", this.file)
        await this.$store.dispatch("profile/updateProfilePicture", formData).then(
          this.isShowing = false,
          this.onReload()
        ).catch(err => {
          alert(err)
        })
      }
    }
  }
}
</script>

<style scoped>
.profilePicture:hover .pictureOverlay{
  opacity: 1;
}
.pictureOverlay {
  position: absolute;
  bottom: 0;
  background: rgb(0, 0, 0);
  background: rgba(0, 0, 0, 0.5); /* Black see-through */
  color: #f1f1f1;
  width: 100%;
  transition: .5s ease;
  opacity:0;
  color: white;
  font-size: 20px;
  padding: 20px;
  text-align: center;
}

</style>
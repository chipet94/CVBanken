<template>
  <section>
    <figure class="image is-4by4">
      <div class="columns is-centered is-vcentered">
        <div class="column">
          <img :src="profilePicture || require('@/assets/stockPhoto.jpg')" alt="Profilbild" class="profilePicture"
               @click="pictureClicked">
        </div>
        <div class="column is-overlay">

          <div class="imageOverlay" @click="pictureClicked">

          </div>
          <b-button v-if="canEdit" class="is-pulled-right editButton" @click="editPicture">
            <b-icon id="editPictureIcon" icon="cog" style="color:black"></b-icon>
          </b-button>
        </div>
      </div>
    </figure>
    <b-modal
        v-if="canEdit"
        v-model="isEditing"
        :destroy-on-hide="false"
        aria-modal
        aria-role="dialog"
        class="is-8"
        has-modal-card trap-focus>
      <div class="modal-card">
        <header class="modal-card-head" style="background-color: #693250; color:white; ">
          <p class="modal-card-title" style="color: white">Profilbild</p>
          <button
              class="delete"
              type="button"
              @click="isEditing= false"/>
        </header>
        <section class="modal-card-body">
          <span class="is-small has-text-danger">allowed files: {{ supportedExt }}</span><br>
          <div v-if="fileUrl !== null">
            <img :src="fileUrl">
          </div>
          <b-field class="detail-container">
            <b-field :class="{'has-name': !!file}" class="file is-primary">
              <div class="columns">
                <div class="column">
                  <b-upload v-model="file" class="file-label" @input="previewImage">
                    <span class="file-cta">
                        <b-icon class="file-icon" icon="upload"></b-icon>
                        <span class="file-label">Välj fil</span>
                    </span>
                  </b-upload>
                </div>
                <div class="column">
                <span v-if="file" class="file-name">
                    {{ file.name }}
                </span>
                </div>
                <div class="column">
                  <b-button v-if="file.name" label="Ladda upp" @click="upload">
                  </b-button>
                </div>
              </div>
            </b-field>
          </b-field>
        </section>

      </div>

    </b-modal>
    <b-modal v-model="isShowing">
      <img :src="profilePicture || require('@/assets/stockPhoto.jpg')" alt="Profilbild" class="">
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
      supportedExt: [".jpg", ".jpeg", ".png"],
      fileUrl: null,
      isShowing: false,
      isEditing: false
    };
  },
  computed: {},
  created() {
  },
  methods: {
    pictureClicked() {
      this.isShowing = true;
    },
    editPicture() {
      this.isEditing = true
    },
    previewImage(e) {
      console.log(e)
      this.fileUrl = URL.createObjectURL(e);
    },
    async upload() {
      console.log(this.file)
      if (this.file !== null && this.file.name !== undefined) {
        let formData = new FormData();
        formData.append("image", this.file)
        await this.$store.dispatch("profile/updateProfilePicture", formData).then(
            this.isEditing = false,
            this.onReload()
        ).catch(err => {
          console.log(err)
          this.file = {}
          alert(err.data)
        })
      }
    }
  }
}
</script>

<style scoped>
.columns.is-vcentered {
  -webkit-box-align: center;
  -ms-flex-align: center;
  align-items: center;
}

.profilePicture {
  position: relative;
  height: 100%;
  width: 100%;
}

.imageOverlay {
  position: absolute;
  opacity: 0;
  background-color: rgba(22, 22, 22, 0.1);
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
}

.editButton {
  opacity: 0;
}

.column:hover .editButton {
  opacity: 1;
}

.column:hover .imageOverlay {
  opacity: 1;
  background-color: rgba(22, 22, 22, 0.4);
}
</style>
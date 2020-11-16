<template>
  <section>
    <figure class="image is-4by4">
      <div class="columns is-centered is-vcentered">
        <div class="column">
          <img :src="profilePicture || require('@/assets/stockPhoto.jpg')" alt="Profilbild" class="profilePicture"
               @click="pictureClicked">
        </div>
        <div class="column is-overlay">
          <div class="imageOverlay" @click="pictureClicked"></div>
          <b-button v-if="canEdit" class="is-pulled-right editButton ITHS-button-small" @click="editPicture">
            <b-icon id="editPictureIcon" icon="cog" style=" "></b-icon>
          </b-button>
        </div>
      </div>
    </figure>
  </section>
</template>

<script>

import ViewProfilePictureModal from "@/components/common/profile/ViewProfilePictureModal";
import EditPictureModal from "@/components/common/profile/EditPictureModal";

export default {
  name: "ProfilePictureBox",
  components: {},
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
      this.$buefy.modal.open({
        parent: this.$parent,
        component: ViewProfilePictureModal,
        props: {profilePicture: this.profilePicture},
        hasModalCard: true,
        customClass: '',
        trapFocus: true
      })
    },
    editPicture() {
      this.$buefy.modal.open({
        parent: this.$parent,
        component: EditPictureModal,
        hasModalCard: true,
        customClass: '',
        trapFocus: true
      })
    },
    previewImage(e) {
      this.fileUrl = URL.createObjectURL(e);
    },
    async upload() {
      if (this.file !== null && this.file.name !== undefined) {
        let formData = new FormData();
        formData.append("image", this.file)
        await this.$store.dispatch("profile/updateProfilePicture", formData).then(
            this.isEditing = false,
            this.onReload()
        ).catch(err => {
          this.file = {}
          this.$buefy.toast.open({
            message: err.data,
            type: 'is-danger'
          })
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
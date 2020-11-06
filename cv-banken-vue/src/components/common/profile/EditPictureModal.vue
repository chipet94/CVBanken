<template>
  <div class="modal-card">
    <header class="modal-card-head" style="background-color: #693250; color:white; ">
      <p class="modal-card-title" style="color: white">Profilbild</p>
      <button
          class="delete"
          type="button"
          @click="$emit('close')"/>
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
</template>

<script>
export default {
  name: "EditPictureModal",
  data() {
    return {
      picture: String,
      file: {},
      supportedExt: [".jpg", ".jpeg", ".png"],
      fileUrl: null,
    };
  },
  methods: {
    previewImage(e) {
      this.fileUrl = URL.createObjectURL(e);
    },
    async upload() {
      if (this.file !== null && this.file.name !== undefined) {
        let formData = new FormData();
        formData.append("image", this.file)
        await this.$store.dispatch("profile/updateProfilePicture", formData).then(
            this.$emit("close")
        ).catch(err => {
          this.file = {}
          alert(err.data)
        })
      }
    }
  }
}
</script>

<style scoped>

</style>
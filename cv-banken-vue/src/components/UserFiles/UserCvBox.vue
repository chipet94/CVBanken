<template>
  <div class="container ITHS-content">
      <div class="header ITHS-list-header">
        <p class="">CV</p>
      </div>
      <div class="content">
            <div v-if="!user.cv && canEdit">
              <b-upload v-model="file" accept="" class="file-label">
                    <span class="file-cta ITHS-button-small">
                        <b-icon class="file-icon" icon="select"></b-icon>
                        <span class="file-label">Välj fil</span>
                    </span>
                <span v-if="file" class="file-name">
                        {{ file.name }}
                  <button class="delete is-small has-background-danger"
                          type="button"
                          @click="file = null">
                  </button>
                    </span>
              </b-upload>
              <b-button v-if="file" class="ITHS-button-small" @click="UploadCv" title="Ladda upp">
                <span class="is-horizontal">

                  <b-icon class="file-icon" icon="upload"></b-icon>
                </span>
              </b-button>
            </div>
            <div v-if="user.cv" class="columns">
              <div class="column">
                {{ user.cv.name }}
              </div>
              <div class="column">
                {{ user.cv.uploaded }}
              </div>
              <div class="column">
                <b-button v-if="canEdit" class="is-danger" @click="Remove" title="Tabort">
                  <b-icon class="file-icon" icon="delete"></b-icon>
                </b-button>
                <b-button class="ITHS-button-small" @click="handleDownload" title="Ladda ner">
                  <b-icon class="file-icon" icon="download"></b-icon>
                </b-button>

              </div>
            </div>
        <span v-if="!user.cv && !canEdit" class="has-text-danger">Användaren har inget CV.</span>
      </div>
  </div>
</template>

<script>
export default {
  name: "UserCvBox",
  props: {user: {}, canEdit: Boolean},
  data() {
    return {
      file: null
    }
  },
  methods: {
    async UploadCv() {
      let formData = new FormData();
      formData.append('file', this.file);
      await this.$store.dispatch("files/SetCV", formData).then(
          res => {
            console.log(res)
            this.$store.dispatch("profile/getByUrl", this.user.url).then(console.log("updated"))
            this.$buefy.toast.open({
              message: 'CV uppladdat!',
              type: 'is-success'
            })
          }
      ).catch(err => {

        this.loading = false;
        this.$buefy.toast.open({
          message: err.response.data,
          type: 'is-danger'
        })
      })
    },
    StartDownload(response, filename) {
      const url = window.URL.createObjectURL(new Blob([response]))
      const downloadLink = document.createElement('a')
      downloadLink.href = url
      downloadLink.setAttribute('download', filename) //or any other extension
      document.body.appendChild(downloadLink)
      downloadLink.click()
    },
    async handleDownload() {
      await this.$store.dispatch("files/downloadCv", this.user.cv.id).then(
          res => {
            this.StartDownload(res, this.user.cv.name)
          }
      ).catch(() => {
        this.$buefy.toast.open({
          message: "Något gick fel, gick inte att hämta...",
          type: 'is-danger'
        })
      })

    },
    async Remove() {
      let confirmed = confirm("Remove file '" + this.user.cv.name + "'?")
      if (confirmed) {
        await this.$store.dispatch("files/removeCv", this.user.cv.id).then(
            res => {
              this.user.cv = null
              console.log(res)
            }
        ).catch(err => {
          this.$buefy.toast.open({
            message: "Något gick fel, gick inte att hämta..." + err,
            type: 'is-danger'
          })
        })
      }
    },
  }
}
</script>

<style scoped>
.columns{
  font-weight: bold;
  font-size: medium;
}
.columns:hover{
  background-color: rgba(0,0,0,0.2);
}
.ITHS-content{
  margin-bottom: 2rem;
}

</style>
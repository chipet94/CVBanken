<template>
  <section>
      <UserCvBox :can-edit="canEdit" :user="user"></UserCvBox>
      <div class="container ITHS-content">
            <div class="header ITHS-list-header">
              <p class="">Övriga filer</p>
            </div>
          <div class="content">
            <div v-if="this.user.files">
              <div v-for="file in this.user.files" :key="file.id" class="columns">
                <div class="column">
                  {{ file.name }}
                </div>
                <div class="column">
                  {{ file.uploaded }}
                </div>
                <div class="column">
                  <b-button v-if="canEdit" class="is-danger" @click="Remove(file.id, file.name)" title="Tabort">
                    <b-icon class="file-icon" icon="delete"></b-icon>
                  </b-button>
                  <b-button class="ITHS-button-small" @click="handleDownload(file.id, file.name)" title="Ladda ner">
                    <b-icon class="file-icon" icon="download"></b-icon>
                  </b-button>
                </div>
              </div>
            </div>
            <div class="pillow">
              <b-button v-if="canEdit && user.files.length < 5" class="ITHS-button-small" @click="openAddFile" title="Ny fil">
                <b-icon icon="plus" style="color:white"></b-icon>
              </b-button>
              <span v-if="!user.cv && !canEdit" class="has-text-danger">Användaren har inga övriga filer....</span>
            </div>

          </div>
        </div>
  </section>
</template>

<script>
import FileUploadModal from "@/components/UserFiles/FileUploadModal";
import UserCvBox from "@/components/UserFiles/UserCvBox";

export default {
  name: "userFileList",
  components: {UserCvBox},
  props: {user: {}, canEdit: Boolean},
  data() {
    return {
      programme: {},
      isEmpty: false,
      userFiles: []
    };
  },
  computed: {},
  async created() {
  },
  methods: {
    getRowClass() {
      return 'has-text-centered'
    },
    openAddFile() {
      this.$buefy.modal.open({
        parent: this.$parent,
        component: FileUploadModal,
        props: {onUploadSuccess: this.onSuccess},
        hasModalCard: true,
        customClass: '',
        trapFocus: true
      })
    },
    async onSuccess() {
      await this.$store.dispatch("profile/getUserFiles", this.user.id)
    },
    async handleDownload(id, name) {
      await this.$store.dispatch("files/downloadById", id).then(
          res => this.StartDownload(res, name)
      ).catch(() => {
        this.$buefy.toast.open({
          message: "Något gick fel, gick inte att hämta...",
          type: 'is-danger'
        })
      })

    },
    //spara om vi behöver senare.
    formatBytes(bytes, decimals = 2) {
      if (bytes === 0) return "0 Bytes";
      const k = 1024;
      const dm = decimals < 0 ? 0 : decimals;
      const sizes = ['Bytes', 'kb', 'mb', 'gb'];

      const i = Math.floor(Math.log(bytes) / Math.log(k));
      return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + ' ' + sizes[i];
    },
    StartDownload(response, filename) {
      const url = window.URL.createObjectURL(new Blob([response]))
      const downloadLink = document.createElement('a')
      downloadLink.href = url
      downloadLink.setAttribute('download', filename) //or any other extension
      document.body.appendChild(downloadLink)
      downloadLink.click()
    },
    async Remove(id, name) {
      let confirmed = confirm("Remove file '" + name + "'?")
      if (confirmed) {
        await this.$store.dispatch("files/removeById", id).then(
            () => {
              let index = this.user.files.findIndex(file => {
                if (file.id === id) {
                  return true;
                }
              });
              this.user.files.splice(index, 1)
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
.pillow{
 margin-top: 1rem;
}
.columns{
  font-weight: bold;
  font-size: medium;
  border-bottom: 1px solid darkgray;
}
.columns:hover{
  background-color: rgba(0,0,0,0.2);
}

.ITHS-header.p{
  margin: auto;
}
.ITHS-content{
  margin-bottom: 1rem;
}

</style>
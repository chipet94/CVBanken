<template>
  <section>
    <div>
      <UserCvBox :can-edit="canEdit" :user="user"></UserCvBox>
    </div>
    <br>
    <div class="title is-6">Övriga filer</div>
    <b-table
        :data="isEmpty ? [] : this.user.files"
        :header-class="getRowClass"
        :hoverable=true
        :loading=false
        :mobile-cards=true
        title="Filer"
    >
      <b-table-column v-slot="props" class="" field="cv" width="80">
        <span v-if="props.row.isCv" class="has-text-black has-text-weight-bold">CV</span>
        <span v-else class="has-text-black has-text-weight-bold">Övrigt</span>
      </b-table-column>
      <b-table-column v-slot="props" class="fileTable" field="name">
        <span :title="'storlek: ' +formatBytes(props.row.size)">
                  {{ props.row.name }}
        </span>

      </b-table-column>
      <b-table-column v-slot="props">
        <b-button v-if="canEdit" class="is-danger" @click="Remove(props.row.id, props.row.name)">X</b-button>
        <b-button class="is-success" @click="handleDownload(props.row.id, props.row.name)">
          <b-icon icon="menu-down"></b-icon>
        </b-button>
      </b-table-column>
    </b-table>
    <!--    <component-modal v-if="canEdit && userFiles.length < 5"></component-modal>-->
    <b-button v-if="canEdit && userFiles.length < 5" class="ITHS-button-small is-center" @click="openAddFile">
      <b-icon icon="plus" style="color:white"></b-icon>
    </b-button>
  </section>
</template>

<script>
import ComponentModal from "@/components/UserFiles/ComponentModal";
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
        component: ComponentModal,
        props: {onUploadSuccess: this.onSuccess},
        hasModalCard: true,
        customClass: '',
        trapFocus: true
      })
    },
    async onSuccess() {
      await this.$store.dispatch("profile/getUserFiles", this.user.id)
    },
    async getFiles() {
      await this.$store.dispatch("profile/getUserFiles", this.user.id).then(
          res => {
            this.userFiles = res;
          }
      )
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
            res => {
              //this.getFiles();
              let index = this.user.files.findIndex(file => {
                if (file.id === id) {
                  return true;
                }
              });
              console.log(index)
              this.user.files.splice(index, 1)
              console.log(res)
            }
        ).catch(err => {
          console.log(err)
          this.$buefy.toast.open({
            message: "Något gick fel, gick inte att hämta...",
            type: 'is-danger'
          })
        })
      }
    },
    async SetCv(id) {
      await this.$store.dispatch("files/SetCV", id).then(() => {
        this.getFiles()
      }).catch(() => alert("Something went wrong, unable to set cv."))
    }
  }
}
</script>

<style scoped>
.ITHS-button-small {
  background-color: #693250;
  color: white;
  font-weight: bold;
}

.fileTable {
  padding-left: 50px;
}

</style>
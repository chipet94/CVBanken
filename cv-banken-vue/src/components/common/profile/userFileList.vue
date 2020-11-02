<template>
  <section>
    <div class="title is-6">Filer</div>
    <b-table
        :data="isEmpty ? [] : files"
        :hoverable=true
        :loading=false
        :mobile-cards=true
        :header-class="getRowClass"
        title="Filer"
    >
      <b-table-column v-slot="props" field="cv" width="80" class="">
        <span v-if="props.row.isCv" class="has-text-black has-text-weight-bold">CV</span>
        <span v-else class="has-text-black has-text-weight-bold">Övrigt</span>
<!--        {{ props.row.isCv === true?-->
<!--        '<b-icon icon="home"></b-icon>' : '<b-icon icon="close"></b-icon>' }}-->
        <b-button v-if="canEdit && !props.row.isCv" @click="SetCv(props.row.id)" class="is-info">set cv</b-button>
      </b-table-column>
      <b-table-column v-slot="props" field="name" class="fileTable">
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
    <component-modal v-if="canEdit && userFiles.length < 5"></component-modal>
  </section>
</template>

<script>
import ComponentModal from "@/components/UserFiles/ComponentModal";

export default {
  name: "userFileList",
  components: {ComponentModal},
  props: {userId: Number, files: Array, canEdit: Boolean},
  data() {
    return {
      programme: {},
      isEmpty: false,
      userFiles: []
    };
  },
  async created() {
  },
  methods: {
    getRowClass(){
      return 'has-text-centered'
    },
    async getFiles() {
      await this.$store.dispatch("files/getAllByUserId", this.userId).then(
          res => {
            this.userFiles = res;
          }
      )
    },
    async handleDownload(id, name) {
      await this.$store.dispatch("files/downloadById", id).then(
          res => this.StartDownload(res, name)
      ).catch(err => {
        alert(err)
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
        await this.$store.dispatch("files/removeById", id).then(() => {
          this.getFiles();
        }).catch(alert("something went wrong..."))
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
.fileTable{
  padding-left: 50px;
}

</style>
<template>
  <section>
    <b-collapse animation="slide" aria-id="userFilesContent" class="card" v-bind:open="true">
      <div
          slot="trigger"
          slot-scope="props"
          aria-controls="userFilesContent"
          class="card-header"
          role="button">
        <p class="card-header-title is-centered">
          Bilagor
        </p>
        <a class="card-header-icon">
          <b-icon
              :icon="props.open ? 'menu-down' : 'menu-up'">
          </b-icon>
        </a>
      </div>
      <b-table
          :data="isEmpty ? [] : userFiles"
          :hoverable=true
          :loading=false
          :mobile-cards=true
      >

        <b-table-column v-slot="props" field="name" label="Namn">
          {{ props.row.name }}
        </b-table-column>
        <b-table-column v-slot="props" field="ext" label="Typ">
          {{ props.row.ext }}
        </b-table-column>
        <b-table-column v-slot="props" field="size" label="Storlek">
          {{ formatBytes(props.row.size) }}
        </b-table-column>
        <b-table-column v-slot="props" label="Kontroller">
          <b-button v-if="canEdit" class="is-danger" @click="Remove(props.row.id)">X</b-button>
          <b-button class="is-success" @click="handleDownload(props.row.id, props.row.name)">
            <b-icon icon="menu-down"></b-icon>
          </b-button>
        </b-table-column>
      </b-table>
      <component-modal v-if="canEdit"></component-modal>
    </b-collapse>
  </section>
</template>

<script>
import ComponentModal from "@/components/UserFiles/ComponentModal";
export default {
name: "userFileList",
  components: {ComponentModal},
  props: {userId: Number, canEdit: Boolean},
  data() {
    return {
      programme: {},
      isEmpty: false,
      userFiles: []
    };
  },
  async created() {
  await this.getFiles();
  },
  methods: {
  async getFiles(){
    console.log(this.userId)
    await this.$store.dispatch("files/getAllByUserId", this.userId).then(
     res => {this.userFiles = res; console.log(res) }
    )
    console.log(this.userFiles)
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
    async Remove(id) {
      console.log(id)
      await this.$store.dispatch("files/removeById", id).then(() => {
        console.log("item has been removed.")
        this.getFiles();
      }).catch(err => console.log(err))
    }
  }
}
</script>

<style scoped>

</style>
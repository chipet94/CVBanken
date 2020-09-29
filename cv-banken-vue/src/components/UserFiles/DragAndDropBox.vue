<template>
  <section class="container">
    <b-field class="detail-container">
      <b-upload v-model="fileList"
                drag-drop
                multiple="multiple"
                size="5">
        <section class="">
          <div class="has-text-centered">
            <p>
              <b-icon
                  icon="upload"
                  size="is-large">
              </b-icon>
            </p>
            <p>Klicka här eller dra filer hit för att ladda upp.</p>
          </div>
        </section>
      </b-upload>
      <b-skeleton :active="loading" size="is-small"></b-skeleton>
      <b-field v-if="fileList.length > 0 && !loading" label="Valda filer:">
        <ul class="box detail-container has-background-grey-lighter">
          <li v-for="(item, index) in fileList" :key="index" class="detail">
            {{ item.name }}
            <button class="delete is-small has-background-danger"
                    type="button"
                    @click="deleteDropFile(index)">
            </button>
          </li>
        </ul>
      </b-field>
      <b-button v-if="fileList.length > 0" style="background-color: #693250; color: white" @click="Upload">Spara
      </b-button>
    </b-field>
  </section>
</template>

<script>
import UserFileService from "@/services/UserFileService";

export default {
  name: "DragAndDropBox",
  components: {},
  data() {
    return {
      fileList: [],
      progress: 0,
      loading: false
    }
  },
  methods: {
    deleteDropFile(index) {
      this.fileList.splice(index, 1)
    },
    Upload: async function () {
      let shouldUpload = confirm("Ladda upp " + this.fileList.length + " filer?")
      if (shouldUpload) {
        this.loading = true;

        let files = this.fileList;
        let formData = new FormData();
        for (let i = 0; i < files.length; i++) {
          formData.append('files', files[i]);
        }
        await UserFileService.uploadFile(formData).then(() => {

          this.fileList = [];
          this.loading = false;
          alert("Success! Dina filer är nu uppladdade!")

        }).catch(err => {

          this.loading = false;
          alert(err.response.data)
          console.log(err)
        })
      }
    },

  }
}
</script>

<style scoped>

</style>
<template>
  <section>
    <b-button @click="isComponentModalActive = true">
      <b-icon icon="plus" style="color:white"></b-icon>
    </b-button>

    <b-modal
        v-model="isComponentModalActive"
        :destroy-on-hide="false"
        aria-modal
        aria-role="dialog"
        has-modal-card
        trap-focus>
      <div class="modal-card" style="width: auto">
        <header class="modal-card-head" style="background-color: #693250; color:white; ">
          <p class="modal-card-title" style="color: white">Lägg till bilaga</p>
          <button
              class="delete"
              type="button"
              @click="isComponentModalActive = false"/>
        </header>
        <div style="background-color: white">
          <span class="is-small has-text-danger">Limits: 10mb per file & 5 files per user</span>
          <br>
          <span class="is-small has-text-danger">allowed: {{ supportedExt }}</span>
        </div>

        <section class="modal-card-body">
          <DragAndDropBox></DragAndDropBox>
        </section>
      </div>
    </b-modal>
  </section>
</template>

<script>
import DragAndDropBox from "@/components/UserFiles/DragAndDropBox";

export default {
  components: {DragAndDropBox},

  data() {
    return {
      isComponentModalActive: false,
      supportedExt: [".jpg", ".jpeg", ".png", ".txt", ".docx", ".doc", ".pdf"],
      formProps: {}
    }
  }
}
</script>

<style scoped>
.button {
  background-color: #693250;
}
</style>
<template>
  <section>
    <div class="modal-card" style="width: auto">
      <header class="modal-card-head" style="background-color: #693250; color:white; ">
        <p class="modal-card-title" style="color: white">Lägg till bilaga</p>
        <button
            class="delete"
            type="button"
            @click="$emit('close')"/>
      </header>
      <div style="background-color: white">
        <span class="is-small has-text-danger">Limits: 10mb per file & 5 files per user</span>
        <br>
        <span class="is-small has-text-danger">allowed: {{ supportedExt }}</span>
      </div>

      <section class="modal-card-body has-text-centered">
        <DragAndDropBox :on-upload-success="onSuccess" class="is-centered"></DragAndDropBox>
      </section>
    </div>
  </section>
</template>

<script>
import DragAndDropBox from "@/components/UserFiles/DragAndDropBox";

export default {
  components: {DragAndDropBox},
  props: {onUploadSuccess: Function},

  data() {
    return {
      isComponentModalActive: false,
      supportedExt: [".jpg", ".jpeg", ".png", ".txt", ".docx", ".doc", ".pdf"],
      formProps: {}
    }
  },
  methods: {
    onSuccess() {
      this.onUploadSuccess()
      this.$emit('close')
    }
  }
}
</script>

<style scoped>
.button {
  background-color: #693250;
}
</style>
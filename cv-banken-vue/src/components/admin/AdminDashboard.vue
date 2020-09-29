<template>
  <div>Only admins belong here.
    <drag-and-drop-box></drag-and-drop-box>
  </div>
</template>

<script>
import DragAndDropBox from "@/components/UserFiles/DragAndDropBox"

export default {
  name: "AdminDashboard",
  components: {DragAndDropBox}
}
</script>

<style scoped>

</style>
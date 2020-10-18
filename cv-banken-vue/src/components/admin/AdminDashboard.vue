<template>
  <div>Only admins belong here.
  <user-list></user-list>
  </div>
</template>

<script>


import UserList from "@/components/admin/UserList";
export default {
  name: "AdminDashboard",
  components: {UserList},
  computed:{
    thisUser() {
      return this.$store.getters["auth/getUser"]
    },
  }
}
</script>

<style scoped>

</style>
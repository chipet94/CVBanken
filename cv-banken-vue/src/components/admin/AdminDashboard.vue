<template>
  <div class="container">Only admins belong here *WORK-IN-PROGRESS*.
    <b-tabs v-model="activeTab" @input="handleTabs">
      <b-tab-item label="Programmes">
        <admin-programme-container>
        </admin-programme-container>
      </b-tab-item>
      <b-tab-item label="Site">
        <admin-site-home></admin-site-home>
      </b-tab-item>
      <b-tab-item label="Placeholder">
      </b-tab-item>
    </b-tabs>
  </div>
</template>

<script>

import AdminProgrammeContainer from "@/components/admin/programme/AdminProgrammeContainer";
import AdminSiteHome from "@/components/admin/site/AdminSiteHome";

export default {
  name: "AdminDashboard",
  components: {AdminSiteHome, AdminProgrammeContainer},
  data() {
    return {
      activeTab: 0,
      users: []
    }
  },
  computed: {

  },
  async created() {
  },
  methods: {
    async handleTabs(index) {
      if (index === 0) {
        console.log(0)
      }
      if (index === 1) {
        console.log(1)
      }
      if (index === 2) {
        console.log(2)
      }
      if (index === 3) {
        console.log(3)
      }
    },
  }
}
</script>

<style scoped>

</style>
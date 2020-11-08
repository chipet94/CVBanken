<template>
  <div>Only admins belong here *WORK-IN-PROGRESS*.
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
    thisProgrammes() {
      return this.$store.getters["edu/getAllProgrammes"];
    },
    thisAllUsers() {
      return this.$store.getters["user/admin_GetAllUsers"]
    },
    thisAllUsersin() {
      return this.$store.getters["user/AllCurrentUsers"]
    },
    thisUser() {
      return this.$store.getters["auth/getSession"]
    },
  },
  async created() {
    console.log(this.thisProgrammes)
    await this.loadPublicUsers();
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

    async loadAllUsers() {
      await this.$store.dispatch("user/admin_all").then(
          res => {
            console.log(res)
            return res;
          }
      )
    },
    async loadPublicUsers() {
      await this.$store.dispatch("user/all").then(
          res => {
            return res;
          }
      )
    },
    async loadAllCurrent() {
      await this.$store.dispatch("user/allInProgramme", 1001).then(
          res => {
            return res;
          })
    },
    async loadAllCurrentCategory() {
      await this.$store.dispatch("user/allInCategory", 3).then(
          res => {
            return res;
          })
    }
  }
}
</script>

<style scoped>

</style>
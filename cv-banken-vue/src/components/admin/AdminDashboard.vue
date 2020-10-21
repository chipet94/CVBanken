<template>
  <div>Only admins belong here.
    <b-tabs v-model="activeTab" @input="handleTabs">
      <b-tab-item label="public users">
        <user-list v-model="users"  :users="thisUsers"></user-list>
      </b-tab-item>

      <b-tab-item label="all users">
        <user-list v-model="users" :users="thisAllUsers"></user-list>
      </b-tab-item>
    </b-tabs>
  </div>
</template>

<script>


import UserList from "@/components/admin/UserList";
export default {
  name: "AdminDashboard",
  components: {UserList},
  data(){
    return{
      activeTab: 0,
      users: []
    }
  },
  computed:{
    thisUsers(){
      return this.$store.getters["user/AllPublicUsers"];
    },
    thisAllUsers(){
      return this.$store.getters["user/admin_GetAllUsers"]
    },
    thisUser() {
      return this.$store.getters["auth/getUser"]
    },
  },
  async created() {
    await this.loadPublicUsers();
  },
  methods:{
    async handleTabs(index) {
      if (index === 0){
        await this.loadPublicUsers();
      }
      if (index === 1){
        await this.loadAllUsers();
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
    }
  }
}
</script>

<style scoped>

</style>
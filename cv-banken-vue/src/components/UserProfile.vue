<template>
  <div>
    <h1 id="headline"></h1>
    <div id="section" class="m-t-12">
      <div class="columns is-centered">
        <div class="column is-6">
          <div class="box">
            <div class="container">
              <UserCard v-if="!loading" v-bind:user="user"></UserCard>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import UserCard from "@/components/UserCard";


export default {
  name: "UserProfile",
  components: {UserCard},
  created() {
    this.user = this.thisUser;
    this.loading = false;
  },
  data() {
    return {
      user: {},
      loading: true
    };
  },
  computed: {
    thisUser() {
      return this.$store.getters["auth/getUser"]
    },
  },
  methods: {}
};
</script>

<style scoped>

</style>
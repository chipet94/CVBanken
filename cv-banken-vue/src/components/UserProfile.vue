<template>
  <div>
    <profile-base v-if="!loading"></profile-base>
  </div>
</template>
<script>
import ProfileBase from "@/components/common/profile/profileBase";


export default {
  name: "UserProfile",
  components: {ProfileBase},
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
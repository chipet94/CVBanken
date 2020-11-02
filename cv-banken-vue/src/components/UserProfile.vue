<template>
  <div>
    <profile-base :editable="true" :url="thisSession.url"></profile-base>
  </div>
</template>
<script>
import ProfileBase from "@/components/common/profile/profileBase";


export default {
  name: "UserProfile",
  components: {ProfileBase},

  data() {
    return {
    };
  },
  computed: {
    thisSession(){
      return this.$store.getters["auth/getSession"];
    }
  },
  methods: {
  }
};
</script>

<style scoped>

</style>
<template>
  <div>
    <profile-base :editable="true" :user-model="thisUser"></profile-base>
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
    thisUser(){
      return this.$store.getters["profile/getThisUser"]?? null
    }
  },
  methods: {
  }
};
</script>

<style scoped>

</style>
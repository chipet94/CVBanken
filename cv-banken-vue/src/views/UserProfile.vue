<template>
  <div class="user_profile">
    <profile-base></profile-base>
  </div>
</template>

<script>
// @ is an alias to /src

import ProfileBase from "@/components/common/profile/profileBase";

export default {
  name: 'UserProfile',
  components: {
    ProfileBase,
  }
}
</script>

<template>
  <div class="user_profile">
    <UserProfile></UserProfile>
  </div>
</template>

<script>
// @ is an alias to /src
import UserProfile from '@/components/UserProfile.vue'

export default {
  name: 'Profile',
  components: {
    UserProfile
  }
}
</script>

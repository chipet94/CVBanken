<template>
  <section class="container">
    <div class="content">
      <img :src="profilePicture || require('@/assets/stockPhoto.jpg')" alt="Profilbild" class="">
    </div>
  </section>
</template>

<script>
export default {
  name: "ViewProfilePictureModal",
  props: {profilePicture: String}
}
</script>

<style scoped>

</style>
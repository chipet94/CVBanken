<template>
  <div class="container">
    <image-slider-container :slides="thisHome.slides"></image-slider-container>
    <div class="card">
      <div class="card-content">
        <div class="media">
          <div class="media-content">
            <p class="title is-4">Välkommen</p>
            <p class="subtitle is-6">Till ITHS CV-bank!</p>
          </div>
        </div>

        <div class="content" v-html="thisHome.content">
          <br>
          <small>@iths</small>
        </div>
      </div>
    </div>
  </div>

</template>

<script>
import ImageSliderContainer from "@/components/common/ImageSliderContainer";

export default {
  name: "Home",
  components: {ImageSliderContainer},
  created() {
    this.$store.dispatch("Site/getHome").then(console.log(this.thisHome));
  },
  computed: {
    thisHome() {
      return this.$store.getters["Site/getHome"];
    }
  }
}
</script>

<style scoped>

</style>
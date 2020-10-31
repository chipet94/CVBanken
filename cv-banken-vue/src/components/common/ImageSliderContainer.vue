<template>
  <b-carousel>
    <b-carousel-item v-for="(carousel, i) in slides" :key="i">
      <section :class="`hero is-medium is-${carousel.color}`">
        <div class="hero-body has-text-centered">
          <h1 class="title">{{carousel.title}}</h1>
          <p class="subtitle">{{carousel.subtitle}}</p>
          {{carousel.text}}
        </div>
      </section>
    </b-carousel-item>
  </b-carousel>
</template>

<script>
export default {
  name: "ImageSliderContainer",
  props:{slides: Array},
  created() {
    this.carousels = this.slides
  },
  data(){
    return {
      carousels: []
    }
  }
}
</script>

<style scoped>
.is-active .al img {
  filter: grayscale(0%);
}
.al img {
  filter: grayscale(100%);
}
</style>

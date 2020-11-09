<template>
  <b-carousel>
    <b-carousel-item v-for="(carousel, i) in slides" :key="i">
      <section :class="`hero is-medium is-${carousel.color}`">
        <div class="hero-body ITHS-carousel">
            <div class="ITHS-carousel-content">
              <h1 class="title">{{ carousel.title }}</h1>
              <p class="subtitle">{{ carousel.subtitle }}</p>
              {{ carousel.text }}
            </div>
        </div>
      </section>
    </b-carousel-item>
  </b-carousel>
</template>

<script>
export default {
  name: "ImageSliderContainer",
  props: {slides: Array},
  created() {
    this.carousels = this.slides
  },
  data() {
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
.ITHS-carousel{
 display: inline-flex;
  justify-content: center;
  text-align: center;
}
.ITHS-carousel-content{
  position: absolute;
  margin-left: auto;
  margin-right: auto;
}

.al img {
  filter: grayscale(100%);
}
</style>

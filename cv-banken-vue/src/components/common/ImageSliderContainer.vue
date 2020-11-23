<template>
  <div class="ITHS-carousel">
    <b-carousel >
      <b-carousel-item style="height: 400px;" v-for="(carousel, i) in slides" :key="i">
        <section style="height: 400px;" :class="`hero is-medium is-${carousel.color}`">
          <div  class="hero-body has-text-centered">
            <h1 class="title">{{carousel.title}}</h1>
            <p class="subtitle">{{carousel.subtitle}}</p>
            <span>{{carousel.text}}</span>
          </div>
        </section>
      </b-carousel-item>
    </b-carousel>
  </div>

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
  position: relative;
  justify-content: center;
  text-align: center;
  height: 400px
}
.ITHS-carousel-content{
  padding: 0;
  display: block;
  position: relative;
  margin: auto;
  height: 100%;
}

.al img {
  filter: grayscale(100%);
}
</style>

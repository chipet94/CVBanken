<template>
  <section>
    <b-loading v-model="loading" :can-cancel="true" :is-full-page="true"></b-loading>
    <div v-for="category in categories" :key="category.id">
      <category-container :category="category"></category-container>
      <br>
    </div>
  </section>

</template>

<script>
import CategoryContainer from "@/components/common/Lists/CategoryContainer";

export default {
  name: "CategoryList",
  components: {CategoryContainer},
  data() {
    return {
      programmes: [],
      loading: false,
    }
  },
  created() {
    this.loadCategories();
  },
  computed: {
    categories() {
      return this.$store.getters["edu/getCategories"]
    },
  },
  methods: {
    async loadCategories() {
      this.loading = true;
      await this.$store.dispatch("edu/getAllCategories").then(this.loading = false)
    },
    //Ta ej bort. Kan användas vid senare tillfälle.
    // groupBy(arr, property) {
    //   return arr.reduce(function (narr, x) {
    //     if (!narr[x[property]]) {
    //       narr[x[property]] = [];
    //     }
    //     narr[x[property]].push(x);
    //     return narr;
    //   }, {});
    // },
  }
}
</script>

<style scoped>

</style>
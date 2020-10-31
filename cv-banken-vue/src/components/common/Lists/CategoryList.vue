<template>
  <section class="container">

    <b-notification :closable="false" size="10x10">
      <b-loading v-model="loading" :can-cancel="true" :is-full-page="true"></b-loading>
      <div v-for="category in categories" :key="category.id">
        <category-container :category="category"></category-container>
        <br>
      </div>
    </b-notification>
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
    groupedProgrammes() {
      return this.groupBy(this.$store.getters["edu/getAllLocal"] ?? this.$store.dispatch("edu/getAll"), "category")
    }
  },
  methods: {
    async loadCategories() {
      this.loading = true;
      await this.$store.dispatch("edu/getAllCategories").then(this.loading = false)
    },
    groupBy(arr, property) {
      console.log(arr)
      return arr.reduce(function (narr, x) {
        if (!narr[x[property]]) {
          narr[x[property]] = [];
        }
        narr[x[property]].push(x);
        return narr;
      }, {});
    },
  }
}
</script>

<style scoped>

</style>
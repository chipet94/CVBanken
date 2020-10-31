<template>
  <section class="container">
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
  data(){
    return{
      programmes:[]
    }
  },
  created() {
    this.$store.dispatch("edu/getAll");
    this.$store.dispatch("edu/getAllCategories")
  },
  computed:{
    categories(){
      return this.$store.getters["edu/getCategories"]
    },
    groupedProgrammes(){
      return this.groupBy(this.$store.getters["edu/getAllLocal"]?? this.$store.dispatch("edu/getAll"), "category")
    }
  },
  methods:{
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
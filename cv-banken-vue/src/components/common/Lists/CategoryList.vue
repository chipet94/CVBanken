<template>
  <section>
    <b-collapse :open="false" animation="slide" aria-id="categoryName" class="card is-centered ITHS-filter-expandable">
      <div
          slot="trigger"
          aria-controls="categoryName"
          slot-scope="props"
          class="card-header ITHS-filter-expandable-header "
          role="button">
      <p class="card-header-title is-centered ITHS-text">
        Filter
      </p>
      <a class="ITHS-text card-header-icon " title="Expandera">
        <b-icon
            :icon="props.open ? 'menu-down' : 'menu-up'">
        </b-icon>
      </a>
      </div>
      <div class="card-content">
        <div class="content">
          <section class="control-section">
            <b-field label="Städer" class="control-section">
              <b-checkbox v-model="programmeFilters.gbg">Göteborg</b-checkbox>
              <b-checkbox v-model="programmeFilters.sthlm">Stockholm</b-checkbox>
            </b-field>
          </section>

        </div>
      </div>

    </b-collapse>

    <b-loading v-model="loading" :can-cancel="true" :is-full-page="true"></b-loading>
    <div v-for="category in categories" :key="category.id">
      <category-container :category="category" :filters="programmeFilters"></category-container>
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
      programmeFilters: {gbg: true, sthlm: true},
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
.ITHS-filter-expandable {
  border-top: 1px solid whitesmoke;
  width: 50%;
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 10px;
}

.ITHS-filter-expandable-header {

  background-color: #693250;
  border-top: 1px solid whitesmoke;
  border-radius: 10px;
  height: 2rem;
  color: #f1f1f1;
}
.ITHS-filter-expandable-header *{
  color: #f1f1f1;
}
.control-board{
  font-size: 1rem;
  border-radius: 15px;
  background-color: lightgray;
  display: block;
  width: 50%;
  margin-right: auto;
  margin-left: auto;
  margin-bottom: 12px;
  padding: 12px;
}
.control-section{
  font-size: 12px;
  display: block;
  width: 50%;
  text-align: center;
}

</style>
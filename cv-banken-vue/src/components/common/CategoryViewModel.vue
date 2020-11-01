<template>
<div class="container">
  <div class="card">
    <div class="card-image">
    </div>
    <div class="card-content">
      <div class="media">
        <div class="media-left">
        </div>
        <div class="media-content">
          <p class="title is-4">{{thisCategory.name}}</p>
          <p class="subtitle is-6">Kategori</p>
        </div>
      </div>

      <div class="content">
        <b-field label="Klasser">
          <programme-list :programmes="thisCategory.programmes"></programme-list>
        </b-field>

      </div>
    </div>
  </div>
</div>
</template>

<script>
import ProgrammeList from "@/components/common/Lists/ProgrammeList";
export default {
  name: "CategoryViewModel",
  components: {ProgrammeList},
  data(){
    return{
        loading : false
    }
  },
  computed:{
    thisCategory(){
      return this.$store.getters["edu/getCategoryFromName"](this.$route.params.name)
    }
  },
  async created() {
    await this.getCategory()
  },
  methods:{
      async getCategory(){
        if (this.$route.params.name){
          this.loading = true;
          await this.$store.dispatch("edu/getCategoryByName", this.$route.params.name).then(
              this.loading = false
          )
        }
        console.log(this.thisCategory)
      }
  },

}
</script>

<style scoped>

</style>
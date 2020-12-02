<template>
  <section>
    <b-collapse :open="false" animation="slide" aria-id="categoryName" class="card ITHS-expandable" :hidden="filteredProgrammes.length < 1">
      <div
          slot="trigger"
          slot-scope="props"
          aria-controls="categoryName"
          class="card-header ITHS-expandable-header"
          role="button">
        <p class="card-header-title is-centered ITHS-text">
          {{ category.name }}
        </p>
        <a class="ITHS-text card-header-icon " title="Expandera">
          <b-icon
              :icon="props.open ? 'menu-down' : 'menu-up'">
          </b-icon>
        </a>
        <router-link :to="{ name: 'Kategori', params: {name: category.name}}" class="card-header-icon"
                     title="Gå till kategori">
          <b-icon
              icon="link">
          </b-icon>
        </router-link>
      </div>
      <div class="card-content">
        <div class="content">
          <programme-list :programmes="filteredProgrammes"></programme-list>
        </div>
      </div>
    </b-collapse>

  </section>
</template>

<script>
import ProgrammeList from "@/components/common/Lists/ProgrammeList";

export default {
  name: "CategoryContainer",
  components: {ProgrammeList},
  props: {category: {}, filters: {}},
  data() {
    return {}
  },
  computed:{
    filteredProgrammes(){
      if (this.filters){
        if ( this.filters.gbg && this.filters.sthlm)
        {
          return this.category.programmes
        }
        else if(this.filters.gbg)
        {
          return this.category.programmes.filter(prog => prog.location === "Göteborg");
        }
        else if (this.filters.sthlm){
          return this.category.programmes.filter(prog => prog.location === "Stockholm");
        }
        return []
      }
      return this.category.programmes

    }
  }
}
</script>

<style scoped>

</style>
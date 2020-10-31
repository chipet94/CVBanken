<template>
  <section>
    <b-collapse class="card" animation="slide" aria-id="categoryName">
      <div
          slot="trigger"
          slot-scope="props"
          class="card-header ITHS-header"
          role="button"
          aria-controls="categoryName">
        <p class="card-header-title is-centered ITHS-text">
          {{category.name}}
        </p>
        <a class="card-header-icon ITHS-text">
          <b-icon
              :icon="props.open ? 'menu-down' : 'menu-up'">
          </b-icon>
        </a>
      </div>
      <div class="card-content">
        <div class="content">
          <programme-list :programmes="category.programmes"></programme-list>
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
  props:{category: {}},
  data(){
    return{

    }
  }
}
</script>

<style scoped>
.ITHS-header{
 background-color: #693250;
  color: #f1f1f1;
}
.ITHS-header :hover{
  background-color: #710642;
}
.ITHS-text{
  color: #f1f1f1;
}
</style>
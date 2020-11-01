<template>
  <div class="container">
    <div class="card">
      <div class="card-content">
        <div class="media-content">
          <p class="title is-4">Alla utbildningar</p>
          <p class="subtitle is-6">Kategorier</p>
        </div>
      </div>

      <div class="content">
        <b-field label="Nedan finner du alla tillgängliga ITHS utbildningar.">
          <p>
            Klicka på <b-icon icon="link"></b-icon> för att besöka klassen / kategorin. <br>
            Eller klicka på Namnet för att expandera / minimera. <br>
            <span class="has-text-danger has-text-weight-bold">Endast studenter med publika profiler syns.</span> <br>
          </p>
        </b-field>
        <category-list></category-list>
      </div>
    </div>
  </div>


</template>
<script>

import CategoryList from "@/components/common/Lists/CategoryList";

export default {
  name: "CategoryView",
  components: {
    CategoryList
  }
}
</script>

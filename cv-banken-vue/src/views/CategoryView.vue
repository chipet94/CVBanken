<template>
  <div>
    <div>
      Här kan du se alla utbildningar och navigera bland elever.
    </div>
    <category-list>
    </category-list>
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

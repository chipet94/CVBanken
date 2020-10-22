<template>

  <div v-if="data.length >1">
    <education-table-model :category-list="data"></education-table-model>
  </div>
</template>

<script>

import EducationTableModel from "@/components/Globals/EducationTableModel";
export default {
  components: {EducationTableModel},
  created() {
    console.log('Educations')
    this.getProgrammes()
  },
  data() {
    return {
      data: []
    }
  },
  methods: {
    async getProgrammes() {
      await this.$store.dispatch("edu/getAll").then(res => {
        this.data = res
      }).catch(err => alert(err))
      console.log(this.data)
    },
    
    rowClicked(row) {
      alert(row.name)
    }
  },
  
}
</script>


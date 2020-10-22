<template>

  <div>
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
        /*for(let i = 0; i<res.length; i++){
          let eduId = res[i]
          console.log(eduId)
          //this.getEducationsArray(eduId)
          //this.getCategoryName(this.data[i].category)
        }*/
      }).catch(err => alert(err))
      console.log(this.data)
    },
    

/*getEducationsArray(obj){
  //let array = []
  this.tables.push(obj)
  //this.tables.push(catId)
  console.log(obj)
  //this.getCategoryName(catId)
  for (let i = 0; i<this.tables.length; i++){
    console.log(this.tables[i].name)
    for(let j = 0; j<this.columns; j++){
      this.tables[i].name = this.columns[j].lable
    }
  }
  
},*/
    
    rowClicked(row) {
      alert(row.name)
    }
  },
  
}
</script>


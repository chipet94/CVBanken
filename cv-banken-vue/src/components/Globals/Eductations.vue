<template>

  <div>
    <education-table-model :category-list="data"></education-table-model>
    <!--<b-field>
      <input type="text" v-model="search" @change="filteredEductions" placeholder="Sök utbildning">
    </b-field>
    <b-field v-for="table in tables" :data="data" :key="table.id" :columns="columns" @click="rowClicked">
      <b-table :data="data" :columns="columns" @click="rowClicked"></b-table>
      <b-table id="1" :data="data" :columns="columns" @click="rowClicked"></b-table>
      <b-table id="2" :data="data" :columns="columns" @click="rowClicked"></b-table>
      <b-table id="3" :data="data" :columns="columns" @click="rowClicked"></b-table>
    </b-field>-->
  </div>
</template>

<script>

import EducationTableModel from "@/components/Globals/EducationTableModel";
export default {
  components: {EducationTableModel},
  created() {
    this.getProgrammes()
  },
  data() {
    return {
      search: "",
      headline: "",
      data: [],
      //tables: [],
      columns: [
      
        {
          field: 'name',
          label: this.headline
        }
      ],
    }
  },
  computed: {
    filteredEductions() {
      return this.data.filter((eduction) => {
        return eduction.name.match(this.search)
      });

    }
  },
  methods: {
    async getProgrammes() {
      await this.$store.dispatch("edu/getAll").then(res => {
        this.data = res
        console.log(res)
        for(let i = 0; i<res.length; i++){
          let eduId = res[i]
          console.log(eduId)
          //this.getEducationsArray(eduId)
          //this.getCategoryName(this.data[i].category)
        }
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


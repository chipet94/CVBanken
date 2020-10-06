<template>
  <div>
    <b-table
        :data="isEmpty ? [] : categoryList"
        :hoverable=true
        :mobile-cards=true
        @click="handleselect"
    >
      <b-table-column field="category" label="Category" v-slot="props">
        {{ getCategoryName(props.row.category) }}
      </b-table-column>
      
    </b-table>
  </div>
</template>

<script>
export default {
  name: "EducationTableModel",
  props:{
    categoryList: []
  },
  data(){
    return {
      templist: [{id: 1, category:2,name:"Defult02"},
        {id: 2, category:2,name:"Defult03"},
        {id: 3, category:2,name:"Defult04"},
      ],

      isEmpty: false
    }
    
  },
  methods: {
    handleselect(record){
      console.log("hej" + record.name)
    },
    getCategoryName(num) {
      switch (num) {
        case 1:
          return "Webbutveckling"
        case 2:
         return  "Applikationsutveckling"
        case 3:
          return "JavaUtveckling"
        case 4:
          return  ".Netutveckling"
        case 5:
          return  "Mjukvarutestare"
        case 6:
         return  "Frontendutvecklare"
        case 7:
          return  "IT-Projektledare"
        case 8:
          return "JavaScriptutveckling"
        case 9:
          return "UX-Designer"
        default:
          return  "Ingen utbildning"
      }
      
    },
  }
}
</script>
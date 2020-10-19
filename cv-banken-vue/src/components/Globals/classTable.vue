<template> 
  <div>
     <b-table v-for="item in data" :key="item.category">
       <div @click="handleClick">
         {{item}}
       </div>
     </b-table>                  
  </div>
</template>

<script>
//import Eductations from "@/components/Globals/Eductations";
export default {
  props:{
     passedData:[]
  },
  created() {
    console.log(this.passedData)
    this.sortClasses()
  },
  data(){
    return{
      data: []
    } 
  },
  methods:{
     sortClasses(){
       console.log("PassedData",this.passedData)
       for(let i=0; i<=this.passedData.length; i++){
          console.log(this.passedData[i].category)
         switch (this.passedData[i].category) {
            case 0:
              this.data.push(this.passedData[i].name)
                 break
            case 1:
              this.data.push(this.passedData[i].name)
                 break
            case 4:
             this.data.push(this.passedData[i].name)
                 break
            case 3:
             this.data.push(this.passedData[i].name)
                 break
         }    
       }
       console.log(this.data)
     },
    handleClick(){
       console.log(this.data)
    }
  }
}
</script>
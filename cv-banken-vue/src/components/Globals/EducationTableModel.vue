<template>
  <div>
    <!--<b-table
        :data="isEmpty ? [] : categoryList"
        :hoverable=true
        :mobile-cards=true
        @click="handleselect"
    >
      <b-table-column field="category" label="Educations" v-slot="props">
        {{ getCategoryName(props.row.category) }}
      </b-table-column>
      
    </b-table>-->
    <section>
      <b-table
          :data="isEmpty ? [] : categoryList"
          ref="table"
          paginated
          per-page="5"
          :opened-detailed="defaultOpenedDetails"
          detailed
          detail-key="id"
          @details-open="(row) => $buefy.toast.open(`Expanded`)"
          :show-detail-icon="showDetailIcon"
          aria-next-label="Next page"
          aria-previous-label="Previous page"
          aria-page-label="Page"
          aria-current-label="Current page">

        <b-table-column field="categoryName" label="Utbildningar" width="40" v-slot="props">
          {{ getCategoryName(props.row.category) }}
        </b-table-column>
        
        <template slot="detail" slot-scope="props">
          <article class="media">
            <div class="media-content">
              <div class="content">
                <class-table :passedData="categoryList"></class-table>
                <!--Här kan jag göra vadfan som, men det jag borde göra :
                1. en ny component som skall visa det olika klasserna i kategorin
                2. för att denna komponent skall fungera behlver jag följade
                3. komponenten skall ta emot en prop som är en array med strängar i(res)
                -->
              </div>
            </div>
          </article>
        </template>
      </b-table>

    </section>
    
  </div>
</template>

<script>
import classTable from "@/components/Globals/classTable";
export default {
  name: "EducationTableModel",
  props:{
    categoryList: []
  },
  components: {classTable},
  data(){
    return {
      templist: [{id: 1, category:2,name:"Defult02"},
        {id: 2, category:2,name:"Defult03"},
        {id: 3, category:2,name:"Defult04"},
      ],
      defaultOpenedDetails: [1],
      showDetailIcon: true,
      isEmpty: false
    }
    
  },
  showDetailIcon: true,
  methods: {
    handleselect(record){
      console.log(record)
      //classModal(record)
      vue.component({classModal})
      //this.$refs.classModal(record)
          //.toggleDetails(record)
      //this.classModal()
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

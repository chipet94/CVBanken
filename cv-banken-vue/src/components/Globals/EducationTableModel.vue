<template>
  <div>
    <b-table
        :data="isEmpty ? [] : categoryList"
        aria-next-label="Next page"
        aria-previous-label="Previous page"
        aria-page-label="Page"
        aria-current-label="Current page"
        ref="table"
        paginated
        per-page="5"
        :opened-detailed="defaultOpenedDetails"
        detailed
        detail-key="id"
        @details-open="(row) => $buefy.toast.open(`Expanded`)"
        :show-detail-icon="showDetailIcon"
    >
      <b-table-column field="categoryName" label="Utbildningar" width="40" v-slot="props">
        {{props.row.categoryName}}
      </b-table-column>
      <template slot="detail" slot-scope="props">
        <article class="media">
          <div class="media-content">
            <div class="content">
              <class-table :category-list="classes[props.row.category]"></class-table>  
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
  </div>
</template>

<script>
import ClassTable from "@/components/Globals/classTable";
export default {
  name: "EducationTableModel",
  components: {ClassTable},
  created() {
    console.log('EducationTableModal class')
  },
  props:{
    categoryList: []
  },
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
  computed:{
    classes(){
      return this.groupBy(this.categoryList, "category")
    }
  },
  methods: {
    groupBy(arr, property) {
      return arr.reduce(function (narr, x) {
        if (!narr[x[property]]) {
          narr[x[property]] = [];
        }
        narr[x[property]].push(x);
        return narr;
      }, {});
    },
  }
}
</script>

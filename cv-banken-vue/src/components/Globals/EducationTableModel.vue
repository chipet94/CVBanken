<template>
  <div>
    <b-table
        :data="isEmpty ? [] : categoryList"
        :opened-detailed="defaultOpenedDetails"
        :show-detail-icon="showDetailIcon"
        detail-key="id"
        detailed
        @details-open="(row) => $buefy.toast.open(`Expanded`)"
    >
      <b-table-column v-slot="props" field="categoryName" label="Utbildningar" width="40">
        {{ props.row.categoryName }}
      </b-table-column>
      <template slot="detail" slot-scope="props">
        <article class="media">
          <div class="media-content">
            <div class="content">
              <class-table :category-list="templist[props.row.category]"></class-table>
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
    this.templist = this.classes
  },
  props: {
    categoryList: Array
  },
  data() {
    return {
      templist: [],
      defaultOpenedDetails: [1],
      showDetailIcon: true,
      isEmpty: false
    }

  },
  showDetailIcon: true,
  computed: {
    classes() {
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

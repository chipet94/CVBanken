<template>
  <section>
    <b-table
        @click="handleClick"
        :data="categoryList"
        :hoverable=true
        :loading=false
        :mobile-cards=true
    >
      <b-table-column v-slot="props" field="name" label="Klass">
        {{ props.row.name }}
      </b-table-column>
    </b-table>
    <b-modal
        v-model="isComponentModalActive"
        has-modal-card
        trap-focus
        :destroy-on-hide="false"
        aria-role="dialog"
        aria-modal>
      <template>
        <Modalmodel v-model="specificUsers" 
                    :class-name="selectedRow.name" 
                    :programme="selectedRow.categoryName" 
                    @close="isComponentModalActive = false" 
                    :users="specificUsers">
          
        </Modalmodel>
      </template>
    </b-modal>
  </section>
</template>

<script>

import Modalmodel from "@/components/Globals/Modalmodel";
export default {
  props: {
    categoryList: Array
  },
  created() {
    console.log('ClassModal class')
  },
  components: {
    Modalmodel,
  },
  methods:{
    async handleClick(index){
      this.selectedRow = index
      await this.loadAllCurrent()
      this.isComponentModalActive = true
      
    },
    async loadAllCurrent(){
      await this.$store.dispatch("user/allInProgramme", this.selectedRow.id)
          .then(
          res => {
            this.specificUsers = res;
          })
    }
  },
  data() {
    return {
      data: [],
      isComponentModalActive: false,
      selected: 0,
      specificUsers: [],
      selectedRow: {}
    }
  }
}
</script>
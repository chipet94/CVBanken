<template>
  <section>
    <b-table
        :data="categoryList"
        :hoverable=true
        :loading=false
        :mobile-cards=true
        @click="handleClick"
    >
      <b-table-column v-slot="props">
        {{ props.row.name }}
      </b-table-column>
    </b-table>
    <b-modal
        v-model="isComponentModalActive"
        :destroy-on-hide="false"
        aria-modal
        aria-role="dialog"
        has-modal-card
        trap-focus>
      <template>
        <Modalmodel v-model="specificUsers"
                    :class-name="selectedRow.name"
                    :programme="selectedRow.categoryName"
                    :users="specificUsers"
                    @close="isComponentModalActive = false">

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
  methods: {
    async handleClick(index) {
      console.log(index)
      this.selectedRow = index
      await this.loadAllCurrent()
      this.isComponentModalActive = true

    },
    async loadAllCurrent() {
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
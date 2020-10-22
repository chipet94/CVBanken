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
        <Modalmodel v-model="specificUsers" @close="isComponentModalActive = false" :users="specificUsers"></Modalmodel>
      </template>
    </b-modal>
  </section>
</template>

<script>
/*const ModalForm = {
 async created() {
    console.log('Modal create')
    await this.loadAllCurrent()
  },
  data(){
    return{
      specificUsers: []
    }
  },
  props: {
    programmeId: Number
  },
  methods:{
    async loadAllCurrent(){
      await this.$store.dispatch("user/allInProgramme", this.programmeId).then(
          res => {
            this.specificUsers = res;
          })
    },
  },
  template: `
            <form action="">
                <div class="modal-card" style="width: auto">
                    <header class="modal-card-head">
                        <p class="modal-card-title">Studenter</p>
                        <button
                            type="button"
                            class="delete"
                            @click="$emit('close')"/>
                    </header>
                    <section class="modal-card-body">
                      <b-table :data="specificUsers">
                        <b-table-column v-slot="props" field="name" label="Studenter">
                          {{ props.row.firstName }}
                        </b-table-column>
                        <b-table-column v-slot="props" field="name" label="Utbildning">
                          {{ props.row.categoryName }}
                        </b-table-column>
                      </b-table>
                    </section>
                </div>
            </form>
        `
}*/

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
      this.selected = index.id
      await this.loadAllCurrent()
      this.isComponentModalActive = true
      
    },
    async loadAllCurrent(){
      await this.$store.dispatch("user/allInProgramme", this.selected).then(
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
      specificUsers: []
    }
  }
}
</script>
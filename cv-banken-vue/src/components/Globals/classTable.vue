<template>
  <section>
    <b-table
        @click="isComponentModalActive = true"
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
      <template #default="props">
        <modal-form v-bind="formProps" @close="props.close"></modal-form>
      </template>
    </b-modal>
  </section>
  <!--<div>
     <b-table v-for="item in data" :key="item.category">
       <div @click="handleClick">
         {{item}}
       </div>
     </b-table>                  
  </div>-->
</template>

<script>
const ModalForm = {
  props: ['email', 'password', 'canCancel'],
  template: `
            <form action="">
                <div class="modal-card" style="width: auto">
                    <header class="modal-card-head">
                        <p class="modal-card-title">Login</p>
                        <button
                            type="button"
                            class="delete"
                            @click="$emit('close')"/>
                    </header>
                    <section class="modal-card-body">
                      
                    </section>
                    <footer class="modal-card-foot">
                        <button class="button" type="button" @click="$emit('close')">Close</button>
                        <button class="button is-primary">Login</button>
                    </footer>
                </div>
            </form>
        `
}
//import Eductations from "@/components/Globals/Eductations";
export default {
  props: {
    categoryList: []
  },
  components: {
    ModalForm
  },
  created() {
    console.log(this.categoryList)
    //this.sortClasses()
  },
  methods: {
    sortClasses() {
      console.log("PassedData", this.passedData)
      for (let i = 0; i <= this.passedData.length; i++) {
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
    handleClick() {
      console.log(this.props.categoryList)
    }
  },
  data() {
    return {
      data: [],
      isComponentModalActive: false,
      formProps: {
        email: 'evan@you.com',
        password: 'testing'
      }
    }
  }
}
</script>
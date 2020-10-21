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
                      <b-table v-for="user in users" :key="user">
                        <b-table-column field="name" label="Studenter">
                          {{ user }}
                        </b-table-column>
                      </b-table>
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
    this.loadAllUsers()
  },
  methods: {
    async loadAllUsers(){
      await this.$store.dispatch("profile/getAllProfiles").then(
          res => {
            this.users = res;
          }
      )
      /*for(let i = 0; i<=this.users.length; i++){
        console.log(this.users[i].user.firstName)
      }*/
      
      //this.isEmpty = this.users < 1
      //this.loading = false;
    },
    
    handleClick() {
      console.log(this.props.categoryList)
    },
  },
  data() {
    return {
      data: [],
      users:[],
      isComponentModalActive: false,
      formProps: {
        email: 'evan@you.com',
        password: 'testing'
      }
    }
  }
}
</script>
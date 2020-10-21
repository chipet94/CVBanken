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
        <modal-form @close="props.close"></modal-form>
      </template>
    </b-modal>
  </section>
</template>

<script>
const ModalForm = {
  created() {
    this.loadPublicUsers()
  },
  props: {
    users: []
  },
  methods:{
    async loadPublicUsers() {
      await this.$store.dispatch("user/all").then(
          res => {
            this.users = res
            console.log(this.users)
          }
      )
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
                      <b-table :data="users">
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
}
//import Eductations from "@/components/Globals/Eductations";
export default {
  props: {
    categoryList: []
  },
  components: {
    ModalForm
  },
  data() {
    return {
      data: [],
      isComponentModalActive: false,
    }
  }
}
</script>
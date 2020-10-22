<template>
  <form action="">
    <div class="modal-card" style="width: auto">
      <header class="modal-card-head">
        <p class="modal-card-title">
          {{users[0].categoryName}}
          <br>
        <span class="subtitle is-centered">{{users[0].class_name}}</span>
        </p>
        <button
            type="button"
            class="delete"
            @click="$emit('close')"/>
      </header>
      <section class="modal-card-body">
        <b-table :data="users" @click="handleClick" hoverable>
          <b-table-column v-slot="props" field="name" label="Namn">
            {{ props.row.firstName + " " + props.row.lastName }}
          </b-table-column>
          <b-table-column v-slot="props" field="email" label="Email">
            {{ props.row.email }}
          </b-table-column>
          <b-table-column v-slot="props" field="name" label="Program">
            {{ props.row.categoryName }}
          </b-table-column>
          <b-table-column v-slot="props" field="searching" label="Söker LIA">
            {{ props.row.searching ? "Ja" : "Nej" }}
          </b-table-column>
        </b-table>
      </section>
    </div>
  </form>
</template>

<script>
export default {
  name: "Modalmodel",
  async created() {
    console.log(this.users)
    console.log('Modal create')
  },
  data(){
    return{
      
    }
  },
  props: {
    programmeId: Number,
    isActive: Boolean,
    users:Array
  },
  methods:{
    handleClick(index){
      console.log(index)
      this.$router.push('profile/'+index.url)
    }
  },
}
</script>

<template>
  <section>
    <b-table
        :data="thisStudents < 1? [] : thisStudents"
        :hoverable=true
        :mobile-cards=true
        :row-class="getRowClass"
        sort-multiple
    >
      <template>
        <b-table-column v-if="isAdmin" v-slot="props" field="id" label="Id">
          {{ props.row.id }}
        </b-table-column>
        <b-table-column v-if="isAdmin" v-slot="props" field="private" label="Privat?">
          {{ props.row.private? "ja" : "nej" }}
        </b-table-column>
        <b-table-column v-slot="props" field="name" label="Namn">
          {{ props.row.firstName + " " + props.row.lastName }}
        </b-table-column>
        <b-table-column v-slot="props" field="email" label="Email">
          {{ props.row.email }}
        </b-table-column>
        <b-table-column v-slot="props" field="searching" label="Söker LIA">
          <b-tag v-if="props.row.searching" rounded class="is-success">ja</b-tag>
          <b-tag v-else rounded class="is-danger" >Nej</b-tag>
        </b-table-column>
        <b-table-column v-if="isAdmin" v-slot="props" field="rank" label="Typ">
          {{ props.row.role }}
        </b-table-column>
        <b-table-column v-if="isAdmin" v-slot="props" field="cv" label="Har CV">
          {{ props.row.gotCv? "ja" : "nej" }}
        </b-table-column>
        <b-table-column v-slot="props" field="profile" label="Profil">
          <router-link :to="{ name: 'Profile', params: { url: props.row.url }}">Öppna</router-link>
        </b-table-column>
      </template>
    </b-table>
  </section>

</template>

<script>
export default {
name: "StudentsList",
  props:{students: Array, programmeId: Number},
  data(){
  return{
    studentArr: []
  }
  },
  computed: {
  thisStudents(){
    return this.$store.getters["edu/studentsInProgramme"](this.programmeId);
  },
      isAdmin(){
        return this.$store.getters["auth/getUser"].role === "Admin";
      }
  },
  async created() {
  // await this.$store.dispatch("edu/getStudentsIn", this.programmeId).then(console.log(this.thisStudents))
  // if (this.students === undefined && this.programmeId !== undefined){
  //   this.studentArr = await this.$store.dispatch("user/allInProgramme", this.programmeId)
  // }
  },
  methods:{
    getRowClass(row) {
      if (row.private) return 'has-background-grey-light'
    }
  }
}
</script>

<style scoped>
.is-private{
  background-color: #710642;
  color: red;
}

</style>
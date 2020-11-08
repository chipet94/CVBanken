<template>
  <section>
    <b-table
        :data="thisStudents < 1? [] : thisStudents"
        :hoverable=true
        :mobile-cards=true
        :row-class="getRowClass"
        sort-multiple
    >
      <b-loading v-model="loading" :can-cancel="true" :is-full-page="false">
        <b-icon
            custom-class="fa-spin"
            icon="sync-alt"
            pack="fas"
            size="is-large">
        </b-icon>
      </b-loading>
      <template v-if="!loading">
        <b-table-column v-if="isAdmin" v-slot="props" field="id" label="Id">
          {{ props.row.id }}
        </b-table-column>
        <b-table-column v-if="isAdmin" v-slot="props" field="private" label="Privat?">
          {{ props.row.private ? "ja" : "nej" }}
        </b-table-column>
        <b-table-column v-slot="props" field="name" label="Namn">
          {{ props.row.firstName + " " + props.row.lastName }}
        </b-table-column>
        <b-table-column v-slot="props" field="email" label="Email">
          {{ props.row.email }}
        </b-table-column>
        <b-table-column v-slot="props" field="searching" label="Söker LIA">
          <b-tag v-if="props.row.searching" class="is-success" rounded>ja</b-tag>
          <b-tag v-else class="is-danger" rounded>Nej</b-tag>
        </b-table-column>
        <b-table-column v-slot="props" field="cv" label="Har CV">
          <b-tag v-if="props.row.gotCv" class="is-success" rounded>ja</b-tag>
          <b-tag v-else class="is-danger" rounded>Nej</b-tag>
        </b-table-column>
        <b-table-column v-slot="props" field="profile" label="Profil">
          <router-link :to="{ name: 'Profile', params: { url: props.row.url }}">
            <b-button class="ITHS-button-small" title="Gå till profil">
            <b-icon icon="account"></b-icon>
            </b-button>
          </router-link>
        </b-table-column>
      </template>
    </b-table>
  </section>

</template>

<script>
export default {
  name: "StudentsList",
  props: {students: Array, programmeId: Number, loading: Boolean},
  data() {
    return {
      studentArr: []
    }
  },
  computed: {
    thisStudents() {
      return this.$store.getters["edu/studentsInProgramme"](this.programmeId);
    },
    isAdmin() {
      return this.$store.getters["auth/getSession"].role === "Admin";
    }
  },
  async created() {
  },
  methods: {
    getRowClass(row) {
      if (row.private) return 'has-background-grey-light'
    }
  }
}
</script>

<style scoped>
.is-private {
  background-color: #710642;
  color: red;
}
.ITHS-button-small {
   background-color: #693250;
   color: white;
   font-weight: bold;
   border: none;
 }

</style>
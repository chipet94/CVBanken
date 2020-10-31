<template>
  <section>
    <b-table
        :data="users < 1? [] : users"
        :hoverable=true
        :loading=loading
        :mobile-cards=true
    >
      <b-table-column v-slot="props" field="id" label="Id">
        {{ props.row.id }}
      </b-table-column>
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
      <b-table-column v-slot="props" field="private" label="Privat">
        {{ props.row.private ? "Ja" : "Nej" }}
      </b-table-column>
      <b-table-column v-slot="props" field="rank" label="Typ">
        {{ props.row.role }}
      </b-table-column>
      <b-table-column v-slot="props" field="profile" label="Profil">
        <router-link :to="{ name: 'Profile', params: { url: props.row.url }}">Öppna</router-link>
      </b-table-column>
    </b-table>
  </section>
</template>

<script>
export default {
  name: "UserList",
  props:  {users: Array},
  data() {
    return {
      isEmpty: Boolean,
      loading: true,
    }
  },
  async created() {
    this.loading = false;
    console.log(this.users)
  },
  methods: {
  }
}
</script>

<style scoped>

</style>
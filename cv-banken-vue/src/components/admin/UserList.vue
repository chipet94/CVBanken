<template>
  <section>
    <b-table
        :data="isEmpty ? [] : users"
        :hoverable=true
        :loading=loading
        :mobile-cards=true
    >
      <b-table-column v-slot="props" field="id" label="Id">
        {{ props.row.user.id }}
      </b-table-column>
      <b-table-column v-slot="props" field="name" label="Namn">
        {{ props.row.user.firstName+ " " + props.row.user.lastName }}
      </b-table-column>
      <b-table-column v-slot="props" field="email" label="Email">
        {{ props.row.user.email}}
      </b-table-column>
      <b-table-column v-slot="props" field="name" label="Program">
        {{ props.row.user.categoryName }}
      </b-table-column>
      <b-table-column v-slot="props" field="searching" label="Söker LIA">
        {{ props.row.searching? "Ja" : "Nej" }}
      </b-table-column>
      <b-table-column v-slot="props" field="private" label="Privat">
        {{ props.row.private? "Ja" : "Nej" }}
      </b-table-column>
      <b-table-column v-slot="props" field="rank" label="Typ">
        {{ props.row.user.role}}
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
  data() {
    return {
      isEmpty: Boolean,
      loading: true,
      users: []
    }
  },
  async created() {
    await this.loadAllUsers()
  },
  methods: {
    async loadAllUsers(){
      await this.$store.dispatch("profile/getAllProfiles").then(
          res => {
            this.users = res;
          }
      )
      this.isEmpty = this.users < 1
      this.loading = false;
    }
  }
}
</script>

<style scoped>

</style>
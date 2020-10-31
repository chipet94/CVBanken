<template>
  <section>
    <b-dropdown
        position="is-bottom-left"
        append-to-body
        aria-role="menu">
      <a
          class="navbar-item"
          slot="trigger"
          role="button">
        <span>Meny</span>
        <b-icon icon="menu-down"></b-icon>
      </a>

      <b-dropdown-item custom aria-role="menuitem">
        Inloggad som:  <b>{{thisSession.name}}</b>
      </b-dropdown-item>
      <hr class="dropdown-divider">
      <b-dropdown-item v-if="thisSession.role === 'Admin'" has-link aria-role="menuitem">
        <router-link :to="{name: 'AdminDashboard'}">
          <b-icon icon="home"></b-icon>
          Admin Dashboard
        </router-link>
      </b-dropdown-item>
      <hr class="dropdown-divider">
      <b-dropdown-item has-link aria-role="menuitem">
        <router-link :to="{name: 'UserProfile'}">
          <b-icon icon="home"></b-icon>
          Profil
        </router-link>
      </b-dropdown-item>
      <hr class="dropdown-divider" aria-role="menuitem">
      <b-dropdown-item value="logout" aria-role="menuitem" @click="logout">
        <b-icon icon="logout"></b-icon>
        Logout
      </b-dropdown-item>
    </b-dropdown>
  </section>
</template>

<script>
export default {
  name: "UserMenu",
  computed:{
    thisSession(){
      return this.$store.getters["auth/getUser"]
    }
  },
  methods: {
    logout() {
      this.$store.dispatch("auth/logout").then({})
      window.location.replace("/")
    }
  }
}
</script>

<style scoped>

</style>
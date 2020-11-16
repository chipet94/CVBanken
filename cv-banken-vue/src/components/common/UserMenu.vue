<template>
  <section>
    <b-dropdown
        append-to-body
        aria-role="menu"
        position="is-bottom-left">
      <a
          slot="trigger"
          class="navbar-item"
          role="button">
        <span>Meny</span>
        <b-icon icon="menu-down"></b-icon>
      </a>

      <b-dropdown-item aria-role="menuitem" custom>
        Inloggad som: <b>{{ thisSession.name }}</b>
      </b-dropdown-item>
      <hr class="dropdown-divider">
      <b-dropdown-item v-if="thisSession.role === 'Admin'" aria-role="menuitem" has-link>
        <router-link :to="{name: 'AdminDashboard'}">
          <b-icon icon="home"></b-icon>
          Admin Dashboard
        </router-link>
      </b-dropdown-item>
      <hr class="dropdown-divider">
      <b-dropdown-item aria-role="menuitem" has-link>
        <router-link :to="{name: 'UserProfile'}">
          <b-icon icon="home"></b-icon>
          Profil
        </router-link>
      </b-dropdown-item>
      <hr class="dropdown-divider">
      <b-dropdown-item aria-role="menuitem" value="logout" class="has-text-danger" @click="logout">
        <b-icon icon="logout"></b-icon>
        Logout
      </b-dropdown-item>
    </b-dropdown>
  </section>
</template>

<script>
export default {
  name: "UserMenu",
  computed: {
    thisSession() {
      return this.$store.getters["auth/getSession"]
    }
  },
  methods: {
    logout() {
      this.$store.dispatch("auth/logout")
    }
  }
}
</script>

<style scoped>

</style>
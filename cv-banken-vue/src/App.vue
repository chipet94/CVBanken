<template>
  <div id="app">
    <div id="nav">
      <div v-if="!isLoggedIn">
        <router-link to="/login">Logga in |</router-link>
        <router-link to="/sign-up"> Registrera</router-link>
      </div>
      <div v-else>
        <router-link to="/profile">Profile</router-link>
        <router-link v-if="thisUser.role === 'Admin'" to="/admin_dashboard"> | Admin Dashboard</router-link>
        <button class="button is-danger is-pulled-right" @click="logout">Logga ut</button>
      </div>
    </div>
    <router-view/>
  </div>
</template>
<script>

export default {
  data() {
    return {
      currentUser: ""
    }
  },
  computed: {
    isLoggedIn() {
      return this.$store.state.auth.status.loggedIn
    },
    thisUser() {
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

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;
  background-color: #693250;
}

#nav a {
  font-weight: bold;
  color: white;
}

#nav a.router-link-exact-active {
  color: white;
}
</style>

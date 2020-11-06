<template>
  <div id="app">
    <b-navbar centered mobile-burger transparent wrapper-class="ITHS-nav">
      <template slot="brand">
        <div class="navbar-brand ITHS-brand">
          <img src="https://www.iths.se/wp-content/themes/stella/assets/images/logo.svg">
        </div>
      </template>

      <template slot="start">
        <b-navbar-item :to="{name: 'Home' }" tag="router-link">Start</b-navbar-item>
        <b-navbar-item :to="{ name: 'Utbildningar' }" tag="router-link">Utbildningar</b-navbar-item>
        <!--        <b-navbar-item :to="{ name: 'Educations' }" tag="router-link">Utbildningar2</b-navbar-item>-->
      </template>
      <template slot="end">

        <b-navbar-item tag="a">
          <div class="buttons">
            <login-container v-if="!isLoggedIn" class=""></login-container>
            <user-menu v-if="isLoggedIn" class=""></user-menu>
          </div>
        </b-navbar-item>
      </template>

    </b-navbar>
    <router-view/>
    <br>
    <iths-footer></iths-footer>
  </div>
</template>
<script>

import LoginContainer from "@/components/common/LoginContainer";
import UserMenu from "@/components/common/UserMenu";
import IthsFooter from "@/components/IthsFooter";

export default {
  components: {IthsFooter, UserMenu, LoginContainer},
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
      return this.$store.getters["auth/getSession"]
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

.ITHS-button-small {
  background-color: #693250;
  color: white;
  font-weight: bold;
}

.ITHS-title {
  color: #f1f1f1;
  font-size: 2rem;
  font-weight: 600;
  color: white;
  text-decoration-line: none;
}

.is-ITHS-purple {
  background-color: #693250;
}

.ITHS-nav {
  background-color: #693250;
  color: #f1f1f1;
  width: 100%;
  display: inherit;
  text-align: center;
}

.ITHS-nav a.navbar-item {
  color: #f1f1f1;
  font-weight: bold;
  text-align: center;
  margin-top: auto;
  margin-bottom: auto;
}

.ITHS-nav a.navbar-burger {
  margin-top: auto;
  margin-bottom: auto;
  color: white;
  position: absolute;
  top: auto;
  bottom: auto;
  right: 0;
  left: auto;
}

a.navbar-burger.burger :hover {
  background-color: #710642;
}

/*a.navbar-burger.burger{*/
/*  color: white;*/
/*}*/
.ITHS-brand {
  padding: 10px;
  margin-left: 20px;
  margin-bottom: 5px;
  margin-top: 5px;
}

.ITHS-nav .navbar-item {
  color: #f1f1f1;
  font-weight: bold;
  text-align: center;
  margin-top: auto;
  margin-bottom: auto;
  display: block;
  position: center;
  vertical-align: center;
}

.ITHS-nav .navbar-item:hover {
  color: white;
  background-color: rgba(102, 50, 78, 0.7);
}

.router-link-exact-active {
  background-color: #68495c;
}

.ITHS-Nav div a {
  color: #f1f1f1;
  font-weight: bold;
  text-align: center;
  margin-top: auto;
  margin-bottom: auto;
}

.ITHS-nav .navbar-menu {

}

.ITHS-nav div.navbar-menu.is-active {
  background-color: #6f435b;
}
</style>

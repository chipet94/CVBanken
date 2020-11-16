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
      </template>

      <template slot="end">
        <b-navbar-item tag="a">
            <login-container v-if="!isLoggedIn" class=""></login-container>
            <user-menu v-if="isLoggedIn" class=""></user-menu>
        </b-navbar-item>
      </template>

    </b-navbar>
    <message-box></message-box>
    <router-view/>
    <br>
    <iths-footer></iths-footer>
  </div>
</template>
<script>

import LoginContainer from "@/components/common/LoginContainer";
import UserMenu from "@/components/common/UserMenu";
import IthsFooter from "@/components/IthsFooter";
import MessageBox from "@/components/common/MessageBox";

export default {
  components: {MessageBox, IthsFooter, UserMenu, LoginContainer},
  data() {
    return {
      currentUser: "",
      polling: null
    }
  },
  created() {
    this.polling = this.checkLoginLoop()
  },
  computed: {
    isLoggedIn() {
      return this.$store.getters["auth/isloggedIn"]
    },
    thisUser() {
      return this.$store.getters["auth/getSession"]
    }
  },
  methods: {
    checkLoginLoop () {
      this.polling = setInterval(() => {
        if (this.isLoggedIn){
          this.$store.dispatch("auth/loggedIn")
        }

      }, 30000)
    },
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: gray();
}
</style>

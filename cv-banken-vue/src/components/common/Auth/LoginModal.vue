<template>
  <section class="container">
      <div class="modal-card">
        <header class="modal-card-head ITHS-background">
          <p class="modal-card-title ITHS-text">
            Logga in | Registrera
          </p>
          <button
              class="delete"
              type="button"
              @click="$emit('close')"/>
        </header>
        <div class="modal-card-body">
          <b-tabs v-model="activeTab" class="block" expanded position="is-centered" type="is-boxed">
            <b-tab-item>
              <template slot="header">
                <b-icon icon="information-outline"></b-icon>
                <span class="has-text-black"> Logga in</span>
              </template>
              <sign-in :on-success="onLogin"></sign-in>
            </b-tab-item>
            <b-tab-item>
              <template slot="header">
                <b-icon icon="information-outline"></b-icon>
                <span class="has-text-black"> Registrera</span>
              </template>
              <sign-up :on-success="registerSuccess"></sign-up>
            </b-tab-item>
          </b-tabs>
        </div>
      </div>
  </section>

</template>

<script>
import SignIn from "@/components/SignIn";
import SignUp from "@/components/SignUp";
export default {
  name: "LoginModal",
  components: {SignUp, SignIn},
  data() {
    return {
      activeTab: 0
    }
  },
  methods: {
    registerSuccess() {
      this.$buefy.toast.open({
        message: 'Konto skapat!',
        type: 'is-success'
      })
      this.activeTab = 0;
    },
    onLogin(){
      this.$emit("close")
    }
  }
}
</script>

<style scoped>
.ITHS-background {
  background: #693250;
}

.ITHS-text {
  color: white;
}

</style>
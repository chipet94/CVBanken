<template>
  <section>
    <a
        class="navbar-item"
        role="button"
        @click="isComponentModalActive = true">
      <span>Konto</span>
      <b-icon icon="menu-down"></b-icon>
    </a>

    <b-modal
        v-model="isComponentModalActive"
        :destroy-on-hide="false"
        aria-modal
        aria-role="dialog"
        has-modal-card
        trap-focus>
      <div class="modal-card">
        <header class="modal-card-head ITHS-background">
          <p class="modal-card-title ITHS-text">
            Logga in | Registrera
          </p>
          <button
              class="delete"
              type="button"
              @click="isComponentModalActive = false"/>
        </header>
        <div class="modal-card-body">
          <b-tabs v-model="activeTab" class="block" expanded position="is-centered" type="is-boxed">
            <b-tab-item>
              <template slot="header">
                <b-icon icon="information-outline"></b-icon>
                <span class="has-text-black"> Logga in</span>
              </template>
              <sign-in></sign-in>
            </b-tab-item>
            <b-tab-item>
              <template slot="header">
                <b-icon icon="information-outline"></b-icon>
                <span class="has-text-black"> Registrera</span>
              </template>
              <sign-up></sign-up>
            </b-tab-item>
          </b-tabs>
        </div>

      </div>
    </b-modal>
  </section>
</template>

<script>

import SignIn from "@/components/SignIn";
import SignUp from "@/components/SignUp";

export default {
  components: {
    SignUp,
    SignIn
  },
  data() {
    return {
      isComponentModalActive: false,
      activeTab: 0
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
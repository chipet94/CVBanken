<template>
  <section>
    <a
        class="navbar-item"
        role="button"
        @click="openLoginModal">
      <span>Konto</span>
      <b-icon icon="menu-down"></b-icon>
    </a>
  </section>
</template>

<script>

import LoginModal from "@/components/common/Auth/LoginModal";
export default {
  components: {
  },
  data() {
    return {
    }
  },
  methods: {
    openLoginModal() {
      this.$buefy.modal.open({
        parent: this,
        component: LoginModal,
        hasModalCard: true,
        customClass: 'custom-class custom-class-2',
        trapFocus: true
      })
    },
  }
}
</script>
<style scoped>
</style>
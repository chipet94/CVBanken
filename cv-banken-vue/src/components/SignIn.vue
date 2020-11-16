<template>
  <div>
    <div id="section" class="m-t-12">
      <div class="columns is-centered">
        <div class="container">
          <b-field label="E-post">
            <b-input v-model="input" required type="email"></b-input>
          </b-field>
          <b-field label="Lösenord">
            <b-input v-model="password" password-reveal required type="password" value=""></b-input>
          </b-field>
          <span class="has-text-danger">{{ message }}</span>
          <b-field>
            <b-button class="button ITHS-button" @click.native="signIn">Logga in</b-button>
          </b-field>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "SignIn",
  props:{onSuccess: Function},
  data() {
    return {
      input: "",
      password: "",
      message: ""
    };
  },
  computed: {
    getUser() {
      return this.$store.getters["auth/getSession"]
    }
  },
  methods: {
    async signIn() {
      await this.$store.dispatch("auth/login",
          {email: this.input, password: this.password})
          .then(() => {
            this.onSuccess();
            this.$router.push("/my_profile")
          })
          .catch(err => {
            this.message = err.response.data.error
          })
    },
  }
};
</script>
<style scoped>
/*Exempel css*/
#headline {
  color: red;
  font-size: 20px;
}

</style>
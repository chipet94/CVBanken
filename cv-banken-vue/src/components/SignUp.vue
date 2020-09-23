<template>
  <div>
    <h1 id="headline">Skapa ett konto med din iths mail, för att ladda upp ditt cv</h1>
    <span class="has-text-danger">{{ message }}</span>
    <div id="section" class="m-t-12">
      <div class="columns is-centered">
        <div class="column is-6">
          <div class="box">
            <div class="container">
              <b-field label="Förnamn">
                <b-input v-model="firstName" required type="text"></b-input>
              </b-field>
              <b-field label="Efternamn">
                <b-input v-model="lastName" required type="text"></b-input>
              </b-field>
              <b-field label="Utbildning">
                <b-select v-model="selected" placeholder="Välj din utbildning">
                  <option v-for="education in educations" v-bind:key="`eductation-${education.id}`"
                          v-bind:value="{ id: education.id, name: education.name }">
                    {{ education.name }}
                  </option>
                </b-select>
              </b-field>
              <b-field label="E-post">
                <b-input id="emailfield" v-model="input" required type="email"></b-input>
              </b-field>
              <b-field label="Lösenord">
                <b-input v-model="password" password-reveal required type="password" value=""></b-input>
              </b-field>
              <b-field>
                <b-input type="button" value="Registrera" @click.native="signUp"></b-input>
              </b-field>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>

export default {
  name: "SignUp",
  mounted() {
    this.getProgrammes()
  },
  data() {
    return {
      selected: "",
      educations: [],
      input: "",
      firstName: "",
      lastName: "",
      password: "",

      message: ""
    };
  },
  methods: {
    async signUp() {
      await this.$store.dispatch("auth/register",
          {
            "firstName": this.firstName,
            "lastName": this.lastName,
            "email": this.input,
            "password": this.password,
            "programmeId": this.selected.id
          })
          .then(() => {
            alert("Success!")
            this.$router.push("/login")
          })
          .catch(err => {
            this.message = err.response.data.error
          })
    },
    async getProgrammes() {
      this.educations = await this.$store.dispatch("edu/getAll").catch(err => alert(err))
    },
  }
}
</script>
<style scoped>
/*Exempel css*/
#headline {
  color: red;
  font-size: 20px;
}
</style>
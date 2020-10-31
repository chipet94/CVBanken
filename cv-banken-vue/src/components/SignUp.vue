<template>
  <div>
    <h1 id="headline">Skapa ett konto med din iths mail, för att ladda upp ditt cv</h1>
    <div id="section" class="m-t-12">
      <div class="columns is-centered">
        <div class="column is-6">
              <span class="has-text-danger">{{ errors.FirstName }}</span>
              <b-field label="Förnamn">
                <b-input v-model="firstName" required type="text"></b-input>
              </b-field>
              <span class="has-text-danger">{{ errors.LastName }}</span>
              <b-field label="Efternamn">
                <b-input v-model="lastName" required type="text"></b-input>
              </b-field>
              <span class="has-text-danger">{{ errors.ProgrammeId }}</span>
              <b-field label="Utbildning">
                <b-select v-model="programme" placeholder="Välj din utbildning">
                  <option v-for="education in educations"
                          :key="education.name"
                          :value="education.id">
                    {{ education.name }}
                  </option>
                </b-select>
              </b-field>
              <span class="has-text-danger">{{ errors.Email }}</span>
              <b-field label="E-post">
                <b-input id="emailfield" v-model="input" required type="email"></b-input>
              </b-field>
              <span class="has-text-danger">{{ errors.Password }}</span>
              <b-field label="Lösenord">
                <b-input v-model="password" password-reveal required type="password" value=""></b-input>
              </b-field>
              <b-field>
                <b-button class="button is-purple text is-black" @click.native="signUp" :disabled="locked">Registrera</b-button>
              </b-field>
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
      programme: '',
      educations: [],
      input: "",
      firstName: "",
      lastName: "",
      password: "",
      locked: false,
      errors: {}
    };
  },
  methods: {
    async signUp() {
      this.locked = true;
      console.log(this.programme)
      await this.$store.dispatch("auth/register",
          {
            "firstName": this.firstName,
            "lastName": this.lastName,
            "email": this.input,
            "password": this.password,
            "programmeId": this.programme
          })
          .then(() => {
            alert("Success!")
            this.$router.push("/login")
          })
          .catch(err => {
            this.errors = err.response.data.errors
            this.locked = false;
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
  padding-top: 2rem;
  padding-bottom: 2rem;
  color: black;
  font-size: 20px;
}
.button{
  background-color: #693250;
}
</style>
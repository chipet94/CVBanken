<template>
  <div>
    <h1 id="headline">Skapa ett konto med din iths mail, för att ladda upp ditt cv</h1>
    <div id="section" class="m-t-12">
      <div class="columns is-centered">
        <div class="column">
          <span class="has-text-danger">{{ errors.FirstName }}</span>
          <b-field label="Förnamn">
            <b-input v-model="firstName" required type="text"></b-input>
          </b-field>
          <span class="has-text-danger">{{ errors.LastName }}</span>
          <b-field label="Efternamn">
            <b-input v-model="lastName" required type="text"></b-input>
          </b-field>
          <span class="has-text-danger">{{ errors.ProgrammeId }}</span>
          <b-field class="" grouped label="Utbildning">
            <b-field horizontal label="Program" label-position="left">
              <b-select v-model="category" placeholder="Utbildningskategori" @input="categoryChanged">
                <option v-for="cat in categories"
                        :key="cat.name"
                        :value="cat">
                  {{ cat.name }}
                </option>
              </b-select>
              <div v-if="category">
                <b-field horizontal label="klass">
                  <b-select v-model="programme" :value="null" placeholder="Välj klass">
                    <option v-for="prog in category.programmes"
                            :key="prog.name"
                            :value="prog.id">
                      {{ prog.name }}
                    </option>
                  </b-select>
                </b-field>

              </div>
            </b-field>
          </b-field>
          <span class="has-text-danger">{{ errors.Email }}</span>
          <span class="has-text-danger">{{ errors.email }}</span>
          <b-field label="E-post">
            <b-input id="emailfield" v-model="email" required type="email"></b-input>
          </b-field>
          <span class="has-text-danger">{{ errors.Password }}</span>
          <b-field label="Lösenord">
            <b-input v-model="password" password-reveal required type="password" value=""></b-input>
          </b-field>
          <b-field>
            <b-button :disabled="locked" class="button ITHS-button" @click.native="signUp">Registrera</b-button>
          </b-field>
        </div>
      </div>
    </div>
  </div>
</template>
<script>

export default {
  name: "SignUp",
  props: {onSuccess: Function},
  mounted() {
    this.getProgrammes()
  },
  data() {
    return {
      category: null,
      programme: null,
      educations: [],
      email: "",
      firstName: "",
      lastName: "",
      password: "",
      locked: false,
      errors: {}
    };
  },
  computed: {
    categories() {
      return this.$store.getters["edu/getCategories"] ?? []
    },
    programmes() {
      return this.$store.getters["edu/getAllProgrammes"] ?? []
    }
  },
  methods: {
    categoryChanged() {
      this.programme = null;
    },
    async signUp() {
      this.locked = true;
      await this.$store.dispatch("auth/register",
          {
            "firstName": this.firstName,
            "lastName": this.lastName,
            "email": this.email,
            "password": this.password,
            "programmeId": this.programme
          })
          .then(() =>
            this.onSuccess())
          .catch(err => {
            this.errors = err.response.data.errors
            console.log(this.errors)
            this.locked = false;
          })
    },
    async getProgrammes() {
      await this.$store.dispatch("edu/getAllCategories").catch(err => alert(err))
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

.ITHS-button {
  background-color: #693250;
  width: 100%;
  height: 5rem;
  color: white;
  font-size: large;
  font-weight: bold;
}
</style>
<template>
  <section>
    <div class="modal-card" style="">
      <header class="modal-card-head" style="background-color: #693250; color:white; ">
        <b-button v-if="hasChanged" class="is-small" style="background-color: #693250; border: none" title="Återställ"
                  @click="reset">
          <b-icon icon="reload" style="color: white"></b-icon>
        </b-button>
        <p class="modal-card-title" style="color: white">Konto inställningar</p>
        <button
            class="delete"
            type="button"
            @click="$emit('close')"/>
      </header>
      <section class="modal-card-body">
        <div class="card-content" style="background-color: white">
          <div class="content">
            <span v-if="request.private" class="has-text-danger">Med privat profil kan endast du och administratörer se din profil.</span>
            <b-field class="" label="Ska din profil vara privat?">
              <div class="field">
                <b-switch v-model="request.private">
                  {{ request.private ? "Profilen ska vara privat." : "Profilen ska vara synlig." }}
                </b-switch>
              </div>
            </b-field>
            <b-field label="Söker du LIA?">
              <div class="field">
                <b-switch v-model="request.searching">{{ request.searching ? "Ja!" : "Nej!" }}</b-switch>
              </div>
            </b-field>

            <span class="has-text-danger">{{ errors.FirstName }}</span>
            <b-field label="Förnamn">
              <b-input v-model="request.firstName" required type="text"></b-input>
            </b-field>
            <span class="has-text-danger">{{ errors.LastName }}</span>
            <b-field label="Efternamn">
              <b-input v-model="request.lastName" required type="text"></b-input>
            </b-field>
            <span class="has-text-danger">{{ errors.ProgrammeId }}</span>
            <b-field class="" grouped label="Utbildning">
              <b-field horizontal label="Program" label-position="left">
                <b-select v-model="category" :value="userCategory" placeholder="Utbildningskategori"
                          @input="categoryChanged">
                  <option v-for="cat in categories"
                          :key="cat.name"
                          :value="cat">
                    {{ cat.name }}
                  </option>
                </b-select>
                <div v-if="category">
                  <b-field horizontal label="klass">
                    <b-select v-model="request.programmeId" :value="null" placeholder="Välj klass">
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
            <b-field label="E-post">
              <b-input id="email" v-model="request.email" required type="email"></b-input>
            </b-field>
            <span class="has-text-danger">{{ errors.Password }}</span>
            <b-field label="Nytt lösenord">
              <b-input v-model="request.password" :validation-message="errors.Password" password-reveal type="password"
                       value=""></b-input>
            </b-field>
            <b-field>
              <b-button :disabled="!hasChanged" class="ITHS-button-small" @click="send">Spara</b-button>
            </b-field>
          </div>
        </div>
      </section>
    </div>
  </section>
</template>

<script>
import User from "@/models/User";
import {updateProfileModel} from "@/models/requests/UpdateProfileRequest";

export default {
  name: "SettingsModal",
  props: {user: User.prototype},
  data() {
    return {
      educations: [],
      isActive: false,
      category: this.userCategory,
      request: {
        firstName: this.user.firstName,
        lastName: this.user.lastName,
        email: this.user.email,
        programmeId: this.user.programmeId,
        // oldPassword: '',
        password: '',
        searching: this.user.searching,
        private: this.user.private
      },
      locked: false,
      changed: false,
      errors: {}
    }
  },
  computed: {
    categories() {
      return this.$store.getters["edu/getCategories"]
    },
    userCategory() {
      return this.$store.getters["edu/getCategoryFromName"](this.user.categoryName)
    },
    currentUserProgramme() {
      return {id: this.user.programmeId, name: this.user.programme}
    },
    hasChanged() {
      return this.request.firstName !== this.user.firstName
          || this.request.lastName !== this.user.lastName
          || this.request.email !== this.user.email
          || this.request.programmeId !== this.user.programmeId
          || this.request.password.length > 1
          || this.request.private !== this.user.private
          || this.request.searching !== this.user.searching
          && !this.locked;
    }
  },
  async mounted() {
    await this.getProgrammes();
    this.reset();
  },
  methods: {
    reset() {
      this.category = this.userCategory;
      this.request = {
        firstName: this.user.firstName,
        lastName: this.user.lastName,
        email: this.user.email,
        programmeId: this.user.programmeId,
        // oldPassword: '',
        password: '',
        private: this.user.private,
        searching: this.user.searching,
      }
    },
    categoryChanged() {
      this.programme = null;
    },
    async send() {
      this.locked = true;
      let _request = updateProfileModel(this.request);
      await this.$store.dispatch("profile/updateProfile", _request).then(this.$emit('close')).catch(err => {
        alert(err)
      })
      this.locked = false;
    },
    async getProgrammes() {
      await this.$store.dispatch("edu/getAllCategories")
    },
  }
}
</script>

<style scoped>
.ITHS-button-small {
  background-color: #693250;
  color: white;
  font-weight: bold;
  border: none;
}
</style>
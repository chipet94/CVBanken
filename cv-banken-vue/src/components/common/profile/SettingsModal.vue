<template>
  <section>
    <b-button @click="isActive = true">
      <b-icon icon="cog" style="color:black"></b-icon>
    </b-button>
    <b-modal
        :active.sync="isActive"
        :destroy-on-hide="true"
        aria-modal
        aria-role="dialog"
        has-modal-card
        onclose="this.$emit('close')"
        trap-focus>
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
              @click="isActive = false"/>
        </header>
        <section class="modal-card-body">
          <div class="card-content" style="background-color: white">
            <div class="content">
              <span v-if="request.private" class="has-text-danger">Med privat profil kan endast du och administratörer se din profil.</span>
              <b-field class="" label="Ska din profil vara synlig?">
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
              <b-field label="Utbildning">
                <b-select v-model="request.programmeId" :loading="educations.length < 0 "
                          :placeholder="request.programme">
                  <option v-for="education in educations"
                          :key="education.name"
                          :value="education.id">
                    {{ education.name }}
                  </option>
                </b-select>
              </b-field>
              <span class="has-text-danger">{{ errors.Email }}</span>
              <b-field label="E-post">
                <b-input id="email" v-model="request.email" required type="email"></b-input>
              </b-field>
              <span class="has-text-danger">{{ errors.Password }}</span>
              <b-field label="Nuvarande lösenord">
                <b-input v-model="request.oldPassword" :validation-message="errors.Password" password-reveal type="password"
                         value=""></b-input>
              </b-field>
              <b-field label="Nytt lösenord">
                <b-input v-model="request.password" :validation-message="errors.Password" password-reveal type="password"
                         value=""></b-input>
              </b-field>
              <b-field>
                <b-button :disabled="!hasChanged" class="is-success" @click="send">Spara</b-button>
              </b-field>
            </div>
          </div>
        </section>
      </div>
    </b-modal>
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
      request: {
        firstName: this.user.firstName,
        lastName: this.user.lastName,
        email: this.user.email,
        programmeId: this.user.programmeId,
        oldPassword: '',
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
  created() {
    this.getProgrammes();
    this.reset();
  },
  methods: {
    reset() {
      this.request = {
        firstName: this.user.firstName,
        lastName: this.user.lastName,
        email: this.user.email,
        programmeId: this.user.programmeId,
        oldPassword: '',
        password: '',
        private: this.user.private,
        searching: this.user.searching,
      }
    },
    async send() {
      this.locked = true;
      let _request = updateProfileModel(this.request);
      console.log(_request)
      await this.$store.dispatch("profile/updateProfile", _request).then(this.isActive = false).catch(err => {
        alert(err)
      })
      this.locked = false;
    },
    async getProgrammes() {
      this.educations = await this.$store.dispatch("edu/getAll").catch(err => alert(err))
    },
  }
}
</script>

<style scoped>

</style>
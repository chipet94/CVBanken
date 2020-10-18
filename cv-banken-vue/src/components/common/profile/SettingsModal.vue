<template>
<section>
  <b-button @click="isActive = true">
    <b-icon icon="cog" style="color:black"></b-icon>
  </b-button>
  <b-modal
      :active.sync="isActive"
      :destroy-on-hide="true"
      onclose="this.$emit('close')"
      aria-modal
      aria-role="dialog"
      has-modal-card
      trap-focus>
    <div class="modal-card" style="">
      <header class="modal-card-head" style="background-color: #693250; color:white; ">
        <b-button class="is-small" v-if="hasChanged" @click="reset" style="background-color: #693250; border: none" title="Återställ">
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
            <b-field label="Ska din profil vara synlig?" class="">
                <div class="field">
                  <b-switch v-model="request.private">{{ request.private ? "Profilen ska vara privat." : "Profilen ska vara synlig." }}</b-switch>
                </div>
            </b-field>
            <b-field label="Söker du LIA?">
              <div class="field">
                <b-switch v-model="request.searching">{{ request.searching ? "Ja!" : "Nej!" }}</b-switch>
              </div>
            </b-field>

            <span class="has-text-danger">{{ errors.FirstName }}</span>
            <b-field label="Förnamn">
              <b-input v-model="request.user.firstName" required type="text"></b-input>
            </b-field>
            <span class="has-text-danger">{{ errors.LastName }}</span>
            <b-field label="Efternamn">
              <b-input v-model="request.user.lastName" required type="text"></b-input>
            </b-field>
            <span class="has-text-danger">{{ errors.ProgrammeId }}</span>
            <b-field label="Utbildning">
              <b-select :loading="educations.length < 0 " v-model="request.user.programmeId" :placeholder="request.user.programme">
                <option v-for="education in educations"
                        :key="education.name"
                        :value="education.id">
                  {{ education.name }}
                </option>
              </b-select>
            </b-field>
            <span class="has-text-danger">{{ errors.Email }}</span>
            <b-field label="E-post">
              <b-input id="email" v-model="request.user.email" required type="email"></b-input>
            </b-field>
            <span class="has-text-danger">{{ errors.Password }}</span>
            <b-field label="Nuvarande lösenord">
              <b-input v-model="request.user.oldPassword" password-reveal type="password" value="" :validation-message="errors.Password" ></b-input>
            </b-field>
            <b-field label="Nytt lösenord">
              <b-input v-model="request.user.password" password-reveal type="password" value="" :validation-message="errors.Password" ></b-input>
            </b-field>
            <b-field>
              <b-button class="is-success" :disabled="!hasChanged" @click="send">Spara</b-button>
            </b-field>
          </div>
        </div>
      </section>
    </div>
  </b-modal>
</section>
</template>

<script>
import {Profile} from "@/models/Profile";
import User from "@/models/User";
import {updateProfileModel} from "@/models/requests/UpdateProfileRequest";
export default {
  name: "SettingsModal",
  props: {profile : Profile, user: User},
  data() {
    return {
      educations: [],
      isActive: false,
      request: {
        user:{
          firstName: this.user.firstName,
          lastName: this.user.lastName,
          email: this.user.email,
          programmeId: this.user.programmeId,
          oldPassword: '',
          password: ''},
        searching: false, private:false},
      locked: false,
      changed: false,
      errors: {}
    }
  },
  computed:{
    currentUserProgramme(){
      return {id: this.user.programmeId, name: this.user.programme}
    },

    // hasChanged(){
    //   return this.request.private === this.profile.private
    //   ? this.request.searching !== this.profile.searching
    //       : !this.locked
    // },
    hasChanged(){
      return this.request.user.firstName !== this.user.firstName
          || this.request.user.lastName !== this.user.lastName
          || this.request.user.email !== this.user.email
          || this.request.user.programmeId !== this.user.programmeId
          || this.request.user.password.length > 1
          || this.request.private !== this.profile.private
          || this.request.searching !== this.profile.searching
          && !this.locked;
    }

  },
  created() {
    this.getProgrammes();

  },
  methods:{
    reset(){
      this.request.private = this.profile.private;
      this.request.searching = this.profile.searching;
      this.request.user = {
        firstName: this.user.firstName,
        lastName: this.user.lastName,
        email: this.user.email,
        programmeId: this.user.programmeId,
        oldPassword: '',
        password: ''
      }
    },
    hasChangedX(){
      return this.profile.private === this.private
      // ? this.profile.searching === this.searching
      // : false;
    },
    async send(){
      this.locked = true;
      let _request = updateProfileModel(this.request);
      console.log(_request)
      await this.$store.dispatch("profile/updateProfile",_request).then(this.isActive = false).catch(err => {
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
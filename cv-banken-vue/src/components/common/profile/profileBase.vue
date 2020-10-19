<template>
  <section class="container is-size-10">
    <div v-if="profile !== null" class="card is-centered">
      <div class="card-header" role="button">
        <p class="card-header-title is-centered">
          <template v-if="!loading && profile !== null">Profile - {{user.firstName + " " + user.lastName }}
          </template>
          <b-skeleton :active="loading" size="is-large"></b-skeleton>
        </p>
        <SettingsModal v-if="canEdit && user != null" class="is-pulled-right" :profile="thisProfile" :user="thisUser"></SettingsModal>
      </div>
      <div class="card-content">
        <template v-if="!loading && profile !== null">


        <div class="content tile is-vertical">
          <div class="tile">
            <div class="tile is-parent is-vertical">
              <article class="tile is-child notification is-centered">
                <profile-picture-box v-model="profilePicture" :on-reload="onReloadPic"
                                     :profile-picture="profilePicture"></profile-picture-box>
                <label>Uppgifter</label>
                <b-field horizontal label="Förnamn:">
                  {{ user.firstName }}
                </b-field>
                <b-field horizontal label="Efternamn:">
                  {{ user.lastName }}
                </b-field>
                <b-field horizontal label="Email:">
                  {{ user.email }}
                </b-field>
                <b-field horizontal label="Klass:">
                  {{ user.class_name }}
                </b-field>
                <b-field horizontal label="Söker LIA:">
                  {{ profile.searching ? "ja" : "nej" }}
                </b-field>
              </article>
            </div>
            <div class="tile is-parent">
              <article class="tile is-child notification">
                <b-button v-if="canEdit" class="is-pulled-right" @click="editDescription = !editDescription">
                  <b-icon :icon="editDescription? 'close' : 'cog'" style="color:black"></b-icon>
                </b-button>
                <label>Om</label>
                <pre v-if="!editDescription" class="text has-text-left" style="white-space: pre-wrap;">{{ profile.description }}</pre>
                <textarea aria-label="Om" v-if="editDescription" v-model="profile.description" class="textarea"></textarea>
                <b-button v-if="editDescription" class="is-success" @click="updateDescription">Spara</b-button>
              </article>
            </div>
          </div>
          <div class="tile is-parent">
            <article class="tile is-child notification">
              <div class="content">
                <user-file-list v-if="profile.userId !== undefined" :user-id="profile.userId" :can-edit="canEdit"></user-file-list>
              </div>
            </article>
          </div>
        </div>
        </template>
      </div>
    </div>
  </section>
</template>

<script>
//import User from "@/models/User";
import SettingsModal from "@/components/common/profile/SettingsModal";
import {Profile} from "@/models/Profile";
import ProfilePictureBox from "@/components/common/profile/ProfilePictureBox";
import userFileList from "@/components/common/profile/userFileList";
import User from "@/models/User";

export default {
  name: "profileBase",
  components: {userFileList, ProfilePictureBox, SettingsModal},
  props: {},
  data() {
    return {
      loading: false,
      canEdit: false,
      isEmpty: false,
      editDescription: false,
      profile: Profile.prototype,
      user: User.prototype
    };
  },
  computed: {
    thisProfile(){
      return this.profile;
    },
    thisUser(){
      return this.user;
    },
    profilePicture() {
      return this.profile.profilePicture ?
          this.profile.profilePicture !== '' ?
              this.profile.profilePicture
              : null
          : null
    },
    // user(){
    //   return new User(this.profile.getUser())
    // },
    //
    // profile() {
    //   return new Profile(this.$store.getters["profile/getProfile"])
    // },
    descriptionMode() {
      return this.editDescription ? "textarea" : "text"
    }
  },
  async created() {
    this.loading = true;
    //await this.$store.dispatch("profile/getUserProfile")
    await this.LoadProfile()
  },
  methods: {
    async updateDescription(){
      await this.$store.dispatch("profile/updateProfile", {description: this.profile.description})
          .then(this.editDescription = false)
          .catch(err => {
            alert(err)
          })
    },
    async onReloadPic() {
      await this.profile.getProfilePicture()
      await this.LoadProfile()
    },
    async LoadProfile() {
      this.loading = true;
      if (this.$route.params.url){
        await this.$store.dispatch("profile/getByUrl", this.$route.params.url).then(res => {
          this.profile = new Profile(res);
        })
        console.log(this.profile)
      }else{
        await this.$store.dispatch("profile/getUserProfile").then(res =>{
          this.profile = new Profile(res);
         this.canEdit = true
        })
      }
      if (this.profile){
        await this.profile.getProfilePicture();
        await this.profile.getUser().then(
            res => {
              this.user = new User(res);
            });
      }

      this.loading = false;
    }
  },
}
</script>

<style scoped>

</style>
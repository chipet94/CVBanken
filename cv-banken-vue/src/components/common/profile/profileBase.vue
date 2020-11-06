<template>
  <section v-if="thisUser !== undefined" class="container is-size-10">
    <div v-if="!loading" class="card is-centered">
      <div class="card-header" role="button">
        <p class="card-header-title is-centered">
          <template v-if="!loading && thisUser !== null">Profile - {{ thisUser.firstName + " " + thisUser.lastName }}
          </template>
          <b-skeleton :active="loading" size="is-large"></b-skeleton>
        </p>
        <!--        <SettingsModal v-if="!loading && canEdit && thisUser !== undefined" :user="thisUser"-->
        <!--                       class="is-pulled-right"></SettingsModal>-->
        <b-button v-if="!loading && canEdit && thisUser !== undefined" @click="openSettings">
          <b-icon icon="cog" style="color:black"></b-icon>
        </b-button>
      </div>
      <div class="card-content">
        <template v-if="!loading && thisUser !== null">
          <div class="content tile is-vertical">
            <div class="tile">
              <div class="tile is-parent is-vertical">
                <article class="tile is-child notification is-centered">
                  <profile-picture-box v-model="thisUser.profilePicture" :can-edit="canEdit" :on-reload="onReloadPic"
                                       :profile-picture="thisUser.profilePicture"></profile-picture-box>
                  <p class="subtitle">Uppgifter</p>
                  <b-field horizontal label="Namn:">
                    {{ thisUser.firstName }} {{ thisUser.lastName }}
                  </b-field>
                  <b-field horizontal label="Email:">
                    {{ thisUser.email }}
                  </b-field>
                  <b-field horizontal label="Klass:">
                    {{ thisUser.class_name }} / {{ thisUser.categoryName }}
                  </b-field>
                  <b-field horizontal label="Söker:">
                    <b-tag :class="thisUser.searching?'is-success': 'is-danger'" rounded
                           style="float: left; font-weight: bold; font-size: 1rem">
                      {{ thisUser.searching ? "ja" : "nej" }}
                    </b-tag>
                  </b-field>
                </article>
              </div>
              <div class="tile is-parent">
                <article class="tile is-child notification">
                  <b-button v-if="canEdit" class="is-pulled-right" @click="editDescription = !editDescription">
                    <b-icon :icon="editDescription? 'close' : 'cog'" style="color:black"></b-icon>
                  </b-button>
                  <p class="subtitle">Om</p>
                  <pre v-if="!editDescription" class="text has-text-left" style="white-space: pre-wrap;">{{
                      thisUser.description
                    }}</pre>
                  <textarea v-if="editDescription" v-model="thisUser.description" aria-label="Om"
                            class="textarea"></textarea>
                  <b-button v-if="editDescription" class="is-success" @click="updateDescription">Spara</b-button>
                </article>
              </div>
            </div>
            <div class="tile is-parent">
              <article class="tile is-child notification">
                <div class="content">
                  <user-file-list v-if="thisUser.id !== undefined" :can-edit="canEdit"
                                  v-bind:user="thisUser"></user-file-list>
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

import ProfilePictureBox from "@/components/common/profile/ProfilePictureBox";
import userFileList from "@/components/common/profile/userFileList";
import User from "@/models/User";

export default {
  name: "profileBase",
  components: {userFileList, ProfilePictureBox},
  props: {editable: Boolean, url: String},
  data() {
    return {
      loading: true,
      canEdit: this.editable ?? false,
      isEmpty: false,
      editDescription: false,
      user: User.prototype
    };
  },
  computed: {
    thisUser() {
      return this.$store.getters["profile/getProfile"](this.url ?? this.$route.params.url)
    },
    thisSession() {
      return this.$store.getters["auth/getSession"];
    },
    profilePicture() {
      return this.thisUser.profilePicture ?
          this.thisUser.profilePicture !== '' ?
              this.thisUser.profilePicture
              : null
          : null
    },
    descriptionMode() {
      return this.editDescription ? "textarea" : "text"
    }
  },
  async created() {
    await this.LoadProfile()
    console.log("UserModel: ", this.thisUser)
  },
  methods: {
    async updateDescription() {
      await this.$store.dispatch("profile/updateProfile", {description: this.thisUser.description})
          .then(this.editDescription = false)
          .catch(err => {
            alert(err)
          })
    },
    openSettings() {
      this.$buefy.modal.open({
        parent: this.$parent,
        component: SettingsModal,
        props: {user: this.thisUser},
        hasModalCard: true,
        customClass: '',
        trapFocus: true
      })
    },

    async onReloadPic() {
      await this.thisUser.getProfilePicture()
      // await this.LoadProfile()
    },
    async LoadProfile() {
      this.loading = true;
      let profileUrl = this.$route.params.url ?? this.url;
      if (profileUrl) {
        await this.$store.dispatch("profile/getByUrl", profileUrl).catch(err => {
          console.log(err)
          this.$router.push({name: "Error"})
        });
      } else {
        console.log("nothing")
      }
      if (this.thisUser.profilePicture === null) {
        await this.$store.dispatch("profile/getProfilePicture", profileUrl)
      }

      this.loading = false;
    }
  },
}
</script>

<style scoped>
.ITHS-button-small {
  background-color: #693250;
  color: white;
  font-weight: bold;
}

</style>
<template>
  <div>
    <div class="card-header" role="button">
      <p class="card-header-title">
        <template v-if="!loading">Profile</template>
        <b-skeleton :active="loading" size="is-large"></b-skeleton>
      </p>
    </div>
    <div class="card-content">
      <div class="content">
        <template v-if="!loading">
          <b-field label="Förnamn">
            {{ user.firstName }}
          </b-field>
          <b-field label="Efternamn">
            {{ user.lastName }}
          </b-field>
          <b-field label="Email">
            {{ user.email }}
          </b-field>
          <b-field label="Utbildning">
            {{ programme.name }}
          </b-field>
        </template>
        <b-skeleton :active="loading" :count="2" size="is-large"></b-skeleton>
      </div>
    </div>
    <span>(just placeholders)</span>
    <footer class="card-footer">
      <a class="card-footer-item">
        <template v-if="!loading">Save</template>
        <b-skeleton :active="loading" size="is-large"></b-skeleton>
      </a>
      <a class="card-footer-item">
        <template v-if="!loading">Edit</template>
        <b-skeleton :active="loading" size="is-large"></b-skeleton>
      </a>
      <a class="card-footer-item">
        <template v-if="!loading">Delete</template>
        <b-skeleton :active="loading" size="is-large"></b-skeleton>
      </a>
    </footer>
  </div>
</template>

<script>
import User from "@/models/User";

export default {
  name: "UserCard",
  props: {user: User},
  data() {
    return {
      programme: {},
      loading: false,
    };
  },
  async created() {
    if (this.user.programmeId)
      this.programme = await this.$store.dispatch("edu/getById", this.user.programmeId)
  }
}
</script>

<style scoped>

</style>
<template>
  <div class="container">
    <div class="card">
      <div class="card-image">
      </div>
      <div class="card-content">
        <div class="media">
          <div class="media-left">
          </div>
          <div class="media-content">
            <p class="title is-4">{{ thisProgramme.name }}</p>
            <p class="subtitle">{{ thisProgramme.categoryName }}</p>
          </div>
        </div>

        <div class="content">
          <b-field label="Studenter">
            <students-list :programme-id="thisProgramme.id"></students-list>
          </b-field>

        </div>
      </div>
    </div>
  </div>
</template>

<script>
import StudentsList from "@/components/common/Lists/StudentsList";

export default {
  name: "ProgrammeViewModel",
  components: {StudentsList},
  data() {
    return {
      loading: false
    }
  },
  computed: {
    thisProgramme() {
      return this.$store.getters["edu/getProgrammeFromName"](this.$route.params.name)
    },
    thisStudents() {
      if (this.thisProgramme !== undefined)
        return this.$store.getters["edu/studentsInProgramme"](this.thisProgramme.id)
      else
        return null;
    }
  },
  async created() {
    await this.getProgramme()
  },
  methods: {
    async getProgramme() {
      if (this.$route.params.name) {
        this.loading = true;
        await this.$store.dispatch("edu/getByName", this.$route.params.name).then(
            res => {
              console.log(res.id)
              this.$store.dispatch("edu/getStudentsIn", res.id)
            }
        )
      }
      console.log(this.thisProgramme)
    }
  },

}
</script>

<style scoped>

</style>
<template>
  <div class="container">
    <div class="card">
      <div class="card-image">
      </div>
      <div v-if="thisProgramme !== undefined" class="card-content">
        <div class="media" v-if="!loading">
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
      let prog = this.$store.getters["edu/getProgrammeFromName"](this.$route.params.name)
      return prog?? undefined;
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
              console.log(res)
              this.$store.dispatch("edu/getStudentsIn", res.id)
            }
        ).catch( err => {
          console.log("eerr", err)
          alert(err)
        })
        this.loading = false;
      }
    }
  },

}
</script>

<style scoped>

</style>
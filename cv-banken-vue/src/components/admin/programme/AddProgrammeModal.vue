<template>
  <section class="container">
    <div class="modal-card" style="">
      <header class="modal-card-head">
        <p class="modal-card-title">{{ editMode ? 'Redigera Klass' : "Ny Klass" }}</p>
        <button
            class="delete is-danger"
            type="button"
            @click="$emit('close')"/>
      </header>
      <section class="modal-card-body">
        <b-field label="Namn">
          <b-input
              v-model="programme.name"
              :value="programme.name"
              placeholder="Klass namn"
              required
              type="text">
          </b-input>
        </b-field>
        <b-field label="Ort">
          <b-select v-model="programme.location" placeholder="Ort">
            <option v-for="loc in locations"
                    :key="loc"
                    :value="loc">
              {{ loc }}
            </option>
          </b-select>
        </b-field>
        <b-field label="Kategori">
          <b-select v-model="programme.categoryId" placeholder="Utbildningskategori">
            <option v-for="cat in categories"
                    :key="cat.name"
                    :value="cat.id">
              {{ cat.name }}
            </option>
          </b-select>
        </b-field>
        <b-field label="Start">
          <b-datepicker
              v-model="programme.start"
              :show-week-number="true"
              :value="programme.start"
              icon="calendar-today"
              locale="sv-SE"
              placeholder="Click to select..."
              trap-focus>
          </b-datepicker>
        </b-field>
        <b-field label="Slut">
          <b-datepicker
              v-model="programme.end"
              :show-week-number="true"
              :value="programme.end"
              icon="calendar-today"
              locale="sv-SE"
              placeholder="Click to select..."
              trap-focus>
          </b-datepicker>
        </b-field>
        <b-field horizontal label="Gömd?">
          <b-checkbox-button
              v-model="programme.hidden"
              :native-value="false"
          >
            {{ programme.hidden ? "Gömd" : "Synlig" }}
          </b-checkbox-button>
        </b-field>
      </section>
      <footer class="modal-card-foot">
        <b-button class="button ITHS-button-small" @click="CreateProgramme">Spara</b-button>
        <b-button v-if="editMode" class="is-danger" @click="RemoveProgramme">Tabort</b-button>
      </footer>
    </div>
  </section>

</template>

<script>
export default {
  name: "AddProgrammeModal",
  props: {defaultProgramme: Object, editMode: Boolean, onDeleted: Function},
  data() {
    return {
      programme: {
        id: null,
        categoryId: null,
        location: null,
        name: null,
        start: null,
        end: null,
        hidden: false
      },
      locations: ["Stockholm", "Göteborg"]
    }
  },
  created() {
    if (this.defaultProgramme !== undefined) {
      this.programme = {
        id: this.defaultProgramme.id,
        categoryId: this.defaultProgramme.categoryId,
        location: this.defaultProgramme.location,
        name: this.defaultProgramme.name,
        start: new Date(this.defaultProgramme.start),
        end: new Date(this.defaultProgramme.end),
        hidden: this.defaultProgramme.hidden
      }
    }
  },
  computed: {
    categories() {
      return this.$store.getters["edu/getCategories"]
    }
  },
  methods: {
    async CreateProgramme() {
      if (this.editMode === true) {
        await this.$store.dispatch("edu/updateProgramme", this.programme).then(
            res => {
              console.log(res);
              this.$emit("close")
            }
        ).catch(err => console.log(err))
      } else {
        await this.$store.dispatch("edu/createProgramme", this.programme).then(
            res => {
              console.log(res);
              this.$emit("close")
            }
        ).catch(err => console.log(err))
      }

    },
    async RemoveProgramme() {
      if (confirm("Radera '" + this.programme.name + "'?")) {
        await this.$store.dispatch("edu/deleteProgramme", this.programme.id).then(
            res => {
              console.log(res);
              this.onDeleted(this.defaultProgramme);
              this.$emit("close")
            }
        )
      }
    }
  }
}
</script>

<style scoped>


</style>
<template>
  <section class="container">
    <div class="modal-card" style="width: auto">
      <header class="modal-card-head">
        <p class="modal-card-title">Ny Kategori</p>
        <button
            class="delete"
            type="button"
            @click="$emit('close')"/>
      </header>
      <section class="modal-card-body">
        <b-field label="Namn">
          <b-input
              v-model="category.name"
              :value="category.name"
              placeholder="Klass namn"
              required
              type="text">
          </b-input>
        </b-field>
        <b-field horizontal label="Gömd?">
          <b-checkbox-button
              v-model="category.hidden"
          >
            {{ category.hidden ? "Gömd" : "Synlig" }}
          </b-checkbox-button>
        </b-field>
      </section>
      <footer class="modal-card-foot">
        <button class="button is-success" @click="Create">Spara</button>
        <button class="button is-warning is-pulled-right" type="button" @click="$emit('close')">Stäng</button>
        <button class="button is-danger is-pulled-right" type="button" @click="Remove">Radera</button>
      </footer>
    </div>
  </section>

</template>

<script>
export default {
  name: "AddCategoryModal",
  props: {defaultCategory: Object, editMode: Boolean},
  data() {
    return {
      category: {
        id: null,
        name: null,
        hidden: false,
      }
    }
  },
  computed: {
    categories() {
      return this.$store.getters["edu/getCategories"]
    }
  },
  created() {
    if (this.defaultCategory !== undefined) {
      this.category = {
        id: this.defaultCategory.id,
        name: this.defaultCategory.name,
        hidden: this.defaultCategory.hidden,
      }
    }
  },
  methods: {
    async Create() {
      if (this.editMode === true) {
        await this.$store.dispatch("edu/updateCategory", this.category).then(
            res => {
              console.log(res)
              this.$emit("close")
            }
        ).catch(err => console.log(err))
      } else {
        await this.$store.dispatch("edu/createCategory", this.category).then(
            res => {
              console.log(res)
              this.$emit("close")
            }
        ).catch(err => console.log(err))
      }
    },
    async Remove() {
      if (confirm("Radera " + this.category.name + "?")) {
        await this.$store.dispatch("edu/deleteCategory", this.category.id).then(
            res => {
              console.log(res)
              this.$emit("close")
            }
        ).catch(err => console.log(err))
      }

    }
  }
}
</script>

<style scoped>

</style>
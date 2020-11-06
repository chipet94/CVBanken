<template>
  <section class="container">
    <div class="modal-card" style="width: auto">
      <header class="modal-card-head">
        <p class="modal-card-title">{{ editMode ? 'Redigera Kategori' : "Ny kategori" }}</p>
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
              placeholder="Kategori namn"
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
        <button class="button ITHS-button-small" @click="SaveCategory">Spara</button>
        <button v-if="editMode" class="button is-danger is-pulled-right" type="button" @click="RemoveCategory">Radera
        </button>
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
    async SaveCategory() {
      if (this.editMode === true) {
        await this.$store.dispatch("edu/updateCategory", this.category).then(
            () => {
              this.$emit("close")
            }
        ).catch(err => console.log(err))
      } else {
        await this.$store.dispatch("edu/createCategory", this.category).then(
            () => this.$emit("close")
        ).catch(err => console.log(err))
      }
    },
    async RemoveCategory() {
      if (confirm("Radera " + this.category.name + "?")) {
        await this.$store.dispatch("edu/deleteCategory", this.category.id).then(
            () => {
              this.$emit("close")
            }
        ).catch(err => console.log(err))
      }

    }
  }
}
</script>

<style scoped>
.ITHS-button-small {
  background-color: #693250;
  color: white;
  font-weight: bold;
}

</style>
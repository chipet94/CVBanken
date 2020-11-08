<template>
  <div class="container is-centered">
    <b-field label="Skapa" position="is-centered">
      <b-button class="ITHS-button" @click="openAddProgramme">Skapa ny klass</b-button>
      <b-button class="ITHS-button" @click="openAddCategory">Skapa ny Kategori</b-button>
    </b-field>

    <b-field class="is-inline-block" label="Välj kategori">
      <b-select v-model="selected_category" placeholder="Utbildningskategori">
        <option v-for="cat in categories"
                :key="cat.name"
                :value="cat.id">
          {{ cat.name }}
        </option>
      </b-select>

      <b-button v-if="this.category" class="ITHS-button-small" @click="openEditCategory">Redigera</b-button>
    </b-field>
    <div v-if="selected_category" class="container">
      <b-field :label="'Klasser i ' + category.name">
        <b-table
            v-if="category"
            :data="category.programmes < 1? [] : category.programmes"
            :hoverable=true
            :mobile-cards=true
        >
          <b-table-column v-slot="props" field="id" label="Id">{{ props.row.id }}</b-table-column>
          <b-table-column v-slot="props" field="name" label="Namn">{{ props.row.name }}</b-table-column>
          <b-table-column v-slot="props" field="category" label="Kategori">{{
              props.row.categoryName
            }}
          </b-table-column>
          <b-table-column v-slot="props" field="start" label="Start">{{ props.row.start }}</b-table-column>
          <b-table-column v-slot="props" field="end" label="Slut">{{ props.row.end }}</b-table-column>
          <b-table-column v-slot="props" field="students" label="Studenter">{{
              props.row.totalStudents
            }}
          </b-table-column>
          <b-table-column v-slot="props" field="hidden" label="Gömd">{{
              props.row.hidden ? "ja" : "nej"
            }}
          </b-table-column>
          <b-table-column v-slot="props" field="" label="">
            <b-button class="ITHS-button-small" @click="openEditProgramme(category.programmes[props.index])">Redigera
            </b-button>
          </b-table-column>
        </b-table>
      </b-field>

    </div>
    <br>
  </div>
</template>

<script>
import AddProgrammeModal from "@/components/admin/programme/AddProgrammeModal";
import AddCategoryModal from "@/components/admin/programme/AddCategoryModal";

export default {
  name: "AdminProgrammeContainer",
  components: {},
  data() {
    return {
      selected_category: null,
      programme: null,
      editModalActive: false,
      modalContent: null
    }
  },
  created() {
    this.load();
  },
  computed: {
    categories() {
      return this.$store.getters["edu/getCategories"]
    },
    category() {
      if (this.selected_category) {
        return this.$store.getters["edu/getCategory"](this.selected_category)
      }
      return undefined;

    }
  },
  methods: {
    setSelectedCategory(index) {
      this.selected_category = index.id;
    },
    programmeDeleted(programme) {
      this.category.programmes.pop(p => p === programme)
    },
    openAddProgramme() {
      this.$buefy.modal.open({
        parent: this,
        component: AddProgrammeModal,
        hasModalCard: true,
        customClass: 'custom-class custom-class-2',
        trapFocus: true
      })
    },
    openEditProgramme(programme) {
      console.log(programme)
      this.$buefy.modal.open({
        parent: this,
        component: AddProgrammeModal,
        props: {defaultProgramme: programme, editMode: true, onDeleted: this.programmeDeleted},
        hasModalCard: true,
        customClass: 'custom-class custom-class-2',
        trapFocus: true
      }, programme)
    },

    openAddCategory() {
      this.$buefy.modal.open({
        parent: this,
        component: AddCategoryModal,
        hasModalCard: true,
        customClass: 'custom-class custom-class-2',
          trapFocus: true
        })
      },
      openEditCategory() {
        this.$buefy.modal.open({
          parent: this,
          component: AddCategoryModal,
          props: {defaultCategory: this.category, editMode: true},
        hasModalCard: true,
        customClass: 'custom-class custom-class-2',
        trapFocus: true
      })
    },
    load() {
      this.$store.dispatch("edu/getAllCategories")
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

.ITHS-button {
  background-color: #693250;
  height: 5rem;
  color: white;
  font-size: large;
  font-weight: bold;
}

</style>
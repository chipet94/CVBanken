<template>
  <section>
    <b-button @click="isShowing = true">
      <b-icon icon="plus" style="color:white"></b-icon>
    </b-button>
    <b-modal
        v-model="isShowing"
        :destroy-on-hide="false"
        aria-modal
        aria-role="dialog"
        has-modal-card
        trap-focus class="is-8">
      <div class="modal-card" style="">
        <header class="modal-card-head" style="background-color: #693250; color:white; ">
          <p class="modal-card-title" style="color: white">Inställningar</p>
          <button
              class="delete"
              type="button"
              @click="isShowing= false"/>
        </header>
        <section class="modal-card-body">
          <div class="modal-card-content is-inline-block" style="background-color: white">
            <b-field horizontal label="Description" class="is-inline">
              <textarea aria-label="Description" cols="50" style="max-width: max-content; min-width: 550px" v-model="profile.description"></textarea>
            </b-field>

            <b-field>
              <b-button class="is-success">Spara</b-button>
            </b-field>
          </div>
        </section>
      </div>
    </b-modal>
  </section>
</template>
<script>

export default {
name: "editDescriptionModal",
  props: {profile : {}},
  data() {
    return {
      isShowing: false,
      formProps: {}
    }
  }
}
</script>

<style scoped>

</style>
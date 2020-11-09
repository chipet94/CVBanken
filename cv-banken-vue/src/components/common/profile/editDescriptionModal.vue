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
        class="is-8"
        has-modal-card trap-focus>
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
            <b-field class="is-inline" horizontal label="Description">
              <textarea v-model="profile.description" aria-label="Description" cols="50"
                        style="max-width: max-content; min-width: 550px"></textarea>
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
  props: {profile: {}},
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
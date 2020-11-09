<template>
  <div class="container">
    <b-field label="Nytt Meddelande">
      <div class="ITHS-message-container">
        <header>
          <span>{{messageRequest.text.length}}/ 125</span>
        </header>
        <span class="ITHS-message-detail">
                  <b-input
                      v-model="messageRequest.text"
                      :value="messageRequest.text"
                      placeholder="Meddelande..."
                      class="ITHS-message-input"
                      required
                      type="text"></b-input>
                  <span class="right">
              <b-button class="ITHS-button-small" title="Spara" @click="addMessage">
              <span class="">
                  <b-icon class="file-icon" icon="send"></b-icon>
                </span>
          </b-button>
                  </span>
        </span>


      </div>


    </b-field>
    <br>
    <div class="container">
      <b-field label="Messages">
        <b-table
            v-if="thisMessages"
            :data="thisMessages < 1? [] : thisMessages"
            :hoverable=true
            :mobile-cards=false>
          <b-table-column v-slot="props" field="id" label="Id">{{ props.row.id }}</b-table-column>
          <b-table-column v-slot="props" field="name" label="Text">{{ props.row.text }}</b-table-column>
          <b-table-column v-slot="props" field="controllers">
            <b-button class="is-danger" @click="removeMessage(props.row.id)" title="Tabort">
              <b-icon class="file-icon" icon="delete"></b-icon>
            </b-button>
          </b-table-column>
        </b-table>
      </b-field>
    </div>


  </div>
</template>

<script>
export default {
  name: "AdminSiteHome",
  data() {
    return {
      messageRequest: {
        text: ''
      }
    }
  },
  computed: {
    thisMessages() {
      return this.$store.getters["Site/getMessages"]
    },
    thisMessage() {
      return this.$store.getters["Site/getMessage"](this.currentIndex ?? 0)
    }
  },
  async created() {
    await this.$store.dispatch("Site/getMessages");
  },
  methods: {
    async addMessage() {
      console.log(this.messageRequest.text)
      if (this.messageRequest.text !== undefined && this.messageRequest.text.length > 0) {
        await this.$store.dispatch("Site/addMessage", this.messageRequest)
            .then(res => {
              console.log(res)
              this.$buefy.toast.open({
                message: 'Meddelande sparat!',
                type: 'is-success'
              })
              this.messageRequest.text = '';
              this.$store.dispatch("Site/getMessages")
            })
            .catch(err => {
                  console.log("err:", err.response)
                  this.$buefy.toast.open({
                    message: err.response.data.errors.text[0],
                    type: 'is-danger'
                  })
                }
            )
      } else
        this.$buefy.toast.open({
          message: 'Meddelandet får ej vara tomt.',
          type: 'is-danger'
        })


    },
    async removeMessage(id) {
      await this.$store.dispatch("Site/deleteMessage", id)
          .then(
              res => {
                console.log(res)
                this.$buefy.toast.open({
                  message: 'Meddelande borttaget.',
                  type: 'is-success'
                })
                this.$store.dispatch("Site/getMessages")
              }
          )
          .catch(err => {
            console.log(err)
            this.$buefy.toast.open({
              message: 'Gick ej att tabort.',
              type: 'is-danger'
            })
          })
    }
  }
}
</script>

<style scoped>
.ITHS-message-container {
  display: block;
  width: 100%;
  position: relative;
}

.right {
  position: relative;
  float: right;
}

.left {
  float: left;
}

.ITHS-message-detail {
  display: inline-flex;
  width: 100%;
}
.ITHS-message-input {
  width: 100%;
}
.ITHS-button-small {
  position: relative;
  display: inline;
  background-color: #693250;
  color: white;
  font-weight: bold;
}

</style>
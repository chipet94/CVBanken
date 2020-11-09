<template>
  <div class="ITHS-noticebox">
    <template>
      <div class="ITHS-notice-content" @mouseover="paused = true" @mouseleave="paused = false">
        <div class="ITHS-notice-header left">Notiser</div>
        <span class="ITHS-notice-message">
          <p class="ITHS-notice-message-text">        > {{thisMessage? thisMessage.text : "none"}}</p>
          <span class="right">
                      <button class="mdi mdi-chevron-left mdi-10px is-small ITHS-notice-button"
                              type="button"
                              @click="previousMessage">
            </button>
            <button class="mdi mdi-chevron-right mdi-10px is-small ITHS-notice-button"
                    type="button"
                    @click="nextMessage">
            </button>
          </span>

        </span>
      </div>
      <br>
    </template>

  </div>

</template>

<script>
export default {
  name: "MessageBox",
  data(){
    return {
      messages: ["hej", "hejpådig"],
      currentIndex: 0,
      polling: null,
      paused: false
    }
  },
  computed:{
    thisMessages(){
      return this.$store.getters["Site/getMessages"]
    },
    thisMessage(){
      return this.$store.getters["Site/getMessage"](this.currentIndex?? 0)
    }
  },
  async created() {
    await this.$store.dispatch("Site/getMessages");
    this.polling = this.loopMessages()
  },
  methods:{
    loopMessages () {
      this.polling = setInterval(() => {
        if (!this.paused){this.nextMessage()}

      }, 8000)
    },
    nextMessage(){
      if (this.currentIndex + 1 >= this.thisMessages.length)
        this.currentIndex = 0
      else
        this.currentIndex++;
    },
    previousMessage(){
      if (this.currentIndex - 1 < 0)
        this.currentIndex = this.messages.length
      else
        this.currentIndex--;
    }
  }
}
</script>

<style scoped>
.ITHS-noticebox{
  display: inline;
  background-color: lightgray;
  position: relative;
  color: white;
}
.ITHS-notice-content{
  background-color: lightgray;
  display: block;
  max-height: initial;
}
.columns{
  margin: 0;
}
.column{
  display: block;
  padding: 12px;
  vertical-align: center;
  justify-content: center;
  align-content: center;
  margin-top: auto;
  margin-bottom: auto;
}
.ITHS-overlay{
  display: inline;
  position: absolute;
  float: right;
}
.ITHS-notice-button{
  top: 0;
  opacity: 0.1;
  background-color: #693250;
  color: white;
  display:inline;
}
.ITHS-notice-content:hover .ITHS-notice-button{
  opacity: 0.8;
}
.right{
 float: right;
}
.left{
  float: left;
}
.ITHS-notice-header{
  padding: 6px 12px;
  display: block;
  float: left;
  background-color: #710642;
  text-align: center;
  color: white;
  font-weight: bold;
  margin-right: 12px;
}
.ITHS-notice-message{
  padding: 6px 12px;
  display: block;
  color: black;
  text-align: left;

}
.ITHS-notice-message-text{
  display: flow;
  height: 1.5rem;
  max-height: 1.5rem;
  overflow: hidden;
  overflow-wrap: anywhere;
  word-wrap: anywhere;
  word-break: break-all;
}
</style>
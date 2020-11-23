<template>
  <div id="ticker-wrap">
    <div id="ticker-announce" class="pull-left">Notiser</div>
    <div id="ticker_container">
      <div id="newsContent">
        <transition name="fade">
              <p v-show="!switching">{{message}}</p>
      </transition>

      </div>
      <div id="controls">

        <span id="pause_trigger" class="menu-icon-button" @click="paused = !paused">
          <i :class="!paused? 'fa fa-pause' : 'fa fa-play'"></i>
        </span>
        <span id="previous_trigger" class="menu-icon-nav"  @click="nextMessage(false)">
          <i class="fa fa-arrow-left"></i>
        </span>
        <span id="next_trigger" class="menu-icon-nav"  @click="nextMessage(false)">
          <i class="fa fa-arrow-right"></i>
        </span>
      </div>
    </div>
  </div>
</template>

<script>

const mBox = {
  props:['message'],
  template:`
        <div id="news">
            {{message}}
        </div>`
}
export default {
  name: "MessageWrapper",
  components:{
  },
  data(){
    return{
      switching: false,
      box: mBox,
      polling: null,
      currentIndex: 0,
      paused: false,
      timeout: 5000,
      message: ''
    }
  },
  async beforeCreate() {
    await this.$store.dispatch("Site/getMessages").then(()=> {
      this.message = this.thisMessage;
    });
    this.polling = this.loopMessages()
  },
  computed:{
    thisMessages(){
      return this.$store.getters["Site/getMessages"]
    },
    thisMessage(){
      let mess = this.$store.getters["Site/getMessage"](this.currentIndex?? 0);
      return mess? mess.text : ' '
    }
  },
  methods:{
    loopMessages () {
      this.polling = setInterval(() => {
        if (!this.paused){
            this.nextMessage();
        }
      }, this.timeout?? 5000)

    },
    async nextMessage(next = true){
      if (next){
        if (this.currentIndex + 1 >= this.thisMessages.length)
          this.currentIndex = 0
        else
          this.currentIndex++;
      }
      else
      {
        if (this.currentIndex - 1 < 0)
          this.currentIndex = this.thisMessages.length -1;
        else
          this.currentIndex--;
      }
      await this.updateMessage()
    },
    async updateMessage() {
      this.switching = true;
      setTimeout(() => {
        this.message = this.thisMessage ?? ' ';
        this.switching = false;
      }, 200);
    }
  },
}
</script>

<style scoped>
#ticker-wrap :hover * .menu-icon-nav{
  opacity: 1;

}
#controls span.menu-icon-nav{
  opacity: 0.2;
  margin-right: 2px;
  margin-left: 2px;
  color: #693250;
}
#controls span.menu-icon-button{
  color: #693250;
  margin-right: 5px;
  margin-left: 5px;
}
#controls span.menu-icon-button :hover, #controls span.menu-icon-nav :hover{
color: #710642;
  cursor: pointer;
  font-size: 110%;
}
.fade-enter-active, .fade-leave-active {
  transition: opacity 2s;
}
.fade-enter, .fade-leave-to /* .fade-leave-active below version 2.1.8 */ {
  opacity: 0;
}
#ticker-announce{
  text-align: left;
  -webkit-font-smoothing: antialiased;
  font-family: Average Sans,sans-serif;
  font-size: 95%;
  line-height: 40px;
  box-sizing: border-box;
  float: left!important;
  background-color: #693250;
  padding: 0 15px;
  color: #eee;
  margin-right: 15px;
  margin-left: -15px;
  text-transform: uppercase;
  font-weight: 600;
}
#ticker-wrap{
  text-align: left;
  -webkit-font-smoothing: antialiased;
  font-family: Average Sans,sans-serif;
  font-size: 95%;
  font-weight: 400!important;
  padding-right: 15px;
  padding-left: 15px;
  display: inline-block;
  max-width: 100%;
  width: 98%;
  position: relative;
  margin: 0 auto;
  border-top: darkgray 1px solid;
  background: #eee;
  line-height: 40px;
}
#ticker_container{
  text-align: left;
  -webkit-font-smoothing: antialiased;
  color: #333;
  line-height: 40px;
  box-sizing: border-box;
  font-size: 16px;
  width: 100%;
}
#newsContent{
  text-align: left;
  color: #333;
  line-height: 40px;
  font-size: 16px;
  box-sizing: border-box;
  float: left;
  width: 100%;
  max-width: 75%;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}
#controls{
  text-align: left;
  -webkit-font-smoothing: antialiased;
  color: #333;
  font-family: Average Sans,sans-serif;
  font-weight: 400!important;
  line-height: 40px;
  font-size: 16px;
  box-sizing: border-box;
  float: right;
  height: 16px;
  padding-right: 15px;
}
#news{
  display: block;
}
</style>
import Vue from 'vue'
import App from './App.vue'
import router from './router'
import Buefy from 'buefy'
import store from "@/store"
import 'buefy/dist/buefy.css'
import DisableAutocomplete from 'vue-disable-autocomplete';

Vue.use(DisableAutocomplete);


Vue.use(Buefy)

Vue.config.productionTip = false

new Vue({
    router,
    store,
    render: h => h(App)
}).$mount('#app')

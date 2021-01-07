import Vue from "vue";
import App from "./App.vue";
import router from "./router";

import { library } from "@fortawesome/fontawesome-svg-core";
import { faCoffee } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";

import {
  MdButton,
  MdContent,
  MdTabs,
  MdRadio,
  MdCard
} from "vue-material/dist/components";
import "vue-material/dist/vue-material.min.css";
import "vue-material/dist/theme/default.css";

import "es6-promise/auto";
import Vuex from "vuex";
import axios from "axios";

Vue.use(Vuex);

const url = "https://localhost:44365/api/values";

const store = new Vuex.Store({
  state: {
    coffeeList: []
  },
  mutations: {
    addCoffee(state, coffee) {
      axios.post(url, coffee)
      .then(_ => {
        console.log(coffee, _);
      });
      state.coffeeList.push(coffee);
    }
  }
});

library.add(faCoffee);

Vue.component("font-awesome-icon", FontAwesomeIcon);

Vue.use(MdButton);
Vue.use(MdContent);
Vue.use(MdTabs);
Vue.use(MdRadio);
Vue.use(MdCard);

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");

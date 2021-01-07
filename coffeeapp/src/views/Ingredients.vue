<template>
  <div class="ingredients">
    <h2>You want a {{ dimension }} coffee with...</h2>
    <div class="choices">
      <p>How much milk do you want?</p>
      <vue-slider
        v-model="value"
        class="slider"
        :data="[0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100]"
      />
      <p>How many spoons with sugar do you want?</p>
      <div class="options">
        <md-radio v-model="sugar" value="1">1</md-radio>
        <md-radio v-model="sugar" value="2">2</md-radio>
        <md-radio v-model="sugar" value="3">3</md-radio>
        <md-radio v-model="sugar" value="4">4</md-radio>
        <md-radio v-model="sugar" value="0">I don't want sugar</md-radio>
      </div>
      <p>How many splashes of flavour syrup do you want?</p>
      <div class="options">
        <md-radio v-model="syrup" value="1">1</md-radio>
        <md-radio v-model="syrup" value="2">2</md-radio>
        <md-radio v-model="syrup" value="3">3</md-radio>
        <md-radio v-model="syrup" value="4">4</md-radio>
        <md-radio v-model="syrup" value="0">I don't want syrup</md-radio>
      </div>
    </div>
    <p v-if="error" class="error">Please check both sugar and syrup levels!</p>
    <md-button v-on:click="submitRequest" class="md-raised md-accent"
      >Next step</md-button
    >
  </div>
</template>

<script>
import VueSlider from "vue-slider-component";
import "vue-slider-component/theme/antd.css";

export default {
  name: "milk",
  components: {
    VueSlider
  },
  data() {
    return {
      dimension: "",
      value: 20,
      sugar: -1,
      syrup: -1,
      error: false
    };
  },
  created() {
    this.dimension = this.$route.query.dimension;
  },
  methods: {
    submitRequest() {
      if (this.sugar == -1 || this.syrup == -1) {
        this.error = true;
      } else
        this.$router.push({
          name: "finish",
          query: {
            dimension: this.dimension,
            milk: this.value,
            sugar: this.sugar,
            syrup: this.syrup
          }
        });
    }
  }
};
</script>

<style scoped>
.md-button {
  margin-top: 20px;
  text-decoration: none;
  color: white;
  background: blue;
}
.error {
  color: pink;
}
.ingredients {
  padding: 10px 0px;
  text-align: center;
  font-weight: 300;
  font-size: 1.5em;
}
.choices {
  padding-top: 25px;
  width: calc(100vw - 250px);
  margin: auto;
}
.slider {
  width: 450px !important;
  margin: auto;
}
.choice {
  width: 50%;
  height: 100px;
  display: inline-block;
  text-align: center;
  padding: 30px;
  cursor: pointer;
}
</style>

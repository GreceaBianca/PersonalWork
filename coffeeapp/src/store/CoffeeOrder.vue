<template>
  <div class="coffee-card">
    <md-card>
      <md-card-header>
        <div class="md-title">{{ name }} wants a {{ dimension }} coffee</div>
      </md-card-header>
      <md-card-content>
        <p>Milk: {{ milk }}%</p>
        <p>Sugar: {{ sugar }} spoons</p>
        <p>Syrup: {{ syrup }} splashes</p>
      </md-card-content>
    </md-card>
  </div>
</template>

<script>
export default {
  name: "coffee-order",
  props: {
    dimension: String,
    milk: Number,
    sugar: String,
    syrup: String,
    name: String
  }
};
</script>

<style scoped>
.coffee-card {
  width: 500px;
  margin: auto;
  margin-bottom: 30px;
  background: linear-gradient(108deg, #d15169 0%, #a83d59 100%);
}
.md-card.md-theme-default {
  background: linear-gradient(108deg, #d15169 0%, #a83d59 100%);
}
</style>

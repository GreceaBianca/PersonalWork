<template>
  <div class="home">
    <font-awesome-icon icon="coffee" class="cup" />
    <p>Ready to order a coffee?</p>
    <router-link to="Size" class="btn">START</router-link>
    <div class="orders">
      <div class="offeredCoffees order">
        <h4>People which are sharing</h4>
      </div>
      <div class="requestedCoffees order">
        <h4>People who want a coffee</h4>
        <div
          class="ordered-coffes"
          v-for="coffee in $store.state.coffeeList"
          v-bind:key="coffee.name"
        >
          <CoffeeOrder
            v-bind:dimension="coffee.dimension"
            v-bind:milk="coffee.milk"
            v-bind:sugar="coffee.sugar"
            v-bind:syrup="coffee.syrup"
            v-bind:name="coffee.name"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import CoffeeOrder from "../store/CoffeeOrder";

export default {
  name: "home",
  props: ["coffee"],
  components: {
    CoffeeOrder
  },
  computed: {
    newCoffee() {
      if (this.coffee) {
        this.$store.commit("addCoffee", this.coffee);
      }
      return this.coffee;
    }
  }
};
</script>

<style scoped>
.home {
  padding: 100px 10px;
  text-align: center;
  font-weight: 300;
  font-size: 1.5em;
}
.cup {
  width: 90px;
  height: 90px;
}
.orders {
  display: flex;
  width: 100%;
  padding: 30px;
}
.order {
  flex-grow: 1;
  padding: 10px;
}
</style>

<template>
  <div class="finish">
    <font-awesome-icon icon="coffee" class="cup" />
    <div v-if="!orderCanBePlaced" class="getName">
      <p>Just one more step</p>
      <input
        class="input-getName"
        type="text"
        placeholder="Please fill in with your name"
        v-model="name"
      />
      <p v-if="error" class="error">You need to provide a name for the order</p>
      <md-button v-on:click="submitRequest" class="md-raised md-accent">
        Confirm
      </md-button>
    </div>
    <div v-if="orderCanBePlaced" class="placedOrder">
      <p>Your order has been placed</p>
      <CoffeeOrder
        v-bind:dimension="this.$route.query.dimension"
        v-bind:milk="this.$route.query.milk"
        v-bind:sugar="this.$route.query.sugar"
        v-bind:syrup="this.$route.query.syrup"
        v-bind:name="name"
      />
      <md-button v-on:click="orderAgain" class="md-raised md-accent">
        Order again?
      </md-button>
    </div>
  </div>
</template>

<script>
import CoffeeOrder from "../store/CoffeeOrder";
export default {
  name: "finish",
  data() {
    return {
      name: "",
      orderCanBePlaced: false,
      error: false
    };
  },
  components: {
    CoffeeOrder
  },
  methods: {
    submitRequest() {
      if (this.name) {
        this.orderCanBePlaced = true;
        this.error = false;
      } else {
        this.error = true;
      }
    },
    orderAgain() {
      this.$router.push({
        name: "home",
        params: {
          coffee: {
            dimension: this.$route.query.dimension,
            milk: this.$route.query.milk,
            sugar: this.$route.query.sugar,
            syrup: this.$route.query.syrup,
            name: this.name
          }
        }
      });
    }
  }
};
</script>

<style>
.getName {
  font-size: 30px;
}
.input-getName {
  width: 250px;
  height: 35px;
  padding: 5px;
}
.error {
  color: red;
  font-size: 20px;
}
.md-button {
  display: block;
  margin: auto;
  margin-top: 30px;
}
.finish {
  padding: 100px 10px;
  text-align: center;
  font-weight: 300;
  font-size: 1.5em;
}
.cup {
  width: 90px;
  height: 90px;
}
</style>

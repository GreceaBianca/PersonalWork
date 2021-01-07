<template>
  <div class="size">
    <p>What is the size of the coffee that you want?</p>
    <div class="choices">
      <div class="choice small" v-on:click="small">
        <font-awesome-icon icon="coffee" class="small-cup" />
        <p>Small</p>
      </div>
      <div class="choice large" v-on:click="large">
        <font-awesome-icon icon="coffee" class="large-cup" />
        <p>Large</p>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "size",
  methods: {
    small() {
      this.$router.push({ name: "ingredients", query: { dimension: "small" } }); //router can be called only inside App.vue, in other components you need to call this.$router
    },
    large() {
      this.$router.push({ name: "ingredients", query: { dimension: "large" } });
    }
  }
};
</script>

<style scoped>
.size {
  padding: 10px 0px;
  text-align: center;
  font-weight: 300;
  font-size: 1.5em;
}
.small-cup {
  width: 50px;
  height: 74px;
}
.large-cup {
  width: 90px;
  height: 74px;
}
.choices {
  height: 150px;
}
.choice {
  width: 50%;
  height: 150px;
  display: inline-block;
  text-align: center;
  padding: 19px;
  cursor: pointer;
}
.small {
  background: #9f4164;
}
.large {
  background: #6c2b59;
}
</style>

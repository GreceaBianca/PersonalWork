<template>
  <div class="nav">
    <div class="container">
      <transition name="slide-fade" mode="out-in">
        <h1>{{ title }}</h1>
      </transition>
    </div>
  </div>
</template>

<script>
export default {
  name: "Navbar",
  data() {
    return {
      title: "Welcome to the coffee app!"
    };
  }
};
</script>

<style scoped>
.nav {
  width: 100%;
  display: inline-block;
}
.container {
  background: linear-gradient(108deg, #d15169 0%, #a83d59 100%);
  padding: 40px 25px;
  font-weight: 500;
  font-size: 1.7em;
}
</style>

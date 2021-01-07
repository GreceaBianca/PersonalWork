<template>
  <div id="app">
    <Navbar />
    <router-view />
  </div>
</template>

<script>
import Navbar from "./components/Navbar";
export default {
  name: "App",
  components: {
    Navbar
  }
};
</script>

<style>
body {
  margin: 0;
  color: #f7d388;
  background: #401f45;
}
button,
a {
  text-decoration: none;
  color: white;
}
</style>

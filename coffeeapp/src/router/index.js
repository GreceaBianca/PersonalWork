import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";
import Size from "../views/Size.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "home",
    component: Home,
    props: true
  },
  {
    path: "/Size",
    name: "size",
    component: Size
  },
  {
    path: "/Ingredients",
    name: "ingredients",
    component: () => import("../views/Ingredients.vue"),
    props: true
  },
  {
    path: "/Finish",
    name: "finish",
    component: () => import("../views/Finish.vue"),
    props: true
  }
];

const router = new VueRouter({
  routes
});

export default router;

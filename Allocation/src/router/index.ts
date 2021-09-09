import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";
import Login from "@/views/Login.vue";
import Home from "@/components/Home.vue";

Vue.use(VueRouter);

export default new VueRouter({
    base: "/",
    mode: "history",
    routes: [
        {
            path: "/",
            name: "Login",
            component: Login
        },
        {
            path: "/Home",
            name: "Home",
            component: Home
        }
    ]
});

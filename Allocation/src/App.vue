<template>
    <div id="app" class="h-100">
        <router-view></router-view>
    </div>
</template>

<script lang="ts">
    import { Component, Vue } from "vue-property-decorator";
    import { BootstrapVue } from "bootstrap-vue";

    Vue.use(BootstrapVue);

    @Component({ })
    export default class App extends Vue { }
</script>
<style lang="scss">
    @import "node_modules/bootstrap/scss/bootstrap.scss";
    @import "node_modules/bootstrap-vue/src/index.scss";

    .border {
        border: 1px solid black;

        &__left {
            border: none;
            border-left: 1px solid black;
        }

        &__right {
            border: none;
            border-right: 1px solid black;
        }


        &--lightgrey {
            border-color: #8a8a8a;
        }
    }
    .h {
        &-10

    {
        height: 10%;
    }

    &-90 {
        height: 90%;
    }

    &-80 {
        height: 80%;
    }

    }

    body,
    html {
        height: 100% !important;
    }

    #app {
        font-family: Avenir, Helvetica, Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        color: #2c3e50;
    }

    #nav {
        padding: 30px;
        a

    {
        font-weight: bold;
        color: #2c3e50;
        &.router-link-exact-active

    {
        color: #42b983;
    }

    }
    }
</style>

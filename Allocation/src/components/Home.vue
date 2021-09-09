<template>
    <div class="w-100 h-100 logo">
        <div class="">Logged in as {{ profile.firstName }} {{ profile.lastname }}</div>
    </div>
  
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { ProfileModule } from "@/store/index";

@Component({})
export default class Home extends Vue {
  get profile() {
    return ProfileModule.profile;
  }
}
</script>

<style lang="scss">
</style>

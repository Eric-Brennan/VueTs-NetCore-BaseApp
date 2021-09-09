<template>
    <div form
         id="login-form"
         class="col-12 d-flex align-items-end h-100 wallpaper">
        <div 
             class="w-100 ">
            <div class="mb-5">
                <div class="col-12 p-0 d-flex justify-content-center">
                    <div class="col-6 col-md-4 col-lg-3 p-0 mb-3">
                        <b-form-input class="w-100"
                                      v-model="username"
                                      placeholder="Username"
                                      name="Username"
                                      cssClass="e-outline"
                                      floatLabelType="Auto"></b-form-input>
                    </div>
                </div>
                <div class="col-12 p-0 d-flex justify-content-center">
                    <div class="col-6 col-md-4 col-lg-3 p-0 mb-3" @keyup.enter="authenticate">
                        <b-form-input class="w-100"
                                     v-model="password"
                                     placeholder="Password"
                                     type="password"
                                     name="Password"
                                     cssClass="e-outline"
                                     floatLabelType="Auto"></b-form-input>
                    </div>
                </div>
                <div class="col-12 p-0 d-flex justify-content-center">
                    <div class="col-6 col-md-4 col-lg-3 p-0">
                        <b-button variant="outline-primary"
                                  @click="authenticate">
                            <span v-if="!authenticating">Login</span>
                            <span v-else
                                  class="spinner-border spinner-border-sm text-success"></span>
                        </b-button>
                    </div>
                </div>
            </div>
        </div>

    </div>
    
</template>

<script lang="ts">
    import { Component, Vue } from "vue-property-decorator";
    import { ProfileClient, IConfig  } from "@/api/api-client.ts";
    import { ProfileModule } from "@/store";
    import { ProfileAccount } from "../api/api-client";

    @Component({})
    export default class Login extends Vue {
        //Data
        username = "";
        password = "";

        authenticating = false;

        async mounted() {
            ProfileModule.updateProfile({ success: false } as ProfileAccount);

        }

        get apiProfileClient() {
            return new ProfileClient(new IConfig(""), "https://localhost:44396");
        }

        async authenticate() {

            this.authenticating = true;

            if (this.username !== "" && this.password.length >= 6) {
                
                try {
                    const response = await this.apiProfileClient.authenticateLogin({ username: this.username, password: this.password } as ProfileAccount);

                    if (response.httpStatusCode === 200) {
                        await ProfileModule.updateProfile(response.result!);
                        this.$router.push({
                            path: "/home"
                        });
                    }
                } catch (error) {
                    //
                } finally {
                    this.authenticating = false;
                }
            }
        }
    }
</script>

<style lang="scss">
    input {
        background-color: white !important;
    }

    .align-bottom {
        display: flex;
        flex-direction: column;
        justify-content: flex-end;
    }
</style>

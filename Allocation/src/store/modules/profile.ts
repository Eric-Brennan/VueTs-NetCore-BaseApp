import { Action, Module, Mutation, VuexModule } from "vuex-module-decorators";
import { ProfileAccount } from "@/api/api-client";

@Module({
  name: "Profile",
  namespaced: true
})
export default class Profile extends VuexModule {
    public profile: ProfileAccount = new ProfileAccount();

  @Mutation
  private async setProfile(profile: ProfileAccount) {
    this.profile = profile;
  }

  @Action
  public async updateProfile(profile: ProfileAccount) {
    await this.setProfile(profile);
  }
}

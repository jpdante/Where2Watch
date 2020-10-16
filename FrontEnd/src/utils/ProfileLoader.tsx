import { useEffect } from "react";
import AuthStore from "../undux/AuthStore";
import ProfileStore from "../undux/ProfileStore";
import Net from "./Net";

function ProfileLoader(props: any) {
  let authStore = AuthStore.useStore();
  let profileStore = ProfileStore.useStore();
  useEffect(() => {
    if (authStore.get("isAuthenticated") && !profileStore.get("loaded")) {
      Net.get("/api/account/get").then((e) => {
        if (e.data) {
          profileStore.set("loaded")(true);
          profileStore.set("email")(e.data.email);
          profileStore.set("username")(e.data.username);
          profileStore.set("picture")(e.data.picture);
        }
      });
    }
  });
  return null;
}

export default ProfileLoader;

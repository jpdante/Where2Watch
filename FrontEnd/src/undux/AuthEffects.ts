import { withReduxDevtools } from "undux";
import {
  deleteAuthenticationToken,
  setAuthenticationToken,
  hasAuthenticationToken,
} from "../utils/LocalStorage";
import { StoreEffects } from "./AuthStore";

let effects: StoreEffects = (store) => {
  store.on("token").subscribe((next) => {
    if (next === undefined || next === null) {
      deleteAuthenticationToken();
      store.set("isAuthenticated")(false);
    } else {
      setAuthenticationToken(next);
      store.set("isAuthenticated")(hasAuthenticationToken());
    }
  });
  return withReduxDevtools(store);
};

export default effects;

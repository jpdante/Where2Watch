import AuthStore from "../undux/AuthStore";
import { hasAuthenticationToken } from "./LocalStorage";
import Net from "./Net";

interface IProps {
  children?: any;
}

function SessionCheck(props: IProps) {
  let authStore = AuthStore.useStore();
  if (hasAuthenticationToken()) {
    Net.get("/api/auth/checksession").then((response) => {
      if (response.data && response.data.isValid === false) {
        authStore.set("token")(null);
      }
    });
  }
  return props.children;
}

export default SessionCheck;

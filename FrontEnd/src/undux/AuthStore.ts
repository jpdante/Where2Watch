import { Effects, Store, createConnectedStore } from "undux";
import { hasAuthenticationToken, getAuthenticationToken } from "../utils/LocalStorage";
import effects from "./AuthEffects";

type State = {
  isAuthenticated: boolean;
  token: string | null;
};

let initialState: State = {
  isAuthenticated: hasAuthenticationToken(),
  token: getAuthenticationToken(),
};

export default createConnectedStore(initialState, effects);

export type StoreProps = {
  store: Store<State>;
};

export type StoreEffects = Effects<State>
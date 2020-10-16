import { Store, createConnectedStore, withReduxDevtools } from "undux";

type State = {
  loaded: boolean;
  email: string;
  username: string;
  picture: string;
};

let initialState: State = {
  loaded: false,
  email: "default@default.com",
  username: "default",
  picture: "default",
};

export default createConnectedStore(initialState, withReduxDevtools);

export type StoreProps = {
  store: Store<State>;
};

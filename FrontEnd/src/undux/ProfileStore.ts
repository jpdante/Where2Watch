import { Store, createConnectedStore, withReduxDevtools } from "undux";

type State = {
  loaded: boolean;
  email: string;
  username: string;
  country: string;
  type: number;
};

let initialState: State = {
  loaded: false,
  email: "default@default.com",
  username: "default",
  country: "us",
  type: 0,
};

export default createConnectedStore(initialState, withReduxDevtools);

export type StoreProps = {
  store: Store<State>;
};

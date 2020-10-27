import { Store, createConnectedStore, withReduxDevtools } from "undux";

import { getCountryByLanguage } from "./../utils/Locale";

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
  country: getCountryByLanguage(),
  type: 0,
};

export default createConnectedStore(initialState, withReduxDevtools);

export type StoreProps = {
  store: Store<State>;
};

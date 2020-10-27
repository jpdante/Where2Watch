import { Store, createConnectedStore, withReduxDevtools } from "undux";
import Country from "../model/Country";

type State = {
  countries: Country[],
};

let initialState: State = {
  countries: [],
};

export default createConnectedStore(initialState, withReduxDevtools);

export type StoreProps = {
  store: Store<State>;
};

import { Store, createConnectedStore, withReduxDevtools } from "undux";

type State = {
  latestFeed: any[];
  mostViewedFeed: any[];
  mostLikedFeed: any[];
  relatedFeed: any[];
};

let initialState: State = {
  latestFeed: [],
  mostViewedFeed: [],
  mostLikedFeed: [],
  relatedFeed: [],
};

export default createConnectedStore(initialState, withReduxDevtools);

export type StoreProps = {
  store: Store<State>;
};

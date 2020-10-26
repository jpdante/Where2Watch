import { Store, createConnectedStore, withReduxDevtools } from "undux";

type State = {
  id: string;
  originalName: string;
  length: number;
  seasons: number;
  episodes: number;
  poster: string;
  summary: string;
  outline: string;
  likes: number;
  dislikes: number;
  imdbId: string;
  year: number;
  classification: string;
  genres: string[];
  availability: any[];
};

let initialState: State = {
  id: "",
  originalName: "",
  length: 0,
  seasons: 0,
  episodes: 0,
  poster: "",
  summary: "",
  outline: "",
  likes: 0,
  dislikes: 0,
  imdbId: "",
  year: 0,
  classification: "",
  genres: [],
  availability: [],
};

export default createConnectedStore(initialState, withReduxDevtools);

export type StoreProps = {
  store: Store<State>;
};

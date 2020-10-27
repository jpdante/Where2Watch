import { useEffect } from "react";
import SharedStore from "../undux/SharedStore";
import Net from "./Net";

function SharedLoader(props: any) {
  let sharedStore = SharedStore.useStore();

  useEffect(() => {
    Net.get("/api/shared/country/get").then((e) => {
      if (e.data) {
        sharedStore.set("countries")(e.data);
      }
    });
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  return null;
}

export default SharedLoader;

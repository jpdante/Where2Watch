import React, { useEffect } from "react";
import Poster from "../components/Poster";
import Platforms from "../components/Platforms";
import Ad from "../components/Ad";
import Net from "../utils/Net";
import TitleStore from "../undux/TitleStore";
import { useHistory, useParams } from "react-router-dom";
import ProfileStore from "../undux/ProfileStore";

interface IParams {
  id: string;
}

function Title(props: any) {
  let titleStore = TitleStore.useStore();
  let profileStore = ProfileStore.useStore();
  let history = useHistory();
  let params = useParams<IParams>();

  useEffect(() => {
    if (titleStore.get("imdbId") === params.id) return;
    Net.post("/api/title/get", { id: params.id, country: profileStore.get("country") })
      .then((e) => {
        if (e.data) {
          titleStore.set("imdbId")(e.data.imdbId);
          titleStore.set("id")(e.data.id);
          titleStore.set("originalName")(e.data.originalName);
          titleStore.set("length")(e.data.length);
          titleStore.set("seasons")(e.data.seasons);
          titleStore.set("episodes")(e.data.episodes);
          titleStore.set("poster")(e.data.poster);
          titleStore.set("summary")(e.data.summary);
          titleStore.set("outline")(e.data.outline);
          titleStore.set("likes")(e.data.likes);
          titleStore.set("dislikes")(e.data.dislikes);
          titleStore.set("year")(e.data.year);
          titleStore.set("classification")(e.data.classification);
          titleStore.set("genres")(e.data.genres);
          titleStore.set("availability")(e.data.availability);
        }
      })
      .catch((e) => {
        history.push("/");
      });
  });

  return (
    <div>
      <Ad />
      <Poster />
      <Platforms />
    </div>
  );
}

export default Title;

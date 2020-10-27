import React, { useEffect } from "react";
import Ad from "../components/Ad";
import Feed from "../components/Feed";
import FeedStore from "../undux/FeedStore";
import Net from "../utils/Net";

function Home(props: any) {
  const feedStore = FeedStore.useStore();

  useEffect(() => {
    Net.get("/api/feed/get").then((e) => {
      if(e.data) {
        feedStore.set("latestFeed")(e.data.latest)
        feedStore.set("mostViewedFeed")(e.data.mostViewed)
        feedStore.set("mostLikedFeed")(e.data.mostLiked)
      }
    });
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

    return (
      <div className="pt-3">
        <Ad />
        <div className="module">
          <h5 className="m-0 p-0">Last titles added</h5>
          <hr className="mt-1 hr-light" />
          <Feed
            data={feedStore.get("latestFeed")}
          />
        </div>
        <div className="module">
          <h5 className="m-0 p-0">Most viewed titles</h5>
          <hr className="mt-1 hr-light" />
          <Feed data={feedStore.get("mostViewedFeed")} />
        </div>
        <div className="module">
          <h5 className="m-0 p-0">Most liked titles</h5>
          <hr className="mt-1 hr-light" />
          <Feed data={feedStore.get("mostLikedFeed")} />
        </div>
      </div>
    );
}

export default Home;

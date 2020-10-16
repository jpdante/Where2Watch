import React, { Suspense } from "react";

import "./utils/I18next";
import "./utils/FontAwesome";

import AuthStore from "./undux/AuthStore";
import Routes from "./routes";

import Loading from "./components/Loading";
import SessionCheck from "./utils/SessionCheck";
import ProfileLoader from "./utils/ProfileLoader";
import ProfileStore from "./undux/ProfileStore";

function App() {
  return (
    <AuthStore.Container>
      <SessionCheck>
        <ProfileStore.Container>
          <ProfileLoader />
          <Suspense fallback={<Loading />}>
            <Routes />
          </Suspense>
        </ProfileStore.Container>
      </SessionCheck>
    </AuthStore.Container>
  );
}

export default App;

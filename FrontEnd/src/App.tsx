import React, { Suspense } from "react";

import "bootstrap";
import "jquery";
import "./style/global.scss";
import "./utils/I18next";
import "./utils/FontAwesome";

import AuthStore from "./undux/AuthStore";
import Routes from "./routes";

import Loading from "./components/Loading";
import SessionCheck from "./utils/SessionCheck";
import ProfileLoader from "./utils/ProfileLoader";
import ProfileStore from "./undux/ProfileStore";
import TitleStore from "./undux/TitleStore";
import SharedStore from "./undux/SharedStore";
import SharedLoader from "./utils/SharedLoader";

function App() {
  return (
    <AuthStore.Container>
      <SessionCheck>
        <ProfileStore.Container>
        <ProfileLoader />
          <SharedStore.Container>
          <SharedLoader />
            <TitleStore.Container>
              <Suspense fallback={<Loading />}>
                <Routes />
              </Suspense>
            </TitleStore.Container>
          </SharedStore.Container>
        </ProfileStore.Container>
      </SessionCheck>
    </AuthStore.Container>
  );
}

export default App;

import React from "react";
import { WithTranslation, withTranslation } from "react-i18next";

import Poster from "../components/Poster";
import Platforms from "../components/Platforms";
import Ad from "../components/Ad";

class Title extends React.Component<WithTranslation> {
  constructor(props: any) {
    super(props);
    this.state = {
      loading: true,
      videos: [],
    };
  }

  render() {
    return (
      <div>
        <Ad />
        <Poster />
        <Platforms />
      </div>
    );
  }
}

export default withTranslation()(Title);

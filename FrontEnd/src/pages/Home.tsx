import React from "react";
import { WithTranslation, withTranslation } from "react-i18next";

class Home extends React.Component<WithTranslation> {
  constructor(props: any) {
    super(props);
    this.state = {
      loading: true,
      videos: [],
    };
  }

  render() {
    return <div className="pt-3" style={{ height: "800px" }}></div>;
  }
}

export default withTranslation()(Home);

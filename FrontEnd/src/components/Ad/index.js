/* eslint-disable jsx-a11y/anchor-has-content */
import React from "react";

class Ad extends React.Component {
  componentDidMount() {
    //(window.adsbygoogle = window.adsbygoogle || []).push({});
  }

  render() {
    return (
      <div className="ad">
        <h4>Ad</h4>
        {/*<ins
          className="adsbygoogle"
          style={{ display: "block" }}
          data-ad-client="ca-pub-9071472716333274"
          data-ad-slot="6433256921"
          data-ad-format="auto"
          data-adtest="on"
          data-full-width-responsive="true"
        ></ins>*/}
      </div>
    );
  }
}

export default Ad;

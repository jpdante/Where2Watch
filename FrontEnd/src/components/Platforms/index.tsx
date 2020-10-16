/* eslint-disable jsx-a11y/anchor-has-content */
import React from "react";

import styles from "./platforms.module.scss";
import { withTranslation, WithTranslation } from "react-i18next";

interface IState {
  like: boolean;
  dislike: boolean;
}

class Poster extends React.Component<WithTranslation, IState> {
  constructor(props: any) {
    super(props);
    this.state = {
      like: false,
      dislike: false,
    };
  }

  like = async (e: any) => {};

  dislike = async (e: any) => {};

  render() {
    const { t } = this.props;
    return (
      <div className={`module ${styles.platforms}`}>
        <h5>{t("platforms.available")}</h5>
        <div className="row">
          <div className="col-md">
            <a
              className={`btn ${styles.button}`}
              style={{ backgroundImage: "url(/assets/svg/netflix.svg)" }}
              href="https://netflix.com/"
            />
          </div>
          <div className="col-md">
            <a
              className={`btn ${styles.button}`}
              style={{ backgroundImage: "url(/assets/svg/prime-video.svg)" }}
              href="https://www.primevideo.com/"
            />
          </div>
          <div className="col-md">
            <a
              className={`btn ${styles.button}`}
              style={{ backgroundImage: "url(/assets/svg/hulu.svg)" }}
              href="https://www.hulu.com/"
            />
          </div>
          <div className="col-md">
            <a
              className={`btn ${styles.button}`}
              style={{ backgroundImage: "url(/assets/svg/hbo-go.svg)" }}
              href="https://www.hbogo.com.br/"
            />
          </div>
          <div className="col-md">
            <a
              className={`btn ${styles.button}`}
              style={{ backgroundImage: "url(/assets/svg/telecine.svg)" }}
              href="https://www.telecineplay.com.br/"
            />
          </div>
        </div>
      </div>
    );
  }
}

export default withTranslation()(Poster);

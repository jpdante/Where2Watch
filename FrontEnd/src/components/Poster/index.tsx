import React from "react";

import styles from "./poster.module.scss";
import { withTranslation, WithTranslation } from "react-i18next";

import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Link } from "react-router-dom";

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
      <div className={`module ${styles.poster}`}>
        <div className="row">
          <div className="col-md-3">
            <img
              src="https://images-na.ssl-images-amazon.com/images/I/71rNJQ2g-EL._AC_SL1178_.jpg"
              className={styles.posterImage}
              alt="Show Poster"
            />
          </div>
          <div className={`col-md-9 ${styles.wrapper}`}>
            <div>
              <h2>Moonlight: Sob a Luz do Luar</h2>
              <h5>Moonlight</h5>
              <div className={styles.badges}>
                <span className="badge badge-dark">16</span>
                <span className="badge badge-dark">1h 51min</span>
                <span className="badge badge-dark">2016</span>
              </div>
              <div className={styles.badges}>
              <Link className="badge badge-primary" to="/category/drama">Drama</Link>
              </div>
            </div>
            <p>
              A young African-American man grapples with his identity and
              sexuality while experiencing the everyday struggles of childhood,
              adolescence, and burgeoning adulthood.
            </p>
            <div className={styles.bottom}>
              <div className="text-center">
                <button
                  type="button"
                  className={`btn btn-sm ${
                    this.state.like ? "btn-primary" : ""
                  }`}
                  onClick={this.like}
                >
                  <FontAwesomeIcon icon="thumbs-up" className={styles.icon} />
                  &nbsp;&nbsp; {t("poster.like")}
                </button>
                <button
                  type="button"
                  className={`btn btn-sm ${
                    this.state.dislike ? "btn-primary" : ""
                  }`}
                  onClick={this.dislike}
                >
                  <FontAwesomeIcon icon="thumbs-down" className={styles.icon} />
                  &nbsp;&nbsp; {t("poster.dislike")}
                </button>
              </div>
              <div className={`progress ${styles.progress}`}>
                <div
                  className={styles.progressBarBox}
                  style={{ width: "50%" }}
                  data-toggle="tooltip"
                  data-placement="top"
                  title="Tooltip on top"
                >
                  <div
                    className={`progress-bar bg-primary ${styles.progressBar}`}
                    role="progressbar"
                    aria-valuenow={50}
                    aria-valuemin={0}
                    aria-valuemax={100}
                  ></div>
                </div>
                <div
                  className={styles.progressBarBox}
                  style={{ width: "50%" }}
                  data-toggle="tooltip"
                  data-placement="top"
                  title="Tooltip on top"
                >
                  <div
                    className={`progress-bar bg-dark ${styles.progressBar}`}
                    role="progressbar"
                    aria-valuenow={50}
                    aria-valuemin={0}
                    aria-valuemax={100}
                  ></div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default withTranslation()(Poster);

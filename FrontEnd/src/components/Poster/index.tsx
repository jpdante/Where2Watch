import React, { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";
import TitleStore from "../../undux/TitleStore";

import styles from "./poster.module.scss";

function Poster(props: any) {
  let titleStore = TitleStore.useStore();
  let { t } = useTranslation();
  let [dislike, setDislike] = useState(false);
  let [like, setLike] = useState(false);

  return (
    <div className={`module ${styles.poster}`}>
      <div className="row">
        <div className="col-md-3">
          <img
            src={titleStore.get("poster")}
            className={styles.posterImage}
            alt="Poster"
          />
        </div>
        <div className={`col-md-9 ${styles.wrapper}`}>
          <div>
            <h2>{titleStore.get("originalName")}</h2>
            <h5>{titleStore.get("originalName")}</h5>
            <div className={styles.badges}>
              <span className="badge badge-dark">
                {titleStore.get("classification")}
              </span>
              <span className="badge badge-dark">
                {titleStore.get("length")} min
              </span>
              <span className="badge badge-dark">{titleStore.get("year")}</span>
            </div>
            <div className={styles.badges}>
              {titleStore.get("genres").map((genre, index) => (
                <Link
                  className="badge badge-primary"
                  to={`/category/${genre}`}
                  key={index}
                >
                  {genre}
                </Link>
              ))}
            </div>
          </div>
          <p>{titleStore.get("summary")}</p>
          <div className={styles.bottom}>
            <div className="text-center">
              <button
                type="button"
                className={`btn btn-sm ${like ? "btn-primary" : ""}`}
                //onClick={like}
              >
                <FontAwesomeIcon icon="thumbs-up" className={styles.icon} />
                &nbsp;&nbsp; {t("poster.like")}
              </button>
              <button
                type="button"
                className={`btn btn-sm ${dislike ? "btn-primary" : ""}`}
                //onClick={this.dislike}
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

export default Poster;

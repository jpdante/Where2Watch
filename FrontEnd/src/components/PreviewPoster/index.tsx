import React from "react";
import { Link } from "react-router-dom";

import styles from "./PreviewPoster.module.scss";

interface IProps {
  data: any;
}

function PreviewPoster(props: IProps) {
  return (
    <div className={styles.previewPoster}>
      <div className={styles.content}>
        <div className={styles.thumbnail}>
          <Link
            className={styles.thumbnailContainer}
            to={`/${props.data.imdbId}`}
          >
            <div className={styles.thumbnailContainer}>
              <img
                src={props.data.poster}
                alt="Video thumbnail"
                className={styles.thumbnailImage}
              />
            </div>
          </Link>
        </div>
      </div>
    </div>
  );
}

export default PreviewPoster;

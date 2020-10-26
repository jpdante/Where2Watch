/* eslint-disable jsx-a11y/anchor-has-content */
import React from "react";

import styles from "./platforms.module.scss";
import { useTranslation } from "react-i18next";
import TitleStore from "../../undux/TitleStore";

function Poster(props: any) {
  const titleStore = TitleStore.useStore();
  const { t } = useTranslation();
  return (
    <div className={`module ${styles.platforms}`}>
      <h5>{t("platforms.available")}</h5>
      <div className="row">
        {titleStore.get("availability").map((obj, index) => {
          return (
            <div className="col-md">
              <a
                className={`btn ${styles.button}`}
                style={{
                  backgroundImage: `url(/assets/svg/${obj.platform.icon}.svg)`,
                }}
                href={obj.link}
                key={index}
              />
            </div>
          );
        })}
      </div>
    </div>
  );
}

export default Poster;

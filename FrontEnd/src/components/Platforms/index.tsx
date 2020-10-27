/* eslint-disable jsx-a11y/anchor-has-content */
import React from "react";

import styles from "./platforms.module.scss";
import { useTranslation } from "react-i18next";
import TitleStore from "../../undux/TitleStore";
import CountrySelect from "../CountrySelect";
import ProfileStore from "../../undux/ProfileStore";
import Net from "../../utils/Net";

function Poster(props: any) {
  const titleStore = TitleStore.useStore();
  const profileStore = ProfileStore.useStore();

  function changeCountry(e: string) {
    profileStore.set("country")(e);
    Net.post("/api/availability/get", {
      id: titleStore.get("id"),
      country: e,
    }).then((e) => {
      if (e.data) {
        titleStore.set("availability")(e.data);
      }
    });
  }

  const { t } = useTranslation();
  return (
    <div className={`module ${styles.platforms}`}>
      <div className="text-center mb-1">
        <CountrySelect
          onSelect={changeCountry}
          value={profileStore.get("country")}
        />
      </div>
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

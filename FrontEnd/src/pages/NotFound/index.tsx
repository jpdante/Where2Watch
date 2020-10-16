import React from "react";
import { WithTranslation, withTranslation } from "react-i18next";

import styles from "./notfound.module.scss";

class NotFound extends React.Component<WithTranslation> {
  render() {
    const { t } = this.props;
    return (
      <div className="pt-3">
        <div className={`module ${styles.notfound}`}>
          <h2>{t("http.notfound")}</h2>
        </div>
      </div>
    );
  }
}

export default withTranslation()(NotFound);

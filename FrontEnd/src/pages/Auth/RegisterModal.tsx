import React from "react";
import $ from "jquery";
import { useHistory } from "react-router-dom";

import styles from "./Auth.module.scss";

function RegisterModal() {
  let history = useHistory();

  function goToLogin() {
    ($("#registerConfirmationModal") as any).modal("hide");
    history.push("/login");
  }

  return (
    <div
      className="modal show fade"
      id="registerConfirmationModal"
      tabIndex={-1}
      role="dialog"
      data-backdrop="static"
      data-keyboard="false"
      aria-labelledby="registerConfirmationModal"
      aria-hidden="true"
    >
      <div className="modal-dialog modal-dialog-centered" role="document">
        <div className={`modal-content ${styles.modalContent}`}>
          <div className={`modal-body ${styles.modalBody}`}>
            <div className={styles.svg}>
              <img src="assets/images/check.svg" alt="Check" />
            </div>
            We look forward to having you here.
            <br />
            First, we need you to confirm your account so check your email, if
            you can't find it check your spam box.
          </div>
          <div className={`modal-footer ${styles.modalFooter}`}>
            <button
              type="button"
              className={`btn ${styles.loginBtn}`}
              onClick={goToLogin}
            >
              Back
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default RegisterModal;

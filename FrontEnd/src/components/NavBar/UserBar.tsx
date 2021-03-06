/* eslint-disable jsx-a11y/anchor-is-valid */
import React from "react";
import { useTranslation } from "react-i18next";
import AuthStore from "../../undux/AuthStore";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Link } from "react-router-dom";

import styles from "./NavBar.module.scss";

function UserBar() {
  const { t } = useTranslation();
  let authStore = AuthStore.useStore();
  async function logOut() {
    authStore.set("token")(null);
  }

  return (
    <ul className="nav navbar-nav ml-auto w-100 justify-content-end">
      <li className="nav-item dropdown">
        {authStore.get("isAuthenticated") ? (
          <a
            className={`nav-link ${styles.profilePicture}`}
            href="#"
            data-toggle="dropdown"
            aria-haspopup="true"
            aria-expanded="false"
          >
            <div className={styles.pictureContainer}>
              <img
                src={`/assets/images/default.webp`}
                width="30"
                height="30"
                className="mx-auto d-inline-block align-top"
                alt="Profile"
              />
            </div>
          </a>
        ) : (
          <a
            className="nav-link"
            href="#"
            data-toggle="dropdown"
            aria-haspopup="true"
            aria-expanded="false"
          >
            <FontAwesomeIcon icon="user" />
          </a>
        )}
        <div
          className={`${styles.animate} ${styles.slideIn} ${styles.dropDown} dropdown-menu dropdown-menu-right`}
          aria-labelledby="navbarDropdownMenuLink"
        >
          {!authStore.get("isAuthenticated") && (
            <Link className="dropdown-item" to="/login">
              <FontAwesomeIcon icon="sign-in-alt" />
              Login
            </Link>
          )}
          {!authStore.get("isAuthenticated") && (
            <Link className="dropdown-item" to="/register">
              <FontAwesomeIcon icon="sign-in-alt" />
              Register
            </Link>
          )}
          {authStore.get("isAuthenticated") && (
            <a className="dropdown-item" onClick={logOut}>
              <FontAwesomeIcon icon="sign-out-alt" />
              Log out
            </a>
          )}
        </div>
      </li>
    </ul>
  );
}

export default UserBar;

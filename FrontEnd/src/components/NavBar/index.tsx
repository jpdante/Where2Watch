import React from "react";
import { withTranslation, WithTranslation } from "react-i18next";
import { Link } from "react-router-dom";
import NavLink from "./NavLink";
import SearchBar from "./SearchBar";
import UserBar from "./UserBar";

class NavBar extends React.Component<WithTranslation> {
  render() {
    return (
      <nav className="navbar navbar-dark navbar-expand-md justify-content-center">
        <div className="container">
          <div className="w-100 visible-md text-center">
            <SearchBar hasNavbarToggler={true} />
          </div>
          <div
            className="navbar-collapse collapse w-100"
            id="mainNavbarColappser"
          >
            <div className="d-flex w-100 mr-auto navbar-margin-top-md">
              <Link to="/" className="navbar-brand hidden-md">
                <img
                  src="/assets/images/logo-dark.svg"
                  alt="Logo"
                  height="20"
                  loading="lazy"
                />
              </Link>
              <ul className="navbar-nav w-100">
                <NavLink to="/" exact={true}>
                  Home
                </NavLink>
              </ul>
            </div>
            <ul className="navbar-nav w-100 justify-content-center hidden-md">
              <SearchBar hasNavbarToggler={false} />
            </ul>
            <UserBar />
          </div>
        </div>
      </nav>
    );
  }
}

export default withTranslation()(NavBar);

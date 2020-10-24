import React from "react";
import { withTranslation, WithTranslation } from "react-i18next";
import { Link } from "react-router-dom";
import NavLink from "./NavLink";

class NavBar extends React.Component<WithTranslation> {
  render() {
    return (
      <nav className="navbar navbar-expand-md navbar-dark justify-content-center">
        <div className="container">
          <Link className="navbar-brand" to="/">
            Where2Watch
          </Link>
          <button
            className="navbar-toggler"
            type="button"
            data-toggle="collapse"
            data-target="#navbarSupportedContent"
            aria-controls="navbarSupportedContent"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>

          <div className="collapse navbar-collapse" id="navbarSupportedContent">
            <ul className="navbar-nav mr-auto">
              <li className="nav-item active">
                <NavLink exact to="/">Home</NavLink>
              </li>
            </ul>
          </div>
        </div>
      </nav>
    );
  }
}

export default withTranslation()(NavBar);

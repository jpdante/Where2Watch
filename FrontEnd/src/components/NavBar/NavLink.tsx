import React, { ReactNode } from "react";
import { Link, useRouteMatch } from "react-router-dom";

type PropType = {
  to: string;
  exact?: boolean;
  children?: ReactNode;
};

function NavLink(props: PropType) {
  let match = useRouteMatch({
    path: props.to,
    exact: props.exact,
  });
  return (
    <Link to={props.to} className={match ? "nav-link active" : "nav-link"}>
      {props.children}
      <div className="nav-item-active" />
    </Link>
  );
}

export default NavLink;

import React from "react";
import { RouteProps as RouterPropsDOM, Route, Redirect } from 'react-router-dom'
import AuthStore from "../undux/AuthStore";

const PrivateRoute:React.FC<RouterPropsDOM> = ({ component: Component, ...rest }) => {
  let store = AuthStore.useStore();
  return store.get("isAuthenticated") ? (
    <Route component={Component} {...rest} />
  ) : (
    <Redirect to="login" />
  );
}

export default PrivateRoute;

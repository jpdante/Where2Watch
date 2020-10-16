import React from "react";
import {
  RouteProps as RouterPropsDOM,
  Route,
  Redirect,
} from "react-router-dom";
import AuthStore from "../undux/AuthStore";

const OnlyPublicRoute: React.FC<RouterPropsDOM> = ({
  component: Component,
  ...rest
}) => {
  let store = AuthStore.useStore();
  return store.get("isAuthenticated") ? (
    <Redirect to="/" />
  ) : (
    <Route component={Component} {...rest} />
  );
};

export default OnlyPublicRoute;

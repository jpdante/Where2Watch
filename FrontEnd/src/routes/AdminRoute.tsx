import React from "react";
import {
  RouteProps as RouterPropsDOM,
  Route,
} from "react-router-dom";
import AuthStore from "../undux/AuthStore";
import ProfileStore from "../undux/ProfileStore";

const AdminRoute: React.FC<RouterPropsDOM> = ({
  component: Component,
  ...rest
}) => {
  let authStore = AuthStore.useStore();
  let profileStore = ProfileStore.useStore();
  return authStore.get("isAuthenticated") && profileStore.get("type") === 1 ? (
    <Route component={Component} {...rest} />
  ) : (
    <div></div>//<Redirect to="/" />
  );
};

export default AdminRoute;

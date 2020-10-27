import React from "react";
import { BrowserRouter, Switch, Route } from "react-router-dom";
import AuthStore, { StoreProps } from "../undux/AuthStore";
import NavBar from "../components/NavBar";
import OnlyPublicRoute from "./OnlyPublicRoute";
import AdminRoute from "./AdminRoute";

const Home = React.lazy(() => import("../pages/Home"));
const Title = React.lazy(() => import("../pages/Title"));
const Login = React.lazy(() => import("../pages/Auth/Login"));
const Register = React.lazy(() => import("../pages/Auth/Register"));
const Admin = React.lazy(() => import("../pages/Admin"));

class Routes extends React.Component<StoreProps> {
  render() {
    return (
      <BrowserRouter>
        <Switch>
          <OnlyPublicRoute component={Login} path="/login" />
          <OnlyPublicRoute component={Register} path="/register" />
          <Route component={SubRoute} />
        </Switch>
      </BrowserRouter>
    );
  }
}

function SubRoute() {
  return (
    <div className="router">
      <NavBar />
      <div className="container">
        <Switch>
          <Route exact component={Home} path="/" />
          <AdminRoute component={Admin} path="/admin" />
          <Route component={Title} path="/:id" />
        </Switch>
      </div>
    </div>
  );
}

export default AuthStore.withStore(Routes);

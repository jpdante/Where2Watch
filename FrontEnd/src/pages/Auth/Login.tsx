import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import HCaptcha from "@hcaptcha/react-hcaptcha";
import { withTranslation, WithTranslation } from "react-i18next";
import Net from "../../utils/Net";
import { HCaptchaKey } from "./../../constants";
import { withRouter, RouteComponentProps } from "react-router-dom";
import AuthStore, { StoreProps } from "../../undux/AuthStore";

import styles from "./Auth.module.scss";

interface IProps extends WithTranslation, StoreProps, RouteComponentProps {}

type IState = {
  email?: string;
  password?: string;
  showPassword: boolean;
  errorId: number;
  errorMessage?: string;
};

class Login extends React.Component<IProps, IState> {
  private hCaptchaRef: any = React.createRef();

  constructor(props: IProps) {
    super(props);
    this.state = {
      email: "",
      password: "",
      showPassword: false,
      errorId: -1,
    };
  }

  validateInputs(state: IState): boolean {
    const emailRegex = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (!emailRegex.test(String(state.email).toLowerCase())) return false;
    if (!state.password || state.password.length < 8) return false;
    return true;
  }

  handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    this.hCaptchaRef.current.execute();
  };

  handleCapchaVerification = (captchaToken: string) => {
    let history = this.props.history;
    let store = this.props.store;
    if (!this.validateInputs(this.state)) return;
    this.setState({
      errorId: -1,
      errorMessage: undefined,
    });
    Net.post("/api/auth/login", {
      email: this.state.email,
      password: this.state.password,
      captcha: captchaToken,
    })
      .then((e) => {
        if (e.data && e.data.error) {
          this.setState({
            errorId: e.data.error.code,
            errorMessage: e.data.error.message,
          });
          return;
        }
        if (e.data && e.data.success) {
          store.set("token")(e.data.token);
          this.setState({
            email: "",
            password: "",
            showPassword: false,
          });
          history.push("/");
        }
      })
      .catch((e) => {
        if (e.response.data && e.response.data.error) {
          this.setState({
            errorId: e.response.data.error.code,
            errorMessage: e.response.data.error.message,
          });
          return;
        }
      });
  };

  render() {
    const { t, i18n } = this.props;
    return (
      <div className={styles.container}>
        <div className={`${styles.wrapper} ${styles.loginWrapper}`}>
          <form onSubmit={this.handleSubmit}>
            <div className={styles.logo}>
              <img src="assets/images/logo-dark.svg" alt="Logo" />
            </div>
            <div
              className={`${styles.errorBubble} ${
                this.state.errorId > -1 && styles.errorBubbleShow
              }`}
            >
              {this.state.errorId > -1 &&
              i18n.exists("error.login." + this.state.errorId.toString())
                ? t("error.login." + this.state.errorId.toString())
                : this.state.errorMessage}
            </div>
            <div className={styles.inputWrap}>
              <input
                className={`${styles.input} ${
                  this.state.email && styles.hasVal
                }`}
                type="text"
                value={this.state.email}
                onChange={(e) => {
                  this.setState({
                    email: e.target.value,
                  });
                }}
                autoComplete="email"
              />
              <span
                className={styles.inputFocus}
                data-placeholder={t("page.login.email")}
              ></span>
            </div>
            <div className={styles.inputWrap}>
              <span
                className={styles.btnShowPass}
                onClick={(e) => {
                  this.setState({
                    showPassword: !this.state.showPassword,
                  });
                }}
              >
                <FontAwesomeIcon
                  icon={this.state.showPassword ? "eye-slash" : "eye"}
                  size="sm"
                />
              </span>
              <input
                className={`${styles.input} ${
                  this.state.password && styles.hasVal
                }`}
                type={this.state.showPassword ? "text" : "password"}
                value={this.state.password}
                onChange={(e) => {
                  this.setState({
                    password: e.target.value,
                  });
                }}
                autoComplete="current-password"
              />
              <span
                className={styles.inputFocus}
                data-placeholder={t("page.login.password")}
              ></span>
            </div>
            <div className="text-center">
              <HCaptcha
                ref={this.hCaptchaRef}
                sitekey={HCaptchaKey}
                theme="dark"
                size="invisible"
                onVerify={this.handleCapchaVerification}
              />
            </div>
            <button
              type="submit"
              disabled={!this.validateInputs(this.state)}
              className={`btn btn-block ${styles.loginBtn}`}
            >
              {t("page.login.login")}
            </button>
          </form>
        </div>
      </div>
    );
  }
}

export default AuthStore.withStore(withRouter(withTranslation()(Login)));

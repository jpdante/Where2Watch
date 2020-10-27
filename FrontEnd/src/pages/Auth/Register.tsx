import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import HCaptcha from "@hcaptcha/react-hcaptcha";
import React from "react";
import { withTranslation, WithTranslation } from "react-i18next";
import Net from "../../utils/Net";
import { HCaptchaKey } from "./../../constants";
import RegisterModal from "./RegisterModal";
import $ from "jquery";

import styles from "./Auth.module.scss";

interface IProps extends WithTranslation {}

interface IState {
  email?: string;
  username?: string;
  password?: string;
  confirmPassword?: string;
  showPassword: boolean;
  days: string[];
  errorId: number;
  errorMessage?: string;
}

class Register extends React.Component<IProps, IState> {
  private hCaptchaRef: any = React.createRef();

  constructor(props: IProps) {
    super(props);
    this.state = {
      email: "",
      username: "",
      password: "",
      confirmPassword: "",
      showPassword: false,
      days: [],
      errorId: -1,
    };
  }

  validateEmail(value?: string): boolean {
    if (value == null) return false;
    const emailRegex = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return emailRegex.test(String(value).toLowerCase());
  }

  validateUsername(value?: string): boolean {
    if (value == null) return false;
    const usernameRegex = /^[0-9a-zA-Z-_.]{3,32}$/;
    return usernameRegex.test(String(value));
  }

  validatePassword(value?: string): boolean {
    if (value == null) return false;
    value = value.replace(/\s/g, "");
    return value.length >= 8;
  }

  validateConfirmPassword(value?: string, original?: string): boolean {
    if (value == null) return false;
    if (original == null) return false;
    value = value.replace(/\s/g, "");
    if (value.length < 8) return false;
    return value === original;
  }

  validateInputs(state: IState): boolean {
    if (!this.validateEmail(state.email)) return false;
    if (!this.validateUsername(state.username)) return false;
    if (!this.validatePassword(state.password)) return false;
    if (!this.validateConfirmPassword(state.confirmPassword, state.password))
      return false;
    return true;
  }

  handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    this.hCaptchaRef.current.execute();
  };

  handleCapchaVerification = (captchaToken: string) => {
    if (!this.validateInputs(this.state)) return;
    this.setState({
      errorId: -1,
      errorMessage: undefined,
    });
    Net.post("/api/auth/register", {
      email: this.state.email,
      username: this.state.username,
      password: this.state.password,
      confirmPassword: this.state.confirmPassword,
      captcha: captchaToken,
      Language: "en",
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
          this.setState({
            email: "",
            username: "",
            password: "",
            confirmPassword: "",
            showPassword: false,
          });
          ($("#registerConfirmationModal") as any).modal("show");
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
    let date = new Date();
    let years = [];
    let possibleYear = date.getFullYear() - 1;
    for (let i = 0; i < 120; i++) {
      years.push(possibleYear - i);
    }
    return (
      <div className={styles.container}>
        <div className={`${styles.wrapper} ${styles.registerWrapper}`}>
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
              i18n.exists("error.register." + this.state.errorId.toString())
                ? t("error.register." + this.state.errorId.toString())
                : this.state.errorMessage}
            </div>
            <div className={styles.inputWrap}>
              <span
                className={`${styles.checkIcon} ${
                  this.validateEmail(this.state.email) && styles.active
                }`}
              >
                <FontAwesomeIcon
                  icon={
                    this.validateEmail(this.state.email) ? "check" : "times"
                  }
                  size="sm"
                />
              </span>
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
                data-placeholder={t("page.register.email")}
              ></span>
            </div>
            <div className={styles.inputWrap}>
              <span
                className={`${styles.checkIcon} ${
                  this.validateUsername(this.state.username) && styles.active
                }`}
              >
                <FontAwesomeIcon
                  icon={
                    this.validateUsername(this.state.username)
                      ? "check"
                      : "times"
                  }
                  size="sm"
                />
              </span>
              <input
                className={`${styles.input} ${
                  this.state.username && styles.hasVal
                }`}
                type="text"
                value={this.state.username}
                onChange={(e) => {
                  this.setState({
                    username: e.target.value,
                  });
                }}
                autoComplete="username"
              />
              <span
                className={styles.inputFocus}
                data-placeholder={t("page.register.username")}
              ></span>
            </div>
            <div className={styles.inputWrap}>
              <span
                className={`${styles.checkIcon} ${
                  this.validatePassword(this.state.password) && styles.active
                }`}
              >
                <FontAwesomeIcon
                  icon={
                    this.validatePassword(this.state.password)
                      ? "check"
                      : "times"
                  }
                  size="sm"
                />
              </span>
              <span
                className={`${styles.btnShowPass} ${styles.paddingRight}`}
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
                autoComplete="new-password"
              />
              <span
                className={styles.inputFocus}
                data-placeholder={t("page.register.password")}
              ></span>
            </div>
            <div className={styles.inputWrap}>
              <span
                className={`${styles.checkIcon} ${
                  this.validateConfirmPassword(
                    this.state.confirmPassword,
                    this.state.password
                  ) && styles.active
                }`}
              >
                <FontAwesomeIcon
                  icon={
                    this.validateConfirmPassword(
                      this.state.confirmPassword,
                      this.state.password
                    )
                      ? "check"
                      : "times"
                  }
                  size="sm"
                />
              </span>
              <span
                className={`${styles.btnShowPass} ${styles.paddingRight}`}
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
                  this.state.confirmPassword && styles.hasVal
                }`}
                type={this.state.showPassword ? "text" : "password"}
                value={this.state.confirmPassword}
                onChange={(e) => {
                  this.setState({
                    confirmPassword: e.target.value,
                  });
                }}
                autoComplete="new-password"
              />
              <span
                className={styles.inputFocus}
                data-placeholder={t("page.register.confirmPassword")}
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
              Register
            </button>
          </form>
        </div>
        <RegisterModal />
      </div>
    );
  }
}

export default withTranslation()(Register);

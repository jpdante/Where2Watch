/* eslint-disable jsx-a11y/anchor-is-valid */
import React, { useCallback, useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useTranslation } from "react-i18next";
import lodash from "lodash";

import styles from "./NavBar.module.scss";
import Net from "../../utils/Net";
import { Link } from "react-router-dom";
import { useHistory } from "react-router-dom";

interface PropType {
  hasNavbarToggler: boolean;
}

function SearchBar(props: PropType) {
  const [searchContent, setSearchContent] = useState<string>("");
  const [searching, setSearching] = useState<boolean>(false);
  const [results, setResults] = useState<any[]>([]);
  const { t } = useTranslation();
  const history = useHistory();
  const delayedQuery = useCallback(
    lodash.debounce((q) => handleSearch(q), 500),
    []
  );

  function handleSearch(e: string) {
    if (!e || e.length <= 0) {
      setResults([]);
      return;
    }
    Net.post("/api/search/get", { data: e }).then((e) => {
      if (e.data && e.data.success) {
        setResults(e.data.result);
      }
    });
  }

  function updateSearchData(e: any) {
    setSearchContent(e.target.value);
    delayedQuery(e.target.value);
  }

  function goToLink(e: any) {
    history.push(`/${e}`);
    setSearching(false);
  }

  const sleep = (ms: number) => new Promise(resolve => setTimeout(resolve, ms))

  async function onSearchBlur(e: any) {
    await sleep(500);
    setSearching(false);
  }

  return (
    <form>
      <div
        className={`input-group mx-auto form-inline dropdown ${styles.searchBar}`}
      >
        <input
          type="text"
          className="form-control"
          placeholder={t("navbar.search")}
          aria-label="Search"
          aria-describedby="basic-addon2"
          value={searchContent}
          onChange={updateSearchData}
          onFocus={() => setSearching(true)}
          onBlur={onSearchBlur}
        />
        <div className="input-group-append">
          <button type="submit" className="btn btn-outline-secondary">
            <FontAwesomeIcon icon="search" aria-hidden="true" />
          </button>
          {props.hasNavbarToggler && (
            <button
              className="btn btn-outline-secondary"
              type="button"
              data-toggle="collapse"
              data-target="#mainNavbarColappser"
              aria-controls="mainNavbarColappser"
              aria-expanded="false"
              aria-label="Toggle navigation"
            >
              <span className="navbar-toggler-icon"></span>
            </button>
          )}
        </div>
        <div
          className={`${styles.animate} ${styles.slideIn} ${
            styles.searchDropDown
          } ${styles.dropDown} dropdown-menu dropdown-menu-right ${
            searching && results.length > 0 && "show"
          }`}
          aria-labelledby="navbarDropdownMenuLink"
        >
          {results.map((result, index) => {
            return (
              <a
                className="dropdown-item"
                href="#"
                key={index}
                onClick={() => goToLink(result.IMDbId)}
              >
                {result.Name}
              </a>
            );
          })}
        </div>
      </div>
    </form>
  );
}

export default SearchBar;

import React, { useCallback, useState } from "react";
import { useTranslation } from "react-i18next";
import lodash from "lodash";
import Net from "../utils/Net";

import styles from "./NavBar/NavBar.module.scss";

type IProps = {
  selectTitle: (id: string) => void;
}

function SearcherSelect(props: IProps) {
  const [searchContent, setSearchContent] = useState<string>("");
  const [searching, setSearching] = useState<boolean>(false);
  const [results, setResults] = useState<any[]>([]);
  const { t } = useTranslation();
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

  const sleep = (ms: number) =>
    new Promise((resolve) => setTimeout(resolve, ms));

  async function onSearchBlur(e: any) {
    await sleep(500);
    setSearching(false);
  }

  function selectResult(r: any) {
    setSearchContent(r.Name);
    props.selectTitle(r.IMDbId);
  }

  return (
    <div className="input-group mx-auto form-inline dropdown">
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
            <button
              className="dropdown-item"
              key={index}
              onClick={() => selectResult(result)}
            >
              {result.Name}
            </button>
          );
        })}
      </div>
    </div>
  );
}

export default SearcherSelect;

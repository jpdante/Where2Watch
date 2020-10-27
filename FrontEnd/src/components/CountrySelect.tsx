import React, { useEffect, useState } from "react";
import Country from "../model/Country";
import SharedStore from "../undux/SharedStore";

interface IProps {
  fill?: boolean;
  value?: string;
  onSelect: (option: string) => void;
}

function CountrySelect(props: IProps) {
  const sharedStore = SharedStore.useStore();
  const [country, setCountry] = useState<Country | null>(
    sharedStore.get("countries")[0]
  );

  useEffect(() => {
    if (props.value) {
      sharedStore.get("countries").forEach((c) => {
        if (c.id.toLowerCase() === props.value?.toLowerCase()) {
          setCountry(c);
        }
      });
    }
  }, [props.value]);

  return (
    <div className="dropdown">
      <button
        className={`btn ${
          props.fill && "btn-block text-left"
        } input-bg dropdown-toggle`}
        type="button"
        id="dropdownMenuButton"
        data-toggle="dropdown"
        aria-haspopup="true"
        aria-expanded="false"
      >
        <span className={`flag-icon flag-icon-${country?.id.toLowerCase()}`} />
        &nbsp;&nbsp;
        {country?.name}
      </button>
      <div className="dropdown-menu pre-scrollable">
        {sharedStore.get("countries").map((country, index) => {
          return (
            <button
              className="dropdown-item"
              onClick={() => {
                props.onSelect(country.id);
                setCountry(country);
              }}
              key={index}
            >
              <span
                className={`flag-icon flag-icon-${country.id.toLowerCase()}`}
              />
              &nbsp;&nbsp;
              {country.name}
            </button>
          );
        })}
      </div>
    </div>
  );
}

export default CountrySelect;

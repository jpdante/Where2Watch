import React, { useState } from "react";
import CountrySelect from "../../components/CountrySelect";
import Platform from "../../model/Platform";
import { getCountryByLanguage } from "../../utils/Locale";
import Net from "../../utils/Net";

interface IProps {
  platforms: Platform[];
}

function AddPlatform(props: IProps) {
  const [name, setName] = useState<string>("");
  const [icon, setIcon] = useState<string>("");
  const [country, setCountry] = useState<string>("");
  const [link, setLink] = useState<string>("");

  function handlerAdd(e: any) {
    if (!name) return;
    if (!icon) return;
    if (!country) return;
    if (!link) return;
    Net.post("/api/admin/platform/add", { name, icon, country, link })
      .then((e) => {
        if (e.data && e.data.success) {
          setName("");
          setIcon("");
          setCountry("");
          setLink("");
          alert("Ok!");
          window.location.reload();
        }
      })
      .catch((e) => {
        console.error(e.data);
        alert(e.data.error.message);
      });
  }

  return (
    <div className="module">
      <h5 className="text-center m-0 p-0">Add platform</h5>
      <hr className="mt-1 hr-light" />
      <div className="form-group">
        <label>Name</label>
        <input
          type="text"
          className="form-control"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
      </div>
      <div className="form-group">
        <label>Icon</label>
        <input
          type="text"
          className="form-control"
          value={icon}
          onChange={(e) => setIcon(e.target.value)}
        />
      </div>
      <div className="form-group">
        <label>Country</label>
        <CountrySelect
          fill
          onSelect={setCountry}
          value={country}
        />
      </div>
      <div className="form-group">
        <label>Link</label>
        <input
          type="text"
          className="form-control"
          value={link}
          onChange={(e) => setLink(e.target.value)}
        />
      </div>
      <button className="btn btn-primary btn-block" onClick={handlerAdd}>
        Add
      </button>
    </div>
  );
}

export default AddPlatform;

import React, { useEffect, useState } from "react";
import CountrySelect from "../../components/CountrySelect";
import SearcherSelect from "../../components/SearcherSelect";
import Platform from "../../model/Platform";
import Net from "../../utils/Net";

interface IProps {
  platforms: Platform[];
}

function AddAvailability(props: IProps) {
  const [titleId, setTitleId] = useState<string>("");
  const [country, setCountry] = useState<string>("");
  const [platform, setPlatform] = useState<string>("");
  const [link, setLink] = useState<string>("");

  useEffect(() => {
    if (props.platforms.length > 0) setPlatform(props.platforms[0].id);
  }, [props.platforms]);

  function setResult(id: string) {
    setTitleId(id);
  }

  function handlerAdd(e: any) {
    console.warn(country);
    if (!titleId) return;
    if (!country) return;
    if (!platform) return;
    if (!link) return;
    Net.post("/api/admin/availability/add", {
      imdbId: titleId,
      country,
      platform,
      link,
    })
      .then((e) => {
        if (e.data && e.data.success) {
          setLink("");
          alert("Ok!");
        }
      })
      .catch((e) => {
        console.error(e.data);
        alert(e.data.error.message);
      });
  }

  return (
    <div className="module">
      <h5 className="text-center m-0 p-0">Add availability</h5>
      <hr className="mt-1 hr-light" />
      <div className="form-group">
        <label>Title</label>
        <SearcherSelect selectTitle={setResult} />
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
        <label>Platform</label>
        <select
          className="form-control"
          value={platform}
          onChange={(e) => setPlatform(e.target.value)}
        >
          {props.platforms.map((platform, index) => {
            return (
              <option value={platform.id} key={index}>
                {platform.name}
              </option>
            );
          })}
        </select>
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

export default AddAvailability;

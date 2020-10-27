import React, { useState } from "react";
import Platform from "../../model/Platform";
import Net from "../../utils/Net";

interface IProps {
  platforms: Platform[];
}

function AddSeriesFromImdb(props: IProps) {
  const [imdb, setImdb] = useState<string>("");

  function setId(e: React.ChangeEvent<HTMLInputElement>) {
    if (e.target.value === imdb) return;
    let link = e.target.value;
    link = link.replace("https://www.imdb.com/title/", "");
    link = link.replace("/", "");
    setImdb(link);
  }

  function handlerAdd(e: any) {
    if(!imdb) return;
    Net.post("/api/admin/series/add", { imdbId: imdb }).then((e) => {
      if(e.data && e.data.success) {
        setImdb("");
        alert("Ok!");
      }
    }).catch((e) => {
      console.error(e.data);
      alert(e.data.error.message);
    });
  }

  return (
    <div className="module">
      <h5 className="text-center m-0 p-0">Add series from IMDb</h5>
      <hr className="mt-1 hr-light" />
      <div className="form-group">
        <label>IMDb Id</label>
        <input
          type="text"
          className="form-control"
          value={imdb}
          onChange={setId}
        />
      </div>
      <button className="btn btn-primary btn-block" onClick={handlerAdd}>
        Add
      </button>
    </div>
  );
}

export default AddSeriesFromImdb;

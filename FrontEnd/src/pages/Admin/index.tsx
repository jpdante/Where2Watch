import React from "react";

function Admin(props: any) {
  return (
    <div>
      <div className="module">
        <h5 className="text-center m-0 p-0">Add series from IMDb</h5>
        <hr className="mt-1 hr-light" />
        <div className="form-group">
          <label>IMDb Id</label>
          <input
            type="text"
            className="form-control"
          />
        </div>
        <button className="btn btn-primary btn-block">Add</button>
      </div>
    </div>
  );
}

export default Admin;

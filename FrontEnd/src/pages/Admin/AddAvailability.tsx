import React from "react";

function AddAvailability(props: any) {
  return (
    <div className="module">
      <h5 className="text-center m-0 p-0">Add availability</h5>
      <hr className="mt-1 hr-light" />
      <div className="form-group">
        <label>IMDb Id</label>
        <input type="text" className="form-control" />
      </div>
      <div className="form-group">
        <label>Country</label>
        <select className="form-control">
          <option>1</option>
          <option>2</option>
          <option>3</option>
          <option>4</option>
          <option>5</option>
        </select>
      </div>
      <div className="form-group">
        <label>Platform</label>
        <select className="form-control">
          <option>1</option>
          <option>2</option>
          <option>3</option>
          <option>4</option>
          <option>5</option>
        </select>
      </div>
      <div className="form-group">
        <label>Link</label>
        <input type="text" className="form-control" />
      </div>
      <button className="btn btn-primary btn-block">Add</button>
    </div>
  );
}

export default AddAvailability;

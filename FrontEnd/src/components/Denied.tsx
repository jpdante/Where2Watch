import React from "react";

function Denied(props: any) {
  return (
    <div className="center-page-content text-center">
      <h3>403 Access Denied</h3>
      <h5>
        <small>You must be an admin to access this page</small>
      </h5>
    </div>
  );
}

export default Denied;

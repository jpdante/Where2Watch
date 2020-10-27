import React from "react";
import AddAvailability from "./AddAvailability";
import AddSeriesFromImdb from "./AddSeriesFromImdb";

function Admin(props: any) {
  return (
    <div>
      <AddSeriesFromImdb />
      <AddAvailability />
    </div>
  );
}

export default Admin;

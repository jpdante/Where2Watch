import React, { useEffect, useState } from "react";
import Platform from "../../model/Platform";
import Net from "../../utils/Net";
import AddAvailability from "./AddAvailability";
import AddSeriesFromImdb from "./AddSeriesFromImdb";
import AddPlatform from './AddPlatform';

function Admin(props: any) {
  const [platforms, setPlatforms] = useState<Platform[]>([]);

  useEffect(() => {
    Net.get("/api/admin/platform/get").then((e) => {
      if(e.data) {
        setPlatforms(e.data);
      }
    });
  }, []);

  return (
    <div>
      <AddSeriesFromImdb platforms={platforms} />
      <AddAvailability platforms={platforms} />
      <AddPlatform platforms={platforms} />
    </div>
  );
}

export default Admin;

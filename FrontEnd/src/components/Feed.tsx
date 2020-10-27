import React from "react";
import PreviewPoster from "./PreviewPoster";

interface IProps {
  data: any[];
}

function Feed(props: IProps) {
  return (
    <div className="d-flex flex-wrap justify-content-start">
      {props.data.map((item, index) => (
        <PreviewPoster key={index} data={item} />
      ))}
    </div>
  );
}

export default Feed;

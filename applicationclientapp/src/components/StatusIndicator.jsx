import React from "react";

const StatusIndicator = ({ status }) => {
  const getColor = (status) => {
    switch (status) {
      case "New": return "red";
      case "In Progress": return "yellow";
      case "Completed"||"Closed": return "green";
      case "Additional Documents Required": return "white";
      default: return "gray";
    }
  };

  return (
    <span
      style={{
        display: "inline-block",
        width: "15px",
        height: "15px",
        borderRadius: "50%",
        border: "1px solid black",
        backgroundColor: getColor(status),
      }}
    ></span>
  );
};

export default StatusIndicator;
import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import ApplicationList from "./components/ApplicationList";
import ApplicationEdit from "./components/ApplicationEdit";
import 'bootstrap/dist/css/bootstrap.min.css';
const App = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<ApplicationList />} />
        <Route path="/edit/:id" element={<ApplicationEdit />} />
      </Routes>
    </Router>
  );
};

export default App;
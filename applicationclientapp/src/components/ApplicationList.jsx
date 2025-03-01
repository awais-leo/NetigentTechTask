import React, { useEffect, useState } from "react";
import { getApplications } from "../api";
import { useNavigate } from "react-router-dom";
import StatusIndicator from "./StatusIndicator";
import { Table, Button, Container, Pagination, Form } from "react-bootstrap";

const ApplicationList = () => {
  const [applications, setApplications] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [itemsPerPage, setItemsPerPage] = useState(10);
  const navigate = useNavigate();

  useEffect(() => {
    getApplications()
      .then((response) => setApplications(response.data))
      .catch((error) => console.error("Error fetching data:", error));
  }, []);

  // Pagination logic
  const indexOfLastItem = currentPage * itemsPerPage;
  const indexOfFirstItem = indexOfLastItem - itemsPerPage;
  const currentItems = applications.slice(indexOfFirstItem, indexOfLastItem);
  const totalPages = Math.ceil(applications.length / itemsPerPage);

  const handlePageChange = (pageNumber) => {
    setCurrentPage(pageNumber);
  };

  return (
    <Container className="mt-4">
      <h2>Customer X - Application Tracker</h2>
      <Button className="mb-3">Add Application</Button>
      <Table bordered hover>
        <thead>
          <tr>
            <th>Status</th>
            <th>#</th>
            <th>Project Name</th>
            <th>Location</th>
            <th>Status Level</th>
          </tr>
        </thead>
        <tbody>
          {currentItems.map((app) => (
            <tr key={app.id} onClick={() => navigate(`/edit/${app.id}`)}>
              <td><StatusIndicator status={app.status.statusName} /></td>
              <td>{app.id || "N/A"}</td>
              <td>{app.projectName || "N/A"}</td>
              <td>{app.projectLocation || "Unknown"}</td>
              <td>{app.status.statusName || "Not Available"}</td>
            </tr>
          ))}
        </tbody>
      </Table>

      {/* Pagination Controls */}
      <div className="d-flex justify-content-between">
        <Pagination>
          <Pagination.Prev 
            onClick={() => setCurrentPage((prev) => Math.max(prev - 1, 1))} 
            disabled={currentPage === 1} 
          />
          {[...Array(totalPages).keys()].map((page) => (
            <Pagination.Item
              key={page + 1}
              active={page + 1 === currentPage}
              onClick={() => handlePageChange(page + 1)}
            >
              {page + 1}
            </Pagination.Item>
          ))}
          <Pagination.Next 
            onClick={() => setCurrentPage((prev) => Math.min(prev + 1, totalPages))} 
            disabled={currentPage === totalPages} 
          />
        </Pagination>

        {/* Items per page dropdown */}
        <Form.Select 
          value={itemsPerPage} 
          onChange={(e) => setItemsPerPage(Number(e.target.value))} 
          style={{ width: "auto" }}
        >
          <option value={5}>5</option>
          <option value={10}>10</option>
          <option value={20}>20</option>
        </Form.Select>
      </div>
    </Container>
  );
};

export default ApplicationList;

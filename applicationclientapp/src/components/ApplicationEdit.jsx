import React, { useEffect, useState } from "react";
import { Form, Button, ListGroup, Alert, Row, Col, Container } from "react-bootstrap";
import axios from "axios";
import { useParams, useNavigate } from "react-router-dom";

const ApplicationEdit = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [formData, setFormData] = useState({ name: "", location: "", status: "", notes: "" });
  const [inquiries, setInquiries] = useState([]);
  const [message, setMessage] = useState("");
  const [errors, setErrors] = useState({});

  useEffect(() => {
    fetchApplication();
    fetchInquiries();
    
  }, []);

  const latestInquiry = inquiries.length > 0 ? inquiries[inquiries.length-1] : null;
  
  const fetchApplication = async () => {
    try {
      const response = await axios.get(`/api/applications/${id}`);
      setFormData(response.data);
    } catch (error) {
      console.error("Error fetching application", error);
    }
  };

  const fetchInquiries = async () => {
    try {
      const response = await axios.get(`/api/inquiries?applicationId=${id}`);
      setInquiries(response.data);
    } catch (error) {
      console.error("Error fetching inquiries", error);
    }
  };

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const validateForm = () => {
    let errors = {};
    if (!formData.projectName) errors.projectName = "Name is required";
    if (!formData.projectLocation) errors.projectLocation = "Location is required";
    if (!formData.appStatus) errors.appStatus = "Status is required";
    setErrors(errors);
    return Object.keys(errors).length === 0;
  };

 const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validateForm()) return;

    try {
      await axios.put(`/api/applications/${id}`, formData, {
        headers: {
          "Content-Type": "application/json",
          useParams: {  id },
            
        },
      });
      setMessage("Application updated successfully!");
    } catch (error) {
      console.error("Error updating application", error);
    }
  };

  return (
    <Container className="mt-4">
      <h2>Customer X - Application Tracker</h2>
     
      {/* New Inquiry Alert */}
      <b >New Inquiry</b>
<Alert variant="warning">
  {latestInquiry && <p>{latestInquiry.inquiry}</p>}
 
</Alert>

      {/* Form */}
      <Form onSubmit={handleSubmit} className="mb-4">
        <Row>
          <Col md={6}>
            <Form.Group as={Row} className="mb-3">
              <Form.Label column sm="3">Ref #</Form.Label>
              <Col sm="9">
                <Form.Control name="ref" value={formData.projectRef || ""} onChange={handleChange} disabled />
              </Col>
            </Form.Group>

            <Form.Group as={Row} className="mb-3">
              <Form.Label column sm="3">Name</Form.Label>
              <Col sm="9">
                <Form.Control name="name" value={formData.projectName} onChange={handleChange} required />
                {errors.name && <span className="text-danger">{errors.name}</span>}
              </Col>
            </Form.Group>

            <Form.Group as={Row} className="mb-3">
              <Form.Label column sm="3">Value</Form.Label>
              <Col sm="9">
                <Form.Control type="number" name="value" value={formData.projectValue || ""} onChange={handleChange} required />
              </Col>
            </Form.Group>

            <Form.Group as={Row} className="mb-3">
              <Form.Label column sm="3">Location</Form.Label>
              <Col sm="9">
                <Form.Control name="location" value={formData.projectLocation || ""} onChange={handleChange} required />
                {errors.location && <span className="text-danger">{errors.location}</span>}
              </Col>
            </Form.Group>

            <Form.Group as={Row} className="mb-3">
              <Form.Label column sm="3">State</Form.Label>
              <Col sm="9">
                <Form.Control name="status" value={formData.appStatus || ""} disabled />
              </Col>
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label>Notes</Form.Label>
              <Form.Control as="textarea" rows={3} name="notes" value={formData.notes || ""} onChange={handleChange} />
            </Form.Group>
          </Col>

          {/* Right Side - Inquiries */}
          <Col md={6}>
            <h5>Inquiries</h5>
            <div style={{ height: "200px", overflowY: "auto", border: "1px solid #ccc", padding: "10px", borderRadius: "5px" }}>
              <ListGroup variant="flush">
                {inquiries.length > 0 ? (
                  inquiries.map((inquiry, index) => (
                    <ListGroup.Item key={index}>{inquiry.inquiry}</ListGroup.Item>
                  ))
                ) : (
                  <ListGroup.Item>No inquiries found</ListGroup.Item>
                )}
              </ListGroup>
            </div>
          </Col>
        </Row>

        {/* Buttons */}
        <div className="d-flex justify-content-end mt-3">
          <Button variant="secondary" onClick={() => navigate(-1)} className="me-2">Close</Button>
          <Button variant="success" type="submit">Save</Button>
        </div>
      </Form>
    </Container>
  );
};

export default ApplicationEdit;

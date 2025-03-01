import axios from "axios";

const API_BASE_URL = "http://localhost:5081"; 

export const getApplications = async () => {
  return axios.get(`${API_BASE_URL}/applications`);
};

export const getApplicationById = async (id) => {
  return axios.get(`${API_BASE_URL}/applications/${id}`);
};

export const updateApplication = async (id, data) => {
  return axios.put(`${API_BASE_URL}/applications/${id}`, data);
};

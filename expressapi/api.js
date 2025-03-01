const axios = require('axios');

const express = require('express');

const app = express();
const cors = require('cors');

app.use(cors());
const posts = {}


const API_BASE_URL = "http://localhost:5080/api"; 

 
app.get(`/applications`, async (req, res) => {
  axios.get(`${API_BASE_URL}/applications`).then((response) => {
    return res.send(response.data);
  });
  
});

app.get(`/applications`, async (id, res) => {
    axios.get(`${API_BASE_URL}/applications/${id}`).then((response) => {
      return res.send(response.data);
    });
    
  });


 app.put(`/applications`, async (id, data) => {
    axios.put(`${API_BASE_URL}/applications/${id}`, data).then((response) => {
      return res.send(response.data);
    });
    
  });


app.listen(5081, () => {    
  console.log("Server is running on port 5081");
});
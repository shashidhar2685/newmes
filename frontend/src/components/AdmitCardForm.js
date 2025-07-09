import React, { useState } from 'react';
import axios from 'axios';

const AdmitCardForm = () => {
  const [formData, setFormData] = useState({
    CandidateId: '',
    AdmitCardNo: '',
    ExamDate: '',
    ExamCenter: ''
  });

  const [message, setMessage] = useState('');

  const handleChange = (e) => {
    setFormData({...formData, [e.target.name]: e.target.value});
  };

const handleSubmit = async (e) => {
  e.preventDefault();
  try {
    const res = await axios.post('http://localhost:54180/api/admitcard/generate', formData);
    console.log("✅ Success Response:", res);

    if (res && res.data) {
      setMessage(res.data); // Should print "Admit card generated successfully."
    } else {
      setMessage("Unknown success response.");
    }
  } catch (err) {
    console.error("❌ Error Response:", err);
    if (err.response) {
      // Server responded with a status outside 2xx
      console.log("Status:", err.response.status);
      console.log("Data:", err.response.data);
      setMessage(`Error: ${err.response.data.ExceptionMessage || "Failed to generate admit card."}`);
    } else if (err.request) {
      // Request sent but no response
      console.log("❗ Request made, no response received.");
      setMessage("No response from server.");
    } else {
      // Other error
      setMessage("Error: " + err.message);
    }
  }
};


  return (
    <div>
      <h2>Generate Admit Card</h2>
      <form onSubmit={handleSubmit}>
        <input type="text" name="CandidateId" placeholder="Candidate ID" onChange={handleChange} required />
        <input type="text" name="AdmitCardNo" placeholder="Admit Card No" onChange={handleChange} required />
        <input type="date" name="ExamDate" onChange={handleChange} required />
        <input type="text" name="ExamCenter" placeholder="Exam Center" onChange={handleChange} required />
        <button type="submit">Generate</button>
      </form>
      <p>{message}</p>
    </div>
  );
};

export default AdmitCardForm;

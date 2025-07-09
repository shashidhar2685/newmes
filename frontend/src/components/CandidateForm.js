import React, { useState } from 'react';
import axios from 'axios';

const CandidateForm = () => {
  const [candidate, setCandidate] = useState({
    name: '',
    email: '',
    phone: '',
    qualification: ''
  });

  const [message, setMessage] = useState('');

  const handleChange = (e) => {
    setCandidate({
      ...candidate,
      [e.target.name]: e.target.value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setMessage('');

    try {
      const response = await axios.post('http://localhost:5000/api/candidates', candidate);
      if (response.status === 200 || response.status === 201) {
        setMessage('Candidate registered successfully!');
        setCandidate({ name: '', email: '', phone: '', qualification: '' });
      } else {
        setMessage('Failed to register candidate.');
      }
    } catch (error) {
      console.error('Error during submission:', error);
      setMessage('Error while submitting form.');
    }
  };

  return (
    <div className="container" style={{ maxWidth: '500px', marginTop: '30px' }}>
      <h2>Candidate Registration Form</h2>
      {message && <p style={{ color: 'green' }}>{message}</p>}
      <form onSubmit={handleSubmit}>
        <div>
          <label>Name:</label>
          <input
            type="text"
            name="name"
            value={candidate.name}
            onChange={handleChange}
            required
            className="form-control"
          />
        </div>

        <div>
          <label>Email:</label>
          <input
            type="email"
            name="email"
            value={candidate.email}
            onChange={handleChange}
            required
            className="form-control"
          />
        </div>

        <div>
          <label>Phone:</label>
          <input
            type="text"
            name="phone"
            value={candidate.phone}
            onChange={handleChange}
            required
            className="form-control"
          />
        </div>

        <div>
          <label>Qualification:</label>
          <input
            type="text"
            name="qualification"
            value={candidate.qualification}
            onChange={handleChange}
            required
            className="form-control"
          />
        </div>

        <button type="submit" style={{ marginTop: '15px' }} className="btn btn-primary">
          Submit
        </button>
      </form>
    </div>
  );
};

export default CandidateForm;

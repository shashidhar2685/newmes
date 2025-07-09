import React, { useEffect, useState } from 'react';
import axios from 'axios';

const CandidateList = () => {
  const [candidates, setCandidates] = useState([]);

  useEffect(() => {
    axios.get('http://localhost:54180/api/Candidate/GetAll')
      .then(response => {
        console.log("Fetched from API:", response.data); // ✅ Log response here
        setCandidates(response.data);
      })
      .catch(error => {
        console.error('Error fetching candidates:', error);
      });
  }, []);

  return (
    <div style={{ margin: '20px' }}>
      <h2>Registered Candidates</h2>
      <table border="1" cellPadding="10">
        <thead>
          <tr>
            <th>ID</th>
            <th>Full Name</th>
            <th>Email</th>
            <th>Phone</th>
            <th>Gender</th>
            <th>Date of Birth</th>
          </tr>
        </thead>
        <tbody>
          {candidates.length === 0 ? (
            <tr><td colSpan="6">No candidates found.</td></tr>
          ) : (
            candidates.map((c, index) => (
              <tr key={index}>
                <td>{c.CandidateId || c.candidateId}</td>
                <td>{c.FullName || c.fullName}</td>
                <td>{c.Email || c.email}</td>
                <td>{c.Phone || c.phone}</td>
                <td>{c.Gender || c.gender}</td>
                <td>{new Date(c.DateOfBirth || c.dateOfBirth).toLocaleDateString()}</td>
              </tr>
            ))
          )}
        </tbody>
      </table>
    </div>
  );
};

export default CandidateList;

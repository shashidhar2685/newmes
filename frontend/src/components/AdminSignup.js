import React, { useState } from 'react';
import axios from 'axios';

const AdminSignup = () => {
  const [formData, setFormData] = useState({
    Username: '',
    Email: '',
    PasswordHash: ''
  });

  const [message, setMessage] = useState('');

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.post('http://localhost:54180/api/admin/signup', formData);
      setMessage(response.data);
    } catch (err) {
      setMessage(err.response?.data?.Message || 'Signup failed');
    }
  };

  return (
    <div>
      <h2>Admin Signup</h2>
      <form onSubmit={handleSubmit}>
        <input type="text" name="Username" placeholder="Username" onChange={handleChange} />
        <input type="email" name="Email" placeholder="Email" onChange={handleChange} />
        <input type="password" name="PasswordHash" placeholder="Password" onChange={handleChange} />
        <button type="submit">Signup</button>
      </form>
      <p>{message}</p>
    </div>
  );
};

export default AdminSignup;

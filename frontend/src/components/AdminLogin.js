import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const AdminLogin = ({ setIsLoggedIn }) => {
  const [formData, setFormData] = useState({
    Username: '',
    Password: ''
  });

  const [error, setError] = useState('');
  const navigate = useNavigate();

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleLogin = async (e) => {
    e.preventDefault();

    try {
      const res = await axios.post('http://localhost:54180/api/admin/login', formData); // ✅ Correct port
      const jwtToken = res.data.token;

      // ✅ Save token
      localStorage.setItem('mes25_token', jwtToken);

      // ✅ Optionally set axios auth header globally
      axios.defaults.headers.common['Authorization'] = `Bearer ${jwtToken}`;

      setError('');
      alert('Login successful!');

      // ✅ Update parent login state
      if (setIsLoggedIn) {
        setIsLoggedIn(true);
      }

      navigate('/admin/dashboard');
    } catch (err) {
      console.error(err);
      setError('Login failed. Invalid credentials or server not reachable.');
    }
  };

  return (
    <div style={{ padding: '20px' }}>
      <h2>Admin Login</h2>
      <form onSubmit={handleLogin}>
        <input
          type="text"
          name="Username"
          placeholder="Username"
          value={formData.Username}
          onChange={handleChange}
          required
        />
        <br /><br />
        <input
          type="password"
          name="Password"
          value={formData.Password}
          onChange={handleChange}
          placeholder="Password"
          required
        />
        <br /><br />
        <button type="submit">Login</button>
      </form>

      {error && <p style={{ color: 'red' }}>{error}</p>}
    </div>
  );
};

export default AdminLogin;

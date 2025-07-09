import React from "react";
import { Link } from "react-router-dom";

const AdminDashboard = () => {
  return (
    <div>
      <h2>MES25 Admin Dashboard</h2>
      <ul>
        <li><Link to="/admin/dashboard">🏠 Dashboard Home</Link></li>
        <li><Link to="/admitcard">Generate Admit Card</Link></li>
        <li><Link to="/questions">Question Bank</Link></li>
        <li><Link to="/candidates">Registered Candidates</Link></li>
        <li><Link to="/admin/report">Live Test Report</Link></li>
        <li><Link to="/admin/logout">Logout</Link></li>
      </ul>
    </div>
  );
};

export default AdminDashboard;

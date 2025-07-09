import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";

export default function TestLoginPage() {
  const [hallTicket, setHallTicket] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!hallTicket.trim() || !password.trim()) {
      setError("Both Hall-Ticket and Password are required.");
      return;
    }

    try {
      // ✅ API call to backend
      const res = await axios.post("http://localhost:54180/api/auth/verifylogin", {
        hallTicketNo: hallTicket,
        password: password
      });

      // ✅ Store both hall ticket and full name in session
      sessionStorage.setItem("hallTicket", res.data.hallTicketNo);
      sessionStorage.setItem("candidateName", res.data.fullName);

      navigate("/start");
    } catch (err) {
      console.error(err);
      setError("Invalid hall-ticket number or password.");
    }
  };

  return (
    <div className="container">
      <h2>Online Test Login</h2>
      {error && <p style={{ color: "red" }}>{error}</p>}

      <form onSubmit={handleSubmit}>
        <div>
          <label>Hall‑Ticket No:</label>
          <input
            type="text"
            value={hallTicket}
            onChange={(e) => setHallTicket(e.target.value)}
          />
        </div>

        <div>
          <label>Password:</label>
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>

        <button type="submit">Login</button>
      </form>
    </div>
  );
}

import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

export default function TestStartPage() {
  const navigate = useNavigate();

  const [name, setName] = useState("");
  const [ticket, setTicket] = useState("");

  useEffect(() => {
    // ✅ Read from sessionStorage on first render
    const storedName = sessionStorage.getItem("candidateName");
    const storedTicket = sessionStorage.getItem("hallTicket");

    setName(storedName || "");
    setTicket(storedTicket || "");

    // ✅ Redirect if not logged in
    if (!storedTicket || !storedName) {
      navigate("/testlogin");
    }
  }, [navigate]);

  return (
    <div className="container">
      <h2>Welcome, {name || "(Name not found)"}</h2>

      <h3>Exam Rules</h3>
      <ul>
        <li>3 Questions · 3 Minutes</li>
        <li>1 point per correct answer</li>
        <li>Do not refresh or close the window</li>
      </ul>

      <button onClick={() => navigate("/test")}>Start Test</button>
    </div>
  );
}

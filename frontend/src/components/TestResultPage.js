import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";

export default function TestResultPage() {
  const navigate = useNavigate();
  const [result, setResult] = useState(null);   // null = still loading

  useEffect(() => {
    const stored = sessionStorage.getItem("testAnswers");

    // If someone opened /result directly, go back to login
    if (!stored) {
      navigate("/testlogin");      // use /testlogin if that's your test login route
      return;
    }

    // Payload we saved in TestPage.js
    const payload = JSON.parse(stored);

    // Debug – verify what we are sending
    console.log("Submitting to backend:", payload);

    // Call backend to evaluate answers
    axios
      .post("http://localhost:54180/api/test/submitresult", payload)
      .then((res) => {
        console.log("Backend score response:", res.data); // Debug
        setResult(res.data);   // { totalQuestions, correctAnswers, wrongAnswers, score }
      })
      .catch((err) => {
        // Log detailed error in console for diagnosis
        console.error(
          "SubmitResult error:",
          err.response ? err.response : err.message
        );

        alert("Failed to fetch test result. See console for details.");
        // Show zeroed result to avoid infinite "Loading..."
        setResult({
          totalQuestions: 0,
          correctAnswers: 0,
          wrongAnswers: 0,
          score: 0
        });
      });
  }, [navigate]);

   if (!result) return <p>Loading result…</p>;

  return (
  <div className="container">
    <h2>Test Result</h2>

    {/* use PascalCase names from backend */}
    <h3>
      Score: {result.Score} / {result.TotalQuestions}
    </h3>

    <p>✅ Correct Answers: {result.CorrectAnswers}</p>
    <p>❌ Wrong Answers: {result.WrongAnswers}</p>

    <button onClick={() => navigate("/testlogin")}>Logout</button>
  </div>
);
}

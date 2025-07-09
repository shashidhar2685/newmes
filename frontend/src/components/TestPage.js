import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";

export default function TestPage() {
  const navigate = useNavigate();
  const hallTicket = sessionStorage.getItem("hallTicket");

  const [questions, setQuestions] = useState([]);
  const [answers, setAnswers] = useState({});   // { QuestionId: "A" }
  const [timeLeft, setTimeLeft] = useState(180); // 3 min = 180 s

  /* ─────────────────────────────── */
  /* Fetch 3 random MCQs on mount    */
  /* ─────────────────────────────── */
  useEffect(() => {
    if (!hallTicket) {
      navigate("/testlogin");
      return;
    }

    axios
      .get("http://localhost:54180/api/test/getrandomquestions")
      .then((res) => setQuestions(res.data))
      .catch(() => alert("Failed to load questions"));
  }, [hallTicket, navigate]);

  /* ─────────────────────────────── */
  /* Countdown timer                 */
  /* ─────────────────────────────── */
  useEffect(() => {
    if (timeLeft <= 0) {
      handleSubmit();   // auto‑submit when timer hits 0
      return;
    }
    const id = setInterval(() => setTimeLeft((t) => t - 1), 1000);
    return () => clearInterval(id);
  }, [timeLeft]);

  /* ─────────────────────────────── */
  /* Save answer for one question    */
  /* ─────────────────────────────── */
  const handleOptionChange = (qid, opt) =>
    setAnswers((prev) => ({ ...prev, [qid]: opt.toUpperCase() }));  // always A/B/C/D

  /* ─────────────────────────────── */
  /* Submit test                     */
  /* ─────────────────────────────── */
 const handleSubmit = () => {
  const payload = {
    hallTicket,
    answers: Object.entries(answers).map(([questionId, selectedOption]) => ({
      questionId: parseInt(questionId),
      selectedOption
    }))
  };

  console.log("Test Submitted:", payload); // 🐞 Debug log

  sessionStorage.setItem("testAnswers", JSON.stringify(payload));
  navigate("/result");
};

  /* MM:SS format */
  const mm = String(Math.floor(timeLeft / 60)).padStart(2, "0");
  const ss = String(timeLeft % 60).padStart(2, "0");

  return (
    <div className="container">
      <h2>Online Test</h2>
      <p>Time Remaining: {mm}:{ss}</p>

      {questions.map((q, i) => (
        <div key={q.QuestionId} style={{ marginBottom: 20 }}>
          <p><b>{i + 1}. {q.QuestionText}</b></p>

          {["A", "B", "C", "D"].map((opt) => (
            <label key={opt} style={{ display: "block" }}>
              <input
                type="radio"
                name={`q${q.QuestionId}`}
                value={opt}
                checked={answers[q.QuestionId] === opt}
                onChange={() => handleOptionChange(q.QuestionId, opt)}
              />
              {q[`Option${opt}`]}
            </label>
          ))}
        </div>
      ))}

      <button onClick={handleSubmit}>Submit Test</button>
    </div>
  );
}

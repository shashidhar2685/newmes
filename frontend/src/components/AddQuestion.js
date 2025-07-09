import React, { useState } from "react";
import axios from "axios";
import { toast } from "react-toastify";

const AddQuestion = () => {
  const [question, setQuestion] = useState("");
  const [optionA, setOptionA] = useState("");
  const [optionB, setOptionB] = useState("");
  const [optionC, setOptionC] = useState("");
  const [optionD, setOptionD] = useState("");
  const [correctOption, setCorrectOption] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();

    // ✅ Frontend Validation
    if (!question.trim()) {
      toast.error("Question is required");
      return;
    }
    if (!optionA || !optionB || !optionC || !optionD) {
      toast.error("All 4 options are required");
      return;
    }
    if (!correctOption) {
      toast.error("Please select the correct option");
      return;
    }

    const payload = {
      question,
      optionA,
      optionB,
      optionC,
      optionD,
      correctOption,
    };

    try {
      const res = await axios.post("http://localhost:12345/api/question/add", payload); // Change port if needed
      toast.success("Question added successfully");

      // Clear form after success
      setQuestion("");
      setOptionA("");
      setOptionB("");
      setOptionC("");
      setOptionD("");
      setCorrectOption("");
    } catch (err) {
      console.error(err);
      toast.error("Failed to add question. Try again later.");
    }
  };

  return (
    <div style={{ maxWidth: "600px", margin: "auto" }}>
      <h2>Add New Question</h2>
      <form onSubmit={handleSubmit}>
        <label>Question *</label>
        <input type="text" value={question} onChange={(e) => setQuestion(e.target.value)} autoFocus />

        <label>Option A *</label>
        <input type="text" value={optionA} onChange={(e) => setOptionA(e.target.value)} />

        <label>Option B *</label>
        <input type="text" value={optionB} onChange={(e) => setOptionB(e.target.value)} />

        <label>Option C *</label>
        <input type="text" value={optionC} onChange={(e) => setOptionC(e.target.value)} />

        <label>Option D *</label>
        <input type="text" value={optionD} onChange={(e) => setOptionD(e.target.value)} />

        <label>Correct Option *</label>
        <select value={correctOption} onChange={(e) => setCorrectOption(e.target.value)}>
          <option value="">--Select--</option>
          <option value="A">A</option>
          <option value="B">B</option>
          <option value="C">C</option>
          <option value="D">D</option>
        </select>

        <br /><br />
        <button type="submit">Add Question</button>
      </form>
    </div>
  );
};

export default AddQuestion;

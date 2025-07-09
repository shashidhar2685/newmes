import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { Link } from 'react-router-dom';

const QuestionList = () => {
  const [questions, setQuestions] = useState([]);
  const navigate = useNavigate();

  // Load questions on component mount
  useEffect(() => {
    fetchQuestions();
  }, []);

  const fetchQuestions = async () => {
    try {
      const res = await axios.get('http://localhost:54180/api/question/getall');
      setQuestions(res.data);
    } catch (err) {
      alert('Failed to load questions');
    }
  };

  const handleDelete = async (id) => {
    if (window.confirm('Are you sure you want to delete this question?')) {
      try {
        await axios.delete(`http://localhost:54180/api/question/delete/${id}`);
        alert('Question deleted');
        fetchQuestions(); // Refresh list
      } catch {
        alert('Failed to delete question');
      }
    }
  };

  return (
    <div>
      <h2>Question Bank</h2>
      <button onClick={() => navigate('/add-question')}>Add New Question</button>
      <table border="1" cellPadding="10" style={{ marginTop: '20px' }}>
        <thead>
          <tr>
            <th>Question</th>
            <th>A</th>
            <th>B</th>
            <th>C</th>
            <th>D</th>
            <th>Correct</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {questions.length > 0 ? (
            questions.map((q) => (
              <tr key={q.QuestionId}>
                <td>{q.QuestionText}</td>
                <td>{q.OptionA}</td>
                <td>{q.OptionB}</td>
                <td>{q.OptionC}</td>
                <td>{q.OptionD}</td>
                <td>{q.CorrectAnswer}</td>
                <td>
                  <button onClick={() => navigate(`/edit-question/${q.QuestionId}`)}>Edit</button>{' '}
                  <button onClick={() => handleDelete(q.QuestionId)}>Delete</button>
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="7">No questions available.</td>
            </tr>
          )}
        </tbody>
      </table>
    </div>
  );
};

export default QuestionList;

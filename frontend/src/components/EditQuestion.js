import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams, useNavigate } from 'react-router-dom';

const EditQuestion = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [q, setQ] = useState({
    QuestionText: '',
    OptionA: '',
    OptionB: '',
    OptionC: '',
    OptionD: '',
    CorrectAnswer: ''
  });

  useEffect(() => {
    axios
      .get(`http://localhost:54180/api/question/get/${id}`)
      .then((res) => setQ(res.data))
      .catch(() => alert("Failed to load question"));
  }, [id]);

  const handleChange = (e) => {
    setQ({ ...q, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await axios.post('http://localhost:54180/api/question/update', q);
      alert('Question updated successfully');
      navigate('/questions');
    } catch {
      alert('Failed to update question');
    }
  };

  return (
    <div>
      <h2>Edit Question</h2>
      <form onSubmit={handleSubmit}>
        <input name="QuestionText" placeholder="Question" value={q.QuestionText} onChange={handleChange} /><br />
        <input name="OptionA" placeholder="Option A" value={q.OptionA} onChange={handleChange} /><br />
        <input name="OptionB" placeholder="Option B" value={q.OptionB} onChange={handleChange} /><br />
        <input name="OptionC" placeholder="Option C" value={q.OptionC} onChange={handleChange} /><br />
        <input name="OptionD" placeholder="Option D" value={q.OptionD} onChange={handleChange} /><br />
        <input name="CorrectAnswer" placeholder="Correct (A/B/C/D)" value={q.CorrectAnswer} onChange={handleChange} /><br />
        <button type="submit">Update</button>
      </form>
    </div>
  );
};

export default EditQuestion;

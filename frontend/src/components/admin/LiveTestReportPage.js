import React, { useEffect, useState } from "react";
import axios from "axios";

export default function LiveTestReportPage() {
  const [rows, setRows] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    axios
      .get("http://localhost:54180/api/testresult/list")
      .then((res) => {
        setRows(res.data);
      })
      .catch((err) => {
        console.error("Failed to load report:", err);
        setError("Unable to fetch test results.");
      })
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <p className="p-6">Loading test report...</p>;
  if (error) return <p className="p-6 text-red-600">{error}</p>;

  return (
    <div className="p-6">
      <h2 className="text-2xl font-semibold mb-4">Live Test Report</h2>

      <table className="w-full border text-sm">
        <thead className="bg-gray-200">
          <tr>
            <Th>S.No</Th>
            <Th>Hallticket</Th>
            <Th>Name</Th>
            <Th>Correct</Th>
            <Th>Wrong</Th>
            <Th>Not Attempted</Th>
            <Th>Score</Th>
            <Th>Test Date</Th>
          </tr>
        </thead>
        <tbody>
          {rows.map((r, i) => (
            <tr key={r.Id} className="text-center even:bg-gray-50">
              <Td>{i + 1}</Td>
              <Td>{r.HallTicketNumber}</Td>
              <Td className="text-left">{r.CandidateName}</Td>
              <Td>{r.CorrectAnswers}</Td>
              <Td>{r.WrongAnswers}</Td>
              <Td>{r.NotAttempted}</Td>
              <Td>{r.Score}</Td>
              <Td>{new Date(r.TestDate).toLocaleString()}</Td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

const Th = ({ children }) => (
  <th className="border px-3 py-2 font-medium">{children}</th>
);
const Td = ({ children }) => (
  <td className="border px-3 py-1">{children}</td>
);

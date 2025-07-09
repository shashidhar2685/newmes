import React, { useState, useEffect } from "react";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  Link,
  Navigate,
  useNavigate,
} from "react-router-dom";

// Pages / Components
import CandidateForm from "./components/CandidateForm";
import CandidateList from "./components/CandidateList";
import AdminSignup from "./components/AdminSignup";
import AdminLogin from "./components/AdminLogin";
import AdmitCardForm from "./components/AdmitCardForm";
import QuestionList from "./components/QuestionList";
import AddQuestion from "./components/AddQuestion";
import EditQuestion from "./components/EditQuestion";
import TestLoginPage from "./components/TestLoginPage";
import TestStartPage from "./components/TestStartPage";
import TestPage from "./components/TestPage";
import TestResultPage from "./components/TestResultPage";
import LiveTestReportPage from "./components/admin/LiveTestReportPage";
import AdminDashboard from "./components/AdminDashboard";

import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

/************************************
 * Utility: PrivateRoute
 ***********************************/
const PrivateRoute = ({ children }) => {
  const token = localStorage.getItem("mes25_token");
  return token ? children : <Navigate to="/adminlogin" replace />;
};

/************************************
 * Navigation Bar
 ***********************************/
/************************************
 * Navigation Bar component
 ***********************************/
const NavBar = ({ isLoggedIn, onLogout }) => (
  <nav style={{ marginBottom: "20px" }}>
    {!isLoggedIn && (
      <>
        <Link to="/" style={{ marginRight: 10 }}>
          Candidate Registration
        </Link>
        <Link to="/signup" style={{ marginRight: 10 }}>
          Admin Signup
        </Link>
        <Link to="/adminlogin" style={{ marginRight: 10 }}>
          Admin Login
        </Link>
        <Link to="/testlogin" style={{ marginRight: 10 }}>
          Take Test
        </Link>
      </>
    )}

    {isLoggedIn && (
      <>
        <button onClick={onLogout}>Logout</button>
      </>
    )}
  </nav>
);



/************************************
 * Main App Component
 ***********************************/
function App() {
  const navigate = useNavigate();
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  useEffect(() => {
    const token = localStorage.getItem("mes25_token");
    setIsLoggedIn(!!token);
  }, []);

  const handleLogout = () => {
    localStorage.removeItem("mes25_token");
    setIsLoggedIn(false);
    navigate("/adminlogin");
  };

  return (
    <div style={{ padding: "20px" }}>
      <h1>MES25 App</h1>

      <NavBar isLoggedIn={isLoggedIn} onLogout={handleLogout} />

      <Routes>
        <Route
          path="/"
          element={
            <>
              <CandidateForm />
              <hr />
              <CandidateList />
            </>
          }
        />
        <Route path="/signup" element={<AdminSignup />} />
        <Route path="/adminlogin" element={<AdminLogin setIsLoggedIn={setIsLoggedIn} />} />

        <Route
          path="/admitcard"
          element={
            <PrivateRoute>
              <AdmitCardForm />
            </PrivateRoute>
          }
        />
        <Route
          path="/questions"
          element={
            <PrivateRoute>
              <QuestionList />
            </PrivateRoute>
          }
        />
        <Route
          path="/add-question"
          element={
            <PrivateRoute>
              <AddQuestion />
            </PrivateRoute>
          }
        />
        <Route
          path="/edit-question/:id"
          element={
            <PrivateRoute>
              <EditQuestion />
            </PrivateRoute>
          }
        />
        <Route
          path="/admin/dashboard"
          element={
            <PrivateRoute>
              <AdminDashboard />
            </PrivateRoute>
          }
        />
        <Route
          path="/admin/report"
          element={
            <PrivateRoute>
              <LiveTestReportPage />
            </PrivateRoute>
          }
        />

        <Route path="/testlogin" element={<TestLoginPage />} />
        <Route path="/start" element={<TestStartPage />} />
        <Route path="/test" element={<TestPage />} />
        <Route path="/result" element={<TestResultPage />} />

        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>

      <ToastContainer position="top-right" autoClose={3000} />
    </div>
  );
}

/************************************
 * Wrap with Router
 ***********************************/
export default function AppWithRouter() {
  return (
    <Router>
      <App />
    </Router>
  );
}

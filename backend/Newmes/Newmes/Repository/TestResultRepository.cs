using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Newmes.Api.Models;

namespace Newmes.Api.Repositories
{
    public class TestResultRepository
    {
        private readonly string _con;

        public TestResultRepository()
        {
            var cs = ConfigurationManager.ConnectionStrings["NewmesDB"];
            if (cs == null)
                throw new InvalidOperationException("Connection string 'NewmesDB' not found in Web.config.");
            _con = cs.ConnectionString;
        }

        public void Save(TestResult r)
        {
            using (var con = new SqlConnection(_con))
            using (var cmd = new SqlCommand(@"
                INSERT INTO dbo.TestResults 
                    (HallTicketNumber, CandidateName, TotalQuestions, CorrectAnswers, 
                     WrongAnswers, NotAttempted, Score, TestDate)
                OUTPUT INSERTED.Id, INSERTED.TestDate
                VALUES 
                    (@Hall, @Name, @Total, @Correct, @Wrong, @NotAtt, @Score, GETDATE());", con))
            {
                cmd.Parameters.AddWithValue("@Hall", r.HallTicketNumber);
                cmd.Parameters.AddWithValue("@Name", r.CandidateName);
                cmd.Parameters.AddWithValue("@Total", r.TotalQuestions);
                cmd.Parameters.AddWithValue("@Correct", r.CorrectAnswers);
                cmd.Parameters.AddWithValue("@Wrong", r.WrongAnswers);
                cmd.Parameters.AddWithValue("@NotAtt", r.NotAttempted);
                cmd.Parameters.AddWithValue("@Score", r.Score);

                con.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        r.Id = (int)reader["Id"];
                        r.TestDate = (DateTime)reader["TestDate"];
                    }
                }
            }
        }

        public TestResult GetByHallTicket(string hallTicket)
        {
            using (var con = new SqlConnection(_con))
            using (var cmd = new SqlCommand(
                "SELECT TOP 1 * FROM dbo.TestResults WHERE HallTicketNumber = @hall", con))
            {
                cmd.Parameters.AddWithValue("@hall", hallTicket);
                con.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.Read()) return null;

                    return new TestResult
                    {
                        Id = (int)rdr["Id"],
                        HallTicketNumber = (string)rdr["HallTicketNumber"],
                        CandidateName = (string)rdr["CandidateName"],
                        TotalQuestions = (int)rdr["TotalQuestions"],
                        CorrectAnswers = (int)rdr["CorrectAnswers"],
                        WrongAnswers = (int)rdr["WrongAnswers"],
                        NotAttempted = (int)rdr["NotAttempted"],
                        Score = (int)rdr["Score"],
                        TestDate = (DateTime)rdr["TestDate"]
                    };
                }
            }
        }

        public List<TestResult> GetAll()
        {
            var list = new List<TestResult>();

            using (var con = new SqlConnection(_con))
            using (var cmd = new SqlCommand("SELECT * FROM dbo.TestResults ORDER BY TestDate DESC", con))
            {
                con.Open();
                using (var rdr = cmd.ExecuteReader())
                    while (rdr.Read())
                        list.Add(new TestResult
                        {
                            Id = (int)rdr["Id"],
                            HallTicketNumber = (string)rdr["HallTicketNumber"],
                            CandidateName = (string)rdr["CandidateName"],
                            TotalQuestions = (int)rdr["TotalQuestions"],
                            CorrectAnswers = (int)rdr["CorrectAnswers"],
                            WrongAnswers = (int)rdr["WrongAnswers"],
                            NotAttempted = (int)rdr["NotAttempted"],
                            Score = (int)rdr["Score"],
                            TestDate = (DateTime)rdr["TestDate"]
                        });
            }

            return list;
        }

        // ⭐️ NEW: GetRankedResults
        public List<TestResult> GetRankedResults()
        {
            var list = new List<TestResult>();

            using (var con = new SqlConnection(_con))
            using (var cmd = new SqlCommand(
                "SELECT * FROM dbo.TestResults ORDER BY Score DESC, TestDate ASC", con))
            {
                con.Open();
                using (var rdr = cmd.ExecuteReader())
                    while (rdr.Read())
                        list.Add(new TestResult
                        {
                            Id = (int)rdr["Id"],
                            HallTicketNumber = (string)rdr["HallTicketNumber"],
                            CandidateName = (string)rdr["CandidateName"],
                            TotalQuestions = (int)rdr["TotalQuestions"],
                            CorrectAnswers = (int)rdr["CorrectAnswers"],
                            WrongAnswers = (int)rdr["WrongAnswers"],
                            NotAttempted = (int)rdr["NotAttempted"],
                            Score = (int)rdr["Score"],
                            TestDate = (DateTime)rdr["TestDate"]
                        });
            }

            return list;
        }
    }
}

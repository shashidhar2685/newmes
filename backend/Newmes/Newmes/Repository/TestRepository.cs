using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Newmes.Models;
using System;

namespace Newmes.Repository
{
    /// <summary>
    /// Data‑access class used ONLY by the Online Test module.
    /// </summary>
    public class TestRepository
    {
        private readonly string connStr =
            ConfigurationManager.ConnectionStrings["NewmesDB"].ConnectionString;

        /// <summary>
        /// Returns <paramref name="count"/> random MCQs,
        /// excluding the CorrectAnswer column.
        /// </summary>
        public List<QuestionForTestDto> GetRandomQuestions(int count)
        {
            var list = new List<QuestionForTestDto>();

            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(@"
                    SELECT TOP (@cnt)
                           QuestionId, QuestionText,
                           OptionA, OptionB, OptionC, OptionD
                    FROM   Questions
                    ORDER  BY NEWID()", conn))
            {
                cmd.Parameters.AddWithValue("@cnt", count);
                conn.Open();

                using (var rdr = cmd.ExecuteReader())
                    while (rdr.Read())
                    {
                        list.Add(new QuestionForTestDto
                        {
                            QuestionId = (int)rdr["QuestionId"],
                            QuestionText = rdr["QuestionText"].ToString(),
                            OptionA = rdr["OptionA"].ToString(),
                            OptionB = rdr["OptionB"].ToString(),
                            OptionC = rdr["OptionC"].ToString(),
                            OptionD = rdr["OptionD"].ToString()
                        });
                    }
            }

            return list;
        }

        public TestScoreDto EvaluateTestResult(TestResultDto dto)
        {
            int correct = 0;
            int total = dto.Answers.Count;

            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();

                foreach (var ans in dto.Answers)
                {
                    // fetch the correct letter (A/B/C/D) from DB
                    using (var cmd = new SqlCommand(
                        "SELECT CorrectAnswer FROM Questions WHERE QuestionId = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", ans.QuestionId);

                        string dbAns = (cmd.ExecuteScalar() ?? "")
                                       .ToString()
                                       .Trim()          // remove spaces
                                       .ToUpper();      // force uppercase

                        string userAns = (ans.SelectedOption ?? "")
                                         .Trim()
                                         .ToUpper();     // make sure user's letter is uppercase

                        // Optional debug line – remove when happy
                        System.Diagnostics.Debug.WriteLine(
                            $"Q{ans.QuestionId}: user {userAns}, correct {dbAns}");

                        if (dbAns == userAns)
                            correct++;
                    }
                }
            }

            return new TestScoreDto
            {
                TotalQuestions = total,
                CorrectAnswers = correct,
                WrongAnswers = total - correct,
                Score = correct          // 1 mark each
            };
        }


    }
}

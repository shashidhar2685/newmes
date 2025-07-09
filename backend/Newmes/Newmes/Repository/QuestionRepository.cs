using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Newmes.Models;

namespace Newmes.Repository
{
    public class QuestionRepository
    {
        private readonly string connectionString;

        public QuestionRepository()
        {
            var conn = ConfigurationManager.ConnectionStrings["NewmesDB"];
            if (conn == null)
                throw new Exception("Connection string 'NewmesDB' not found in Web.config.");

            connectionString = conn.ConnectionString;
        }


        // ✅ Get all questions
        public List<Question> GetAll()
        {
            List<Question> list = new List<Question>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Questions", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Question
                    {
                        QuestionId = Convert.ToInt32(reader["QuestionId"]),
                        QuestionText = reader["QuestionText"].ToString(),
                        OptionA = reader["OptionA"].ToString(),
                        OptionB = reader["OptionB"].ToString(),
                        OptionC = reader["OptionC"].ToString(),
                        OptionD = reader["OptionD"].ToString(),
                        CorrectAnswer = reader["CorrectAnswer"].ToString()
                    });
                }
                reader.Close();
            }

            return list;
        }

        // ✅ Get question by ID
        public Question GetById(int id)
        {
            Question q = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Questions WHERE QuestionId = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    q = new Question
                    {
                        QuestionId = Convert.ToInt32(reader["QuestionId"]),
                        QuestionText = reader["QuestionText"].ToString(),
                        OptionA = reader["OptionA"].ToString(),
                        OptionB = reader["OptionB"].ToString(),
                        OptionC = reader["OptionC"].ToString(),
                        OptionD = reader["OptionD"].ToString(),
                        CorrectAnswer = reader["CorrectAnswer"].ToString()
                    };
                }
                reader.Close();
            }

            return q;
        }

        // ✅ Add a new question
        public bool Add(Question q)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO Questions (QuestionText, OptionA, OptionB, OptionC, OptionD, CorrectAnswer)
                    VALUES (@QuestionText, @OptionA, @OptionB, @OptionC, @OptionD, @CorrectAnswer)", conn);

                cmd.Parameters.AddWithValue("@QuestionText", q.QuestionText);
                cmd.Parameters.AddWithValue("@OptionA", q.OptionA);
                cmd.Parameters.AddWithValue("@OptionB", q.OptionB);
                cmd.Parameters.AddWithValue("@OptionC", q.OptionC);
                cmd.Parameters.AddWithValue("@OptionD", q.OptionD);
                cmd.Parameters.AddWithValue("@CorrectAnswer", q.CorrectAnswer);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ✅ Update question
        public bool Update(Question q)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(@"
                    UPDATE Questions
                    SET QuestionText = @QuestionText,
                        OptionA = @OptionA,
                        OptionB = @OptionB,
                        OptionC = @OptionC,
                        OptionD = @OptionD,
                        CorrectAnswer = @CorrectAnswer
                    WHERE QuestionId = @QuestionId", conn);

                cmd.Parameters.AddWithValue("@QuestionText", q.QuestionText);
                cmd.Parameters.AddWithValue("@OptionA", q.OptionA);
                cmd.Parameters.AddWithValue("@OptionB", q.OptionB);
                cmd.Parameters.AddWithValue("@OptionC", q.OptionC);
                cmd.Parameters.AddWithValue("@OptionD", q.OptionD);
                cmd.Parameters.AddWithValue("@CorrectAnswer", q.CorrectAnswer);
                cmd.Parameters.AddWithValue("@QuestionId", q.QuestionId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ✅ Delete question
        public bool Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Questions WHERE QuestionId = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ✅ Get N random questions (without CorrectAnswer or CreatedOn)
        public List<Question> GetRandom(int count)
        {
            var list = new List<Question>();

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(
                @"SELECT TOP (@cnt)
              QuestionId, QuestionText, OptionA, OptionB, OptionC, OptionD
          FROM Questions
          ORDER BY NEWID()", conn))
            {
                cmd.Parameters.AddWithValue("@cnt", count);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Question
                        {
                            QuestionId = (int)reader["QuestionId"],
                            QuestionText = reader["QuestionText"].ToString(),
                            OptionA = reader["OptionA"].ToString(),
                            OptionB = reader["OptionB"].ToString(),
                            OptionC = reader["OptionC"].ToString(),
                            OptionD = reader["OptionD"].ToString()
                        });
                    }
                }
            }

            return list;
        }

    }
}

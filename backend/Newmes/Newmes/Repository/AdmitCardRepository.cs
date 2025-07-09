using System;
using System.Configuration;
using System.Data.SqlClient;
using Newmes.Models; // Make sure your AdmitCardModel is here

namespace Newmes.Repository
{
    public class AdmitCardRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["NewmesDB"].ConnectionString;

        public string GenerateAdmitCard(AdmitCardModel model)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO AdmitCards (CandidateId, AdmitCardNo, ExamDate, ExamCenter, IsGenerated) " +
                               "VALUES (@CandidateId, @AdmitCardNo, @ExamDate, @ExamCenter, 1)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CandidateId", model.CandidateId);
                cmd.Parameters.AddWithValue("@AdmitCardNo", model.AdmitCardNo);
                cmd.Parameters.AddWithValue("@ExamDate", model.ExamDate);
                cmd.Parameters.AddWithValue("@ExamCenter", model.ExamCenter);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0 ? "Admit card generated successfully." : "Admit card generation failed.";
            }
        }
    }
}

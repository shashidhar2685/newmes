using System.Configuration;
using System.Data.SqlClient;
using Newmes.Models;

namespace Newmes.Repository
{
    public class CandidateRepository
    {
        private readonly string connStr =
            ConfigurationManager.ConnectionStrings["NewmesDB"].ConnectionString;

        /// <summary>
        /// Returns the candidate if hall‑ticket & password match; otherwise null.
        /// </summary>
        public Candidate VerifyLogin(string hallTicket, string password)
        {
            Candidate cand = null;

            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(@"
                SELECT TOP 1 CandidateId, FullName, HallTicketNo
                FROM   Candidates
                WHERE  HallTicketNo = @ht AND Password = @pwd", conn))
            {
                cmd.Parameters.AddWithValue("@ht", hallTicket);
                cmd.Parameters.AddWithValue("@pwd", password);
                conn.Open();

                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        cand = new Candidate
                        {
                            CandidateId = (int)rdr["CandidateId"],
                            FullName = rdr["FullName"].ToString(),
                            HallTicketNo = rdr["HallTicketNo"].ToString()
                        };
                    }
                }
            }
            return cand;
        }
    }
}

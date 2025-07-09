using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;
using Newmes.Models;
using System.Net;
using System.Collections.Generic;
using System.Web.Http.Cors;

namespace Newmes.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CandidateController : ApiController
    {
        [HttpPost]
        [Route("api/Candidate/Register")]
        public IHttpActionResult RegisterCandidate([FromBody] Candidate candidate)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewmesDB"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Candidates (FullName, Email, Phone, Gender, DateOfBirth) " +
                                   "VALUES (@FullName, @Email, @Phone, @Gender, @DateOfBirth)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FullName", candidate.FullName);
                    cmd.Parameters.AddWithValue("@Email", candidate.Email);
                    cmd.Parameters.AddWithValue("@Phone", candidate.Phone);
                    cmd.Parameters.AddWithValue("@Gender", candidate.Gender);
                    cmd.Parameters.AddWithValue("@DateOfBirth", candidate.DateOfBirth);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                return Ok("Candidate Registered Successfully");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new
                {
                    message = "Registration failed.",
                    exception = ex.Message,
                    stack = ex.StackTrace
                });
            }
        }

        [HttpGet]
        [Route("api/Candidate/GetAll")]
        public IHttpActionResult GetAllCandidates()
        {
            List<Candidate> candidates = new List<Candidate>();

            string connectionString = ConfigurationManager.ConnectionStrings["NewmesDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Candidates";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Candidate c = new Candidate
                    {
                        CandidateId = Convert.ToInt32(reader["CandidateId"]),
                        FullName = reader["FullName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"])
                    };
                    candidates.Add(c);
                }
            }

            return Ok(candidates);
        }


    }
}

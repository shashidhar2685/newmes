using System;
using System.Data.SqlClient;
using System.Configuration;
using Newmes.WebAPI.Models; // You’ll create this model

namespace Newmes.WebAPI.Repository
{
    public class AdminRepository
    {
        // Read connection string from Web.config
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["NewmesDB"].ConnectionString;

        // ✅ Signup - Insert Admin
        public bool RegisterAdmin(Admin admin)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Admins (Username, Email, PasswordHash) VALUES (@Username, @Email, @PasswordHash)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", admin.Username);
                cmd.Parameters.AddWithValue("@Email", admin.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", admin.PasswordHash);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result > 0; // Return true if insert succeeded
            }
        }

        // ✅ Login - Get Admin by Username
        public Admin GetAdminByUsername(string username)
        {
            Admin admin = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Admins WHERE Username = @Username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    admin = new Admin
                    {
                        AdminId = Convert.ToInt32(reader["AdminId"]),
                        Username = reader["Username"].ToString(),
                        Email = reader["Email"].ToString(),
                        PasswordHash = reader["PasswordHash"].ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                    };
                }
            }

            return admin;
        }
    }
}

using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrateTrack.Database;

namespace CrateTrack.Database
{
    internal class User
    {
        //  Todo: error handling, transaction management
        //  Todo: Hashing mechanism. Popular choices include BCrypt and Argon2.
        //  Todo: Hashing mechanism. Popular choices include BCrypt and Argon2.
        //  Ensure your connection string is stored securely, preferably in a configuration that's not hard-coded.
        //  The Npgsql namespace and related methods will work once the Npgsql library is added to the project.

        internal DbHelper _dbHelper;

        public bool Authorised = false;
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string SessionToken { get; set; }
        public DateTime JoinDate { get; set; }

        private string HashPassword(string password)
        {
            // Implement a hashing mechanism here, e.g., using BCrypt or similar.
            // For the sake of example, I'll just return the password.
            return password;
        }

        public void SetPassword(string password)
        {
            this.PasswordHash = HashPassword(password);
        }

        // this function is used to signing users up to the database
        public bool CreateUser()
        {
            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO users (email, username, password_hash, join_date) VALUES (@email, @username, @password, @join_date) RETURNING user_id;";
                    cmd.Parameters.AddWithValue("username", Username);
                    cmd.Parameters.AddWithValue("email", Email);
                    cmd.Parameters.AddWithValue("password", PasswordHash);
                    cmd.Parameters.AddWithValue("join_date", JoinDate);

                    UserId = Convert.ToInt32(cmd.ExecuteScalar());
                    return UserId > 0;
                }
            }
        }

        public bool Authenticate(string password, string username)
        {
            // Use BCrypt or similar here to check the password against the hash
            // return BCrypt.Verify(password, PasswordHash);

            string username_to_verify = null;
            string password_to_verify = null;

            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT username, password_hash FROM users WHERE username = @username", conn))
                {
                    cmd.Parameters.AddWithValue("username", username);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            username_to_verify = reader.GetString(0);
                            password_to_verify = reader.GetString(1);
                        }
                    }
                }
                if (password == null || username == null)
                {
                    return false;
                }
                else if (password == password_to_verify && username == username_to_verify)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void CreateSession()
        {
            // Generate a session token. A GUID for simplicity:
            SessionToken = Guid.NewGuid().ToString();

            using var conn = _dbHelper.GetConnection();
            conn.Open();

            using var cmd = new NpgsqlCommand("UPDATE users SET session_token = @token WHERE user_id = @id", conn);
            cmd.Parameters.AddWithValue("token", SessionToken);
            cmd.Parameters.AddWithValue("id", UserId);

            cmd.ExecuteNonQuery();
        }

        public User GetBySessionToken(string token)
        {
            using var conn = _dbHelper.GetConnection();
            conn.Open();

            using var cmd = new NpgsqlCommand("SELECT user_id, username, password_hash FROM users WHERE session_token = @token", conn);
            cmd.Parameters.AddWithValue("token", token);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    UserId = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    PasswordHash = reader.GetString(2),
                    SessionToken = token
                };
            }
            return null;
        }
    }
}

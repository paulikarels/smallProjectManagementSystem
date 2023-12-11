using backend.Models;


using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;


namespace backend.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private IConfiguration _configuration;
        private string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ProjectManagementCon");
        }

        
        public IEnumerable<User> GetAll()
        {
            string query = @"SELECT * FROM dbo.Users";
            DataTable table = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(_connectionString))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            var users = new List<User>();
            foreach (DataRow row in table.Rows)
            {
                users.Add(new User(
                    (int)row["UserID"],
                    row["Username"].ToString(),
                    row["Password"].ToString()
                ));
            }

            return users;

        }
        public User GetById(int userId)
        {
            string query = @"SELECT * FROM dbo.Users WHERE UserID = @UserID";
            User user = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userId);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        user = new User(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2)

                        );
                    }
                    reader.Close();

                }
                connection.Close();
            }

            return user;

        }

        public bool Add(User user)
        {
            string query = @"INSERT INTO dbo.Users 
                             (Username, Password) 
                             VALUES (@Username, @Password)";
            try
            {
                using (SqlConnection myCon = new SqlConnection(_connectionString))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@Username", user.Username);
                        myCommand.Parameters.AddWithValue("@Password", user.Password);
                        myCommand.ExecuteNonQuery();
                        myCon.Close();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Update(User user)
        {
            string query = @"UPDATE dbo.Users 
                                SET Username = @Username, 
                                Password = @Password
                                WHERE UserID = @UserID;";

            using (SqlConnection myCon = new SqlConnection(_connectionString))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@UserID", user.UserID);
                    myCommand.Parameters.AddWithValue("@Username", user.Username);
                    myCommand.Parameters.AddWithValue("@Password", user.Password);

                    int rowsAffected = myCommand.ExecuteNonQuery();
                    myCon.Close();

                    return rowsAffected > 0;

                }
            }
        }

        public bool Delete(int userId)
        {
            string query = @"DELETE FROM dbo.Users 
                             WHERE UserID = @UserID";

            using (SqlConnection myCon = new SqlConnection(_connectionString))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@UserID", userId);
                    int rowsAffected = myCommand.ExecuteNonQuery();
                    myCon.Close();

                    return rowsAffected > 0;
                }
            }
        }

    }
}

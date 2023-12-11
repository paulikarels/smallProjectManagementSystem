using backend.Models;

using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;


namespace backend.Repositories
{
    public class ProjectRepository : IRepository<Project>
    {
        private IConfiguration _configuration;
        private string _connectionString;

        public ProjectRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ProjectManagementCon");
        }


        public IEnumerable<Project> GetAll()
        {
            string query = @"SELECT * FROM dbo.Projects";
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
            var projects = new List<Project>();
            foreach (DataRow row in table.Rows)
            {
                projects.Add(new Project(
                    (int)row["ProjectID"],
                    row["ProjectName"].ToString(),
                    row["ProjectDescription"].ToString(),
                    (DateTime)row["StartDate"],
                    (DateTime)row["EndDate"],
                    (int)row["UserID"]

                ));
            }

            return projects;

        }
        public Project GetById(int projectId)
        {
            string query = @"SELECT * FROM dbo.Projects WHERE ProjectID = @ProjectID";
            Project project = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProjectID", projectId);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        project = new Project(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetDateTime(3),
                            reader.GetDateTime(4),
                            reader.GetInt32(5)

                        );
                    }
                    reader.Close();

                }
                connection.Close();
            }

            return project;

        }

        public bool Add(Project project)
        {
            string query = @"INSERT INTO dbo.Projects 
                             (ProjectName, ProjectDescription, StartDate, EndDate, UserID) 
                             VALUES (@ProjectName, @ProjectDescription, @StartDate, @EndDate, @UserID)";
            try
            {
                using (SqlConnection myCon = new SqlConnection(_connectionString))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                        myCommand.Parameters.AddWithValue("@ProjectDescription", project.ProjectDescription);
                        myCommand.Parameters.AddWithValue("@StartDate", project.StartDate);
                        myCommand.Parameters.AddWithValue("@EndDate", project.EndDate);
                        myCommand.Parameters.AddWithValue("@UserID", project.UserID);
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
        public bool Update(Project project)
        {
            string query = @"UPDATE dbo.Projects 
                                SET  ProjectName = @ProjectName,
                                ProjectDescription = @ProjectDescription,
                                StartDate = @StartDate,
                                EndDate = @EndDate
                                WHERE ProjectID = @ProjectID;";

            using (SqlConnection myCon = new SqlConnection(_connectionString))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                    myCommand.Parameters.AddWithValue("@ProjectDescription", project.ProjectDescription);
                    myCommand.Parameters.AddWithValue("@StartDate", project.StartDate);
                    myCommand.Parameters.AddWithValue("@EndDate", project.EndDate);
                    myCommand.Parameters.AddWithValue("@ProjectID", project.ProjectID);

                    int rowsAffected = myCommand.ExecuteNonQuery();
                    myCon.Close();

                    return rowsAffected > 0;

                }
            }
        }

        public bool Delete(int projectId)
        {
            string query = @"DELETE FROM dbo.Projects 
                             WHERE ProjectID = @ProjectID";

            using (SqlConnection myCon = new SqlConnection(_connectionString))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ProjectID", projectId);
                    int rowsAffected = myCommand.ExecuteNonQuery();
                    myCon.Close();

                    return rowsAffected > 0;
                }
            }
        }

    }
}

using backend.Models;

using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Task = backend.Models.Task;
using TaskStatus = backend.Models.TaskStatus;


namespace backend.Repositories
{
    public class TaskRepository : IRepository<Task>
    {
        private IConfiguration _configuration;
        private string _connectionString;

        public TaskRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ProjectManagementCon");
        }


        public IEnumerable<Task> GetAll()
        {
            string query = @"SELECT * FROM dbo.Tasks";
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
            var tasks = new List<Task>();
            foreach (DataRow row in table.Rows)
            {
                TaskStatus taskStatus = (TaskStatus)Enum.Parse(typeof(TaskStatus), row["Status"].ToString());
                tasks.Add(new Task(
                    (int)row["TaskID"],
                    row["TaskName"].ToString(),
                    row["TaskDescription"].ToString(),
                    (DateTime)row["DueDate"],
                    (int)row["ProjectID"],
                    (int)row["UserID"],
                    taskStatus
                ));
            }

            return tasks;

        }
        public Task GetById(int taskId)
        {
            string query = @"SELECT * FROM dbo.Tasks WHERE TaskID = @TaskID";
            Task task = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TaskID", taskId);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        task = new Task(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetDateTime(3),
                            reader.GetInt32(4),
                            reader.GetInt32(5),
                            Enum.Parse<TaskStatus>(reader.GetString(6))
                        );
                    }
                    reader.Close();

                }
                connection.Close();
            }

            return task;

        }

        public bool Add(Task task)
        {
            string query = @"INSERT INTO dbo.Tasks 
                             (TaskName, TaskDescription, DueDate, ProjectID, UserID, Status) 
                             VALUES (@TaskName, @TaskDescription, @DueDate, @ProjectID, @UserID, @Status)";
            try
            {
                using (SqlConnection myCon = new SqlConnection(_connectionString))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {

                        myCommand.Parameters.AddWithValue("@TaskName", task.TaskName);
                        myCommand.Parameters.AddWithValue("@TaskDescription", task.TaskDescription);
                        myCommand.Parameters.AddWithValue("@DueDate", task.DueDate);
                        myCommand.Parameters.AddWithValue("@ProjectID", task.ProjectID);
                        myCommand.Parameters.AddWithValue("@UserID", task.UserID);
                        myCommand.Parameters.AddWithValue("@Status", task.Status.ToString());
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
        public bool Update(Task task)
        {
            string query = @"UPDATE dbo.Tasks 
                                SET TaskName = @TaskName, 
                                TaskDescription = @TaskDescription,
                                DueDate = @DueDate,
                                Status = @Status
                                WHERE TaskID = @TaskID;";

            using (SqlConnection myCon = new SqlConnection(_connectionString))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@TaskName", task.TaskName);
                    myCommand.Parameters.AddWithValue("@TaskDescription", task.TaskDescription);
                    myCommand.Parameters.AddWithValue("@DueDate", task.DueDate);
                    myCommand.Parameters.AddWithValue("@Status", task.Status.ToString());
                    myCommand.Parameters.AddWithValue("@TaskID", task.TaskID);

                    int rowsAffected = myCommand.ExecuteNonQuery();
                    myCon.Close();

                    return rowsAffected > 0;

                }
            }
        }

        public bool Delete(int taskID)
        {
            string query = @"DELETE FROM dbo.Tasks 
                             WHERE TaskID = @TaskID";

            using (SqlConnection myCon = new SqlConnection(_connectionString))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@TaskID", taskID);
                    int rowsAffected = myCommand.ExecuteNonQuery();
                    myCon.Close();

                    return rowsAffected > 0;
                }
            }
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace backend.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ProjectManagementController : ControllerBase
    {
        private IConfiguration _configuration;

        public ProjectManagementController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //[HttpGet]
        //[Route("GetAll")]

        public JsonResult GetUsers()
        {
            string query = "SELECT * FROM dbo.Users";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("ProjectManagementCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
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
            return new JsonResult(table);
        }
        
        
    }
}

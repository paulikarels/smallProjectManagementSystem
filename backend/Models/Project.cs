
namespace smallProjectbackendManagement.Models
{


    public class Project
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserID { get; set; }


        public Project(int projectID, string projectName, string projectDescription,  DateTime startDate, DateTime endDate,int userID)
        {
            ProjectID = projectID;
            ProjectName = projectName;
            ProjectDescription = projectDescription;
            StartDate = startDate;
            EndDate = endDate;
            UserID = userID;
        }

    }
}
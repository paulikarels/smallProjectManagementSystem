

namespace backend.Models
{
    public enum TaskStatus
    {
        Started,
        Done,
        Cancelled
    }

    public class Task
    {
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime DueDate { get; set; }
        public int ProjectID { get; set; }
        public int UserID { get; set; }


        public Task(int taskID, string taskName, string taskDescription, DateTime dueDate, int projectID, int userID, TaskStatus status)
        {
            TaskID = taskID;
            TaskName = taskName;
            TaskDescription = taskDescription;
            DueDate = dueDate;
            ProjectID = projectID;
            UserID = userID;
            Status = status;
        }

    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ERENCAN_TEKIN_19290273_TO_DO_API.Pages
{
    public class CreateModel : PageModel
    {
        public ToDoTaskInfo todotaskInfo = new ToDoTaskInfo();
        public String errorMsg="", successMsg="";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            todotaskInfo.TaskTitle = Request.Form["taskTitle"];
            todotaskInfo.TaskDescription = Request.Form["taskDescription"];
            todotaskInfo.TaskPriority = Request.Form["taskPriority"];

            
        
            if(todotaskInfo.TaskTitle.Length == 0 || todotaskInfo.TaskDescription.Length == 0 || todotaskInfo.TaskPriority.Length > 1 || todotaskInfo.TaskPriority.Length == 0)
            {
                errorMsg = "All fields must be filled correctly !";
                return;
            }

            if (int.Parse(todotaskInfo.TaskPriority) >= 3 || int.Parse(todotaskInfo.TaskPriority) < 0)
            {
                errorMsg = "Priority should be in between 0 - 2. 0 = Min Priority,  1 = AVG Priortiy,  2 =  MAX Priority";
                return;
            }

            try
            {
                var UserID = TempData["UserID"];
                TempData["UserID"] = UserID;  // re-prepare USerID for Create Page

                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=tododatabase;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "INSERT INTO Todotasks" +
                    "(UserID, TaskTitle, TaskDescription, TaskPriority) VALUES " +
                    "(@UserID, @TaskTitle, @TaskDescription, @TaskPriority); SELECT SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", UserID);
                        command.Parameters.AddWithValue("@TaskTitle", todotaskInfo.TaskTitle);
                        command.Parameters.AddWithValue("@TaskDescription", todotaskInfo.TaskDescription);
                        command.Parameters.AddWithValue("@TaskPriority", todotaskInfo.TaskPriority);

                        command.ExecuteNonQuery();
                    }
                }
                

            }
            catch(Exception ex)
            {
                errorMsg = ex.Message;
                return;
            }

            todotaskInfo.TaskTitle = "";
            todotaskInfo.TaskDescription = "";
            todotaskInfo.TaskPriority = "";
            successMsg = "New Task Added Successfully !";

            Response.Redirect("/Home");
        }
    }
}

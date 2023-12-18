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
            todotaskInfo.taskName = Request.Form["taskTitle"];
            todotaskInfo.taskDescription = Request.Form["taskDescription"];
            todotaskInfo.taskPriority = Request.Form["taskPriority"];
        
            if(todotaskInfo.taskName.Length == 0 || todotaskInfo.taskDescription.Length == 0 || todotaskInfo.taskPriority.Length > 1)
            {
                errorMsg = "All fields must be filled correctly !";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=todoDatabase;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "INSERT INTO todotasks" +
                    "(taskName,taskDescription,taskPriority) VALUES " +
                    "(@taskName, @taskDescription, @taskPriority);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@taskName", todotaskInfo.taskName);
                        command.Parameters.AddWithValue("@taskDescription", todotaskInfo.taskDescription);
                        command.Parameters.AddWithValue("@taskPriority", todotaskInfo.taskPriority);

                        command.ExecuteNonQuery();
                    }
                }
                

            }
            catch(Exception ex)
            {
                errorMsg = ex.Message;
                return;
            }

            todotaskInfo.taskName = "";
            todotaskInfo.taskDescription = "";
            todotaskInfo.taskPriority = "";
            successMsg = "New Task Added Successfully !";

            Response.Redirect("/Home");
        }
    }
}

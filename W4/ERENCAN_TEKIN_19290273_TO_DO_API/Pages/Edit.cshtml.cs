using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ERENCAN_TEKIN_19290273_TO_DO_API.Pages
{
    public class EditModel : PageModel
    {
        public ToDoTaskInfo todotaskInfo = new ToDoTaskInfo();
        public String errorMsg = "";
        public String successMsg = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=todoDatabase;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "SELECT * FROM todotasks WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                todotaskInfo.id = "" + reader.GetInt32(0);
                                todotaskInfo.taskName = reader.GetString(1);
                                todotaskInfo.taskDescription = reader.GetString(2);
                                todotaskInfo.taskPriority = reader.GetString(3);
                            }
                        }
                        
                    }
                }
            }
            catch(Exception ex)
            {
                errorMsg = ex.Message;
            }
        }

        public void OnPost()
        {
            todotaskInfo.id = Request.Form["id"];
            todotaskInfo.taskName = Request.Form["taskName"];
            todotaskInfo.taskDescription = Request.Form["taskDescription"];
            todotaskInfo.taskPriority = Request.Form["taskPriority"];
        
            if(todotaskInfo.id.Length == 0 || todotaskInfo.taskName.Length == 0 ||
                todotaskInfo.taskDescription.Length == 0 || todotaskInfo.taskPriority.Length == 0)
            {
                errorMsg = "All fields must be filled correctly !";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=todoDatabase;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE todotasks " +
                        "SET taskName=@taskName, taskDescription=@taskDescription, taskPriority=@taskPriority " +
                        "WHERE id=@id";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
        
                        command.Parameters.AddWithValue("@taskName", todotaskInfo.taskName);
                        command.Parameters.AddWithValue("@taskDescription", todotaskInfo.taskDescription);
                        command.Parameters.AddWithValue("@taskPriority", todotaskInfo.taskPriority);
                        command.Parameters.AddWithValue("@id", todotaskInfo.id);


                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return;
            }

            Response.Redirect("/Home");
        }
        
    }
}

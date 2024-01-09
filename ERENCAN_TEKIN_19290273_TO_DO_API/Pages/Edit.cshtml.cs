using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ERENCAN_TEKIN_19290273_TO_DO_API.Pages
{
    public class EditModel : PageModel
    {
        public ToDoTaskInfo todotaskInfo = new ToDoTaskInfo();
        public String selectedRadioButton="";
        public String errorMsg = "";
        public String successMsg = "";
        [BindProperty]
        public string radioButtonOptionEdit { get; set; }
        public void OnGet()
        {
            String TaskID = Request.Query["TaskID"];
            String UserID = Request.Query["UserID"];
            try
            {
                

                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=tododatabase;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "SELECT * FROM todotasks WHERE TaskID=@TaskID AND UserID = @UserID";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@TaskID", TaskID);
                        command.Parameters.AddWithValue("@UserID", UserID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                todotaskInfo.TaskID = "" + reader.GetInt32(0).ToString();
                                todotaskInfo.UserID = reader.GetInt32(1).ToString();
                                todotaskInfo.TaskTitle = reader.GetString(2);
                                todotaskInfo.TaskDescription = reader.GetString(3);
                                todotaskInfo.TaskPriority = reader.GetInt32(4).ToString();
                                selectedRadioButton = todotaskInfo.TaskPriority;
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
            todotaskInfo.TaskID = Request.Form["TaskID"];
            todotaskInfo.UserID = Request.Form["UserID"];
            TempData["UserID"] = todotaskInfo.UserID;  // re-prepare USerID for Create Page
            todotaskInfo.TaskTitle = Request.Form["TaskTitle"];
            todotaskInfo.TaskDescription = Request.Form["TaskDescription"];
            todotaskInfo.TaskPriority = radioButtonOptionEdit;




            if (todotaskInfo.TaskTitle.Length == 0 || todotaskInfo.TaskDescription.Length == 0)
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
                    String sql = "UPDATE Todotasks " +
                        "SET TaskTitle=@TaskTitle, TaskDescription=@TaskDescription, TaskPriority=@TaskPriority " +
                        "WHERE TaskID=@TaskID AND UserID=@UserID";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", todotaskInfo.UserID);
                        command.Parameters.AddWithValue("@TaskTitle", todotaskInfo.TaskTitle);
                        command.Parameters.AddWithValue("@TaskDescription", todotaskInfo.TaskDescription);
                        command.Parameters.AddWithValue("@TaskPriority", todotaskInfo.TaskPriority);
                        command.Parameters.AddWithValue("@TaskID", todotaskInfo.TaskID);


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

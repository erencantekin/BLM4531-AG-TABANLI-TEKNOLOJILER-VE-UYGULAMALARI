using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace ERENCAN_TEKIN_19290273_TO_DO_API.Pages
{
    public class HomeModel : PageModel
    {
        [BindProperty]
        public string SelectedOption { get; set; }



        public List<ToDoTaskInfo> listToDoTasks = new List<ToDoTaskInfo>();

        public void OnGet()
        {

            try
            {
                var UserID = TempData["UserID"];
                TempData["UserID"] = UserID;

                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=tododatabase;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Todotasks WHERE UserID = @UserID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", UserID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ToDoTaskInfo todotaskInfo = new ToDoTaskInfo();
                                
                                todotaskInfo.TaskID = "" + reader.GetInt32(0).ToString();
                                todotaskInfo.UserID = reader.GetInt32(1).ToString();
                                todotaskInfo.TaskTitle = reader.GetString(2);
                                todotaskInfo.TaskDescription = reader.GetString(3);
                                todotaskInfo.TaskPriority = reader.GetInt32(4).ToString();
                                todotaskInfo.CreatedAt = reader.GetDateTime(5).ToString();

                                listToDoTasks.Add(todotaskInfo);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        public IActionResult OnPost()
        {
            Console.WriteLine(SelectedOption);
            return Page();
        }
    }

    public class ToDoTaskInfo
    {
        public string UserID;
        public string TaskID;
        public string TaskTitle;
        public string TaskDescription;
        public string TaskPriority;
        public string CreatedAt;
    }
}

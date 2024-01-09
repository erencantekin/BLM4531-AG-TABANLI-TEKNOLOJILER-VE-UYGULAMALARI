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

                                String title = reader.GetString(2);
                                if(reader.GetString(2).Length > 20)
                                {
                                    title = reader.GetString(2).Substring(0, 20) +"...";
                                }

                                todotaskInfo.TaskTitle = title;

                                String description = reader.GetString(3);
                                if (reader.GetString(3).Length > 20)
                                {
                                    description = reader.GetString(3).Substring(0,20) +"...";
                                }
                                todotaskInfo.TaskDescription = description;
                                todotaskInfo.TaskPriority = reader.GetInt32(4).ToString() == "0" ? "MIN" : reader.GetInt32(4).ToString() == "1" ? "AVG" : reader.GetInt32(4).ToString() == "2" ? "MAX" : "";
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

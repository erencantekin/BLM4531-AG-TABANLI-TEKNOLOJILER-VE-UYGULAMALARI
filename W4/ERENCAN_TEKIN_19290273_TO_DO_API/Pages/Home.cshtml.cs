using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ERENCAN_TEKIN_19290273_TO_DO_API.Pages
{
    public class HomeModel : PageModel
    {
        public List<ToDoTaskInfo> listToDoTasks = new List<ToDoTaskInfo>();

        public void OnGet()
        {

            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=todoDatabase;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM todotasks";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ToDoTaskInfo todotaskInfo = new ToDoTaskInfo();
                                todotaskInfo.id = "" + reader.GetInt32(0);
                                todotaskInfo.taskName = reader.GetString(1);
                                todotaskInfo.taskDescription = reader.GetString(2);
                                todotaskInfo.taskPriority = reader.GetString(3);
                                todotaskInfo.created_at = reader.GetDateTime(4).ToString();

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
    }

    public class ToDoTaskInfo
    {
        public string id;
        public string taskName;
        public string taskDescription;
        public string taskPriority;
        public string created_at;
    }
}

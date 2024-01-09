using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ERENCAN_TEKIN_19290273_TO_DO_API.Pages
{
    public class IndexModel : PageModel
    {
        public string errorMsg = "";
        public string Username { get; set; }
        public string Password { get; set; }

        public IActionResult OnGet(string buttonClicked)
        {
            Username = Request.Query["username"];
            Password = Request.Query["password"];


            if (buttonClicked == "loginButton")
            {
                int isUserAvailable = GetUsers(Username, Password);
                if (isUserAvailable > -1)
                {
                    return RedirectToPage("/Home");
                }
                else if(isUserAvailable == -1)
                {
                    errorMsg = "Wrong Username or Password, please check your credentials.";
                    TempData["AlertMessage"] = errorMsg;
                }
                else
                {
                    if (Username == null || Password == null || Username.Length < 6 || Username.Length >= 25 || Password.Length < 6 || Password.Length >= 25) 
                    {
                        errorMsg = "Wrong Username or Password, please check your credentials.";
                        TempData["AlertMessage"] = errorMsg;
                    }
                }
            }
            else if (buttonClicked == "signupButton")
            {
                return RedirectToPage("/SignUp");
            }
            return Page();

        }

        public void OnPost()
        {
            
        }

        private int GetUsers(string username, string password)
        {
            string connectionString = "Data Source=.\\sqlExpress;Initial Catalog=tododatabase;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Users";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if(reader.GetString(1) == username && reader.GetString(2) == password)
                            {
                                TempData["UserID"] = reader.GetInt32(0);
                                return reader.GetInt32(0);
                            }   
                        }
                        return -1;
                    }
                }
            }
        }

    }

    
}

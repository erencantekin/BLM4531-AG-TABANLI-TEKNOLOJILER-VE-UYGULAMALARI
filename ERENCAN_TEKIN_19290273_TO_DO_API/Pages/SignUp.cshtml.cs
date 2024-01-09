using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ERENCAN_TEKIN_19290273_TO_DO_API.Pages
{
    public class SignUpModel : PageModel
    {
        public String errorMsg = "";
        public string SignUpUsername { get; set; }
        public string SignUpPassword { get; set; }


        public IActionResult OnGet(string buttonClicked)
        {
                                                
            SignUpUsername = Request.Query["signupusername"];
            SignUpPassword = Request.Query["signuppassword"];

            if (buttonClicked == "SignupButton")
            {
                if (SignUpUsername.Trim() == null || SignUpPassword.Trim() == null || SignUpUsername.Trim().Length < 6 || SignUpUsername.Trim().Length >= 25 || SignUpPassword.Trim().Length < 6 || SignUpPassword.Trim().Length >= 25)
                {
                    errorMsg = "Username and Password length should be in range of 6 and 25.";
                    TempData["AlertMessage"] = errorMsg;

                }
                else if ((SignUpUsername.Trim() != null && SignUpPassword.Trim() != null) && (SignUpUsername.Trim().Length >= 6 && SignUpUsername.Trim().Length < 25 && SignUpPassword.Trim().Length >= 6 && SignUpPassword.Trim().Length < 25))
                {
                    if(GetUsers(SignUpUsername, SignUpPassword) != -1)
                    {
                        errorMsg = "This Username is available ! Please create another.";
                        TempData["AlertMessage"] = errorMsg;
                    }
                    errorMsg = "Successful ! Please enter your credentials and login.";
                    TempData["AlertMessage"] = errorMsg;
                    AddToUsersDB(SignUpUsername, SignUpPassword);
                    return RedirectToPage("/Index");
                }
            }
            else if(buttonClicked == "LoginButton")
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }
        public void OnPost() { }

        private int AddToUsersDB(string username, string password)
        {
            string connectionString = "Data Source=.\\sqlExpress;Initial Catalog=tododatabase;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Users (Username, Password) VALUES (@SignUpUsername, @SignUpPassword); SELECT SCOPE_IDENTITY();";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SignUpUsername", username);
                    command.Parameters.AddWithValue("@SignUpPassword", password);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
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
                            if (reader.GetString(1) == SignUpUsername && reader.GetString(2) == SignUpPassword)
                            {
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

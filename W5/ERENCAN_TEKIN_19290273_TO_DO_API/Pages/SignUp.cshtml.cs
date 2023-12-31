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
                if ((SignUpUsername != null && SignUpPassword != null) && (SignUpUsername.Length < 6 || SignUpUsername.Length > 25 || SignUpPassword.Length < 6 || SignUpPassword.Length > 25))
                {
                    errorMsg = "Username and Password length should be in range of 6 and 25.";
                    TempData["AlertMessage"] = errorMsg;

                }
                else if ((SignUpUsername != null && SignUpPassword != null) && (SignUpUsername.Length > 6 && SignUpUsername.Length < 25 && SignUpPassword.Length > 6 && SignUpPassword.Length < 25))
                {
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
    }
}

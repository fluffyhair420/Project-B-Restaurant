using Newtonsoft.Json;
using System;
using System.Threading.Tasks;


namespace Restaurant
{
    public class UserRegister : UserInfo
    {
        static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/User.json"));
        static string json = File.ReadAllText(path);

        public void Register()
        {
            
            Console.WriteLine("\n=== Register ===");
            Console.WriteLine("Press Q, followed by Enter to go back to the Homepage at any given time.\n");
            Console.WriteLine("* Required field.");
            // Email vragen
            string userEmail = "";
            bool emailValid = false;
            while (emailValid == false)
            {
                Console.Write("Email*: ");
                userEmail = Console.ReadLine();
                if (userEmail == "q" || userEmail == "Q")
                {
                    Program.Main();
                    break;
                }
                emailValid = UserCheck.EmailCheck(userEmail);
                if (emailValid == false)
                {
                    Console.WriteLine(@"Email address should only contain:
- Letters
- Numbers
- An @
- Ends with .com or similar
");
                }
            }

            // Email check of email overeenkomt met vorig ingevoerde email
            string userEmailCheck = "";
            bool emailVerification = false;
            while (emailVerification == false)
            {
                Console.Write("Enter your email again to make sure you are using the correct email*: ");
                userEmailCheck = Console.ReadLine();
                if (userEmailCheck == "q" || userEmailCheck == "Q")
                {
                    Program.Main();
                    break;
                }

                if (userEmailCheck == userEmail)
                {
                    emailVerification = true;
                }
            }


            // Wachtwoord vragen
            string userPassWord = "";
            bool passwordValid = false;
            while (passwordValid == false)
            {
                Console.WriteLine(@"
Password should contain at least
- 8 characters
- 1 lower case letter
- 1 capital letter
- 1 number
");
                Console.Write("Password*: ");
                userPassWord = Console.ReadLine();
                if (userPassWord == "q" || userPassWord == "Q")
                {
                    Program.Main();
                    break;
                }
                passwordValid = UserCheck.PasswordCheck(userPassWord);
            }

            // Wachtwoord check of wachtwoord overeenkomt met vorig ingevoerde wachtwoord
            string userPassCheck = "";
            bool passVerification = false;
            while (passVerification == false)
            {
                Console.Write("Enter your password again to make sure you are using the correct password*: ");
                userPassCheck = Console.ReadLine();
                if (userPassCheck == "q" || userPassCheck == "Q")
                {
                    Program.Main();
                    break;
                }
                if (userPassCheck == userPassWord)
                {
                    passVerification = true;
                }
            }

            // Check if Email already exists in JSON file
            dynamic data = JsonConvert.DeserializeObject(json);
            bool emailAlreadyExists = false;
            if (data != null)
            {
                foreach (var item in data)
                {
                    // Email already exists
                    if (item.Email == userEmailCheck)
                    {
                        Console.Write(@"
An account using this email address already exists.
Do you want to login instead?
typ ""Y"" or ""N"": ");
                        emailAlreadyExists = true;
                        bool wrongInput = true;
                        while (wrongInput)
                        {
                            string userInput = Console.ReadLine().ToUpper();
                            switch (userInput)
                            {
                                case "Y":
                                    wrongInput = false;
                                    UserLogin userLogin = new UserLogin();
                                    userLogin.Login();
                                    break;

                                case "N":
                                    wrongInput = false;
                                    Console.WriteLine(@"
May you decide to continue with an account later on, please register with
a different email address.");
                                    Program.Main();
                                    break;

                                default:
                                    Console.Write("\nInvalid input. Please typ \"Y\" or \"N\": ");
                                    break;
                            }
                        }
                        break;
                    }
                }
            }
        

            if (!emailAlreadyExists)
            {
                // Checks if the user's inputs are correct and creates the UserInfo object
                UserRegisterLogic userCheck = new UserRegisterLogic();
                UserInfo newUser = userCheck.newUserCheck(userEmail, userPassWord);

                // Write all user data to JSON file
                UpdateJson newUserJson = new UpdateJson();
                newUserJson.writeToJson(newUser, path);
                // Write info of the user who just registered to the CurrentUser JSON
                // the user is now marked as logged in.
                CurrentUserJson.WriteCurrentUserToJson(newUser);
                UserLogin.userLoggedIn = true;
                Console.WriteLine("\n- Successfully created account! -");
                Console.WriteLine($"\nWelcome, {newUser.UserName}");
                
            }
            Program.Main();
        }
    }
}
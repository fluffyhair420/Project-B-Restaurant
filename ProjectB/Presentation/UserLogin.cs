using Newtonsoft.Json;

namespace Restaurant
{
    class UserLogin
    {
        public static bool userLoggedIn { get; set; }
        // path for json file that stores all user's information
        static string userPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/User.json"));
        string userJson = File.ReadAllText(userPath);
        // path for json file that stores currently logged in user
        static string currentUserPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/CurrentUser.json"));

        public UserLogin()
        {
            
        }

        public void Login()
        {
            
            Console.WriteLine("\n=== Login ===");
            Console.WriteLine("Press Enter in an empty field to go back to the Homepage at any given time.\n");
            // Email
            Console.Write("Email: ");
            string userEmail = Console.ReadLine();
            if (userEmail == "")
            {
                Program.Main();
            }
            // Password
            Console.Write("Password: ");
            string userPassWord = Console.ReadLine();
            if (userPassWord == "")
            {
                Program.Main();
            }

            dynamic data = JsonConvert.DeserializeObject(userJson);
            bool emailInJson = false;
            if (data != null)
            {
                foreach (var item in data)
                {
                    
                    // Email in JSON file
                    if (item.Email == userEmail)
                    {
                        emailInJson = true;
                        // Password matches Email
                        if (item.PassWord == userPassWord)
                        {
                            Console.WriteLine($"\nWelcome, {item.UserName}");
                            CurrentUserJson.WriteCurrentUserToJson(item);
                            userLoggedIn = true;
                            Program.Main();
                            break;
                        }

                        // Password doesn't match Email
                        else
                        {
                            Console.WriteLine(@"
Incorrect password.");
                            //userLoggedIn = false;
                            Program.Main();
                            break;
                        }
                    } 

                }
                if (!emailInJson)
                {
                    Console.Write(@"
There's no account using this email address. Do you want to register instead?
Typ ""Y"" or ""N"": ");
                    bool wrongInput = true;
                    while (wrongInput)
                    {
                        string userInput = Console.ReadLine().ToUpper();
                        switch (userInput)
                        {
                            case "Y":
                                wrongInput = false;
                                UserRegister userRegister = new UserRegister();
                                userRegister.Register();
                                break;

                            case "N":
                                wrongInput = false;
                                Console.WriteLine(@"
May you decide to continue with an account later on, please feel
free to register.");
                                Program.Main();
                                break;

                            default:
                                Console.Write("\nInvalid input. Please typ \"Y\" or \"N\": ");
                                break;
                        }
                    }
                }
            }
            else
            {
                Console.Write(@"
There's no account using this email address. Do you want to register instead?
Typ ""Y"" or ""N"": ");
                bool wrongInput = true;
                while (wrongInput)
                {
                    string userInput = Console.ReadLine().ToUpper();
                    switch (userInput)
                    {
                        case "Y":
                            wrongInput = false;
                            UserRegister userRegister = new UserRegister();
                            userRegister.Register();
                            break;

                        case "N":
                            wrongInput = false;
                            Console.WriteLine(@"
May you decide to continue with an account later on, please feel
free to register.");
                            Program.Main();
                            break;

                        default:
                            Console.Write("\nInvalid input. Please typ \"Y\" or \"N\": ");
                            break;
                    }
                }
            }
        }
    }
}
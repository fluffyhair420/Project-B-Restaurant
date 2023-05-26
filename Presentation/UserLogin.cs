using Newtonsoft.Json;

namespace Restaurant
{
    class UserLogin : UserInfo
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
            // Email
            Console.Write("Email: ");
            string userEmail = Console.ReadLine();
            // Password
            Console.Write("Password: ");
            string userPassWord = Console.ReadLine();

            dynamic data = JsonConvert.DeserializeObject(userJson);
            bool emailInJson = false;
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
                        MainMenu.Main();
                        break;
                    }

                    // Password doesn't match Email
                    else
                    {
                        Console.WriteLine(@"
Uh oh, did you forget your password, or are you trying to
break into someone else's account?");
                        //userLoggedIn = false;
                        MainMenu.Main();
                        break;
                    }
                } 

                // Email not in JSON file
                // else if (item.Email != userEmail)
                // {
                //     continue;
                // }
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
                            MainMenu.Main();
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
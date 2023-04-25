using Newtonsoft.Json;

namespace Restaurant
{
    class UserLogin : UserInfo
    {
        static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/User.json"));
        string json = File.ReadAllText(path);

        // TO DO
        // Save which user is currently logged in
        // If JSON file is empty
        public void Login()
        {
            // Email
            Console.Write("Email: ");
            string userEmail = Console.ReadLine();
            // Password
            Console.Write("Password: ");
            string userPassWord = Console.ReadLine();

            dynamic data = JsonConvert.DeserializeObject(json);
            bool emailInJson = true;
            foreach (var item in data)
            {
                // Email in JSON file
                if (item.Email == userEmail)
                {
                    // Password matches Email
                    if (item.PassWord == userPassWord)
                    {
                        Console.WriteLine("Correct! That Password matches the email :)");
                    }

                    // Password doesn't match Email
                    else
                    {
                        Console.WriteLine(@"
Uh oh, did you forget your password, or are you trying to
break into someone else's account?");
                    }
                    break;
                }

                // Email not in JSON file
                else if (item.Email != userEmail)
                {
                    emailInJson = false;
                }
            }

            if (emailInJson == false)
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
                            //MainMenu.Main();
                            break;

                        default:
                            Console.Write("\nInvalid input. Please typ \"Y\" or \"N\": ");
                            break;
                    }

                    //                     if (userInput == "Y")
                    //                     {
                    //                         wrongInput = false;
                    //                         UserRegister userRegister = new UserRegister();
                    //                         userRegister.Register();
                    //                     }
                    //                     else if (userInput == "N")
                    //                     {
                    //                         wrongInput = false;
                    //                         Console.WriteLine(@"
                    // May you decide to continue with an account later on, please feel
                    // free to register.");
                    //                         MainMenu.Main();
                    //                     }

                    //                     else
                    //                     {
                    //                         Console.Write("\nInvalid input. Please typ \"Y\" or \"N\": ");
                    //                     }
                }
            }
        }
    }
}
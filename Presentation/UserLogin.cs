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
        string currentUserJson = File.ReadAllText(currentUserPath);

        public UserLogin()
        {
            
        }

        // public static void SetUserLoggedIn(bool value)
        // {
        //     userLoggedIn = value;
        // }

        // TO DO
        // Save which user is currently logged in
        // If JSON file is empty
        public void Login()
        {
            if (userLoggedIn == true)
            {
                Console.WriteLine("You're already logged in!");
                MainMenu.Main();
            }
            else
            {
                // Email
                Console.Write("Email: ");
                string userEmail = Console.ReadLine();
                // Password
                Console.Write("Password: ");
                string userPassWord = Console.ReadLine();

                dynamic data = JsonConvert.DeserializeObject(userJson);
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
                            Address tempCurrentUserAddress;
                            foreach (var bloop in item.Address)
                            {
                                Address currentUserAddress = new Address{City = bloop["City"],
                                                                            Street = bloop["Street"],
                                                                            HouseNumber = bloop["HouseNumber"],
                                                                            ZipCode = bloop["ZipCode"]};
                                tempCurrentUserAddress = currentUserAddress;

                                UserLogin currentUser = new UserLogin { UserID = item.UserID, UserName = item.UserName, 
                                                                    PassWord = item.PassWord, FirstName = item.FirstName,
                                                                    LastName = item.LastName, Email = item.Email,
                                                                    PhoneNumber = item.PhoneNumber, Address = new List<Address> { tempCurrentUserAddress } };

                            string currentUserJson = JsonConvert.SerializeObject(currentUser, Formatting.Indented);
                            // write current user to CurrentUser json
                            File.WriteAllText(currentUserPath, currentUserJson);
                            // read current user from CurrentUser json
                            // UserLogin currentUser1 = JsonConvert.DeserializeObject<UserLogin>(currentUserJson);
                            // Console.WriteLine(currentUser1.Address[0].City);
                            userLoggedIn = true;
                            MainMenu.Main();
                            break;
                            }
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
                                    //MainMenu.Main();
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
    }
}
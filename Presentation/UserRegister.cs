using Newtonsoft.Json;
using System;
using System.Threading.Tasks;


namespace Restaurant
{
    class UserRegister : UserInfo
    {
        static string currentUserPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/CurrentUser.json"));

        static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/User.json"));
        string json = File.ReadAllText(path);

        public void Register()
        {
            // Email
            Console.WriteLine("\n=== Register ===");
            Console.Write("Email: ");
            string userEmail = "";
            bool emailValid = false;
            while (emailValid == false)
            {
                userEmail = Console.ReadLine();
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
                Console.Write("\nEnter your email again to make sure you are using the correct email: ");
                userEmailCheck = Console.ReadLine();
                if (userEmailCheck == userEmail)
                {
                    emailVerification = true;
                }
            }
            base.Email = userEmail;


            // Password
            Console.WriteLine("\nPassword: ");
            string userPassWord = "";
            bool passwordValid = false;
            while (passwordValid == false)
            {
                Console.WriteLine(@"Password should contain at least
- 8 characters
- 1 capital letter
- 1 number
");
                userPassWord = Console.ReadLine();
                passwordValid = UserCheck.PasswordCheck(userPassWord);
            }

            // Password check of password overeenkomt met vorig ingevoerde password
            string userPassCheck = "";
            bool passVerification = false;
            while (passVerification == false)
            {
                Console.Write("\nEnter your password again to make sure you are using the correct password: ");
                userPassCheck = Console.ReadLine();
                if (userPassCheck == userPassWord)
                {
                    passVerification = true;
                }
            }
            base.PassWord = userPassWord;

            // Check if Email already exists in JSON file
            dynamic data = JsonConvert.DeserializeObject(json);
            if (data != null)
            {
                foreach (var item in data)
                {
                    // Email already exists
                    if (item.Email == Email)
                    {
                        Console.Write(@"
    An account using this email address already exists.
    Do you want to login instead?
    typ ""Y"" or ""N"": ");
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
                                    MainMenu.Main();
                                    break;

                                default:
                                    Console.Write("\nInvalid input. Please typ \"Y\" or \"N\": ");
                                    break;
                            }
                        }
                    }
                    // Email doesn't yet exist
                    else
                    {
                        Console.Write("First name: ");
                        base.FirstName = Console.ReadLine();
                        Console.Write("Last name: ");
                        base.LastName = Console.ReadLine();
                        Console.Write("Username: ");
                        base.UserName = Console.ReadLine();
                        Console.Write("Phone number: ");
                        base.PhoneNumber = Console.ReadLine();

                        Console.Write("City: ");
                        string userAddressCity = Console.ReadLine();
                        Console.Write("Street: ");
                        string userAddressStreet = Console.ReadLine();
                        Console.Write("Housenumber: ");
                        string userAddressHousenumber = Console.ReadLine();
                        Console.Write("Zipcode: ");
                        string userAddressZipcode = Console.ReadLine();


                        // create List object for address
                        List<Address> address = new List<Address>()
                        {
                            new Address
                            {
                                City = userAddressCity,
                                Street = userAddressStreet,
                                HouseNumber = userAddressHousenumber,
                                ZipCode = userAddressZipcode
                            }
                        };

                        // create object for the new user
                        UserInfo newUser = new UserInfo
                        {
                            UserName = UserName,
                            PassWord = PassWord,
                            FirstName = FirstName,
                            LastName = LastName,
                            Email = Email,
                            PhoneNumber = PhoneNumber,
                            Address = address,
                        };

                        // Write all user data to JSON file
                        UpdateJson newUserJson = new UpdateJson();
                        newUserJson.writeToJson(FirstName, LastName, UserName, PhoneNumber, PassWord, Email, address, path);
                        UpdateCurrentUser(newUser);
                        break;
                    }
                }
            }


            // empty json file
            else
            {
                Console.Write("First name: ");
                base.FirstName = Console.ReadLine();
                Console.Write("Last name: ");
                base.LastName = Console.ReadLine();
                Console.Write("Username: ");
                base.UserName = Console.ReadLine();
                Console.Write("Phone number: ");
                base.PhoneNumber = Console.ReadLine();

                Console.Write("City: ");
                string userAddressCity = Console.ReadLine();
                Console.Write("Street: ");
                string userAddressStreet = Console.ReadLine();
                Console.Write("Housenumber: ");
                string userAddressHousenumber = Console.ReadLine();
                Console.Write("Zipcode: ");
                string userAddressZipcode = Console.ReadLine();


                // create List object for address
                List<Address> address = new List<Address>()
                {
                    new Address
                    {
                        City = userAddressCity,
                        Street = userAddressStreet,
                        HouseNumber = userAddressHousenumber,
                        ZipCode = userAddressZipcode
                    }
                };

                UserInfo newUser = new UserInfo
                {
                    UserName = UserName,
                    PassWord = PassWord,
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    PhoneNumber = PhoneNumber,
                    Address = address,
                };

                // Write all user data to JSON file
                UpdateJson newUserJson = new UpdateJson();
                newUserJson.writeToJson(FirstName, LastName, UserName, PhoneNumber, PassWord, Email, address, path);
                UpdateCurrentUser(newUser);
            }
        

        UserLogin.userLoggedIn = true;
        MainMenu.Main();
        }

        static void UpdateCurrentUser(UserInfo user)
        {
            // Update the CurrentUser.json file with the user's information
            CurrentUserInfo currentUser = new CurrentUserInfo
            {
                // UserID = user.UserID,
                UserName = user.UserName,
                PassWord = user.PassWord,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address
            };

            string currentJson = JsonConvert.SerializeObject(currentUser, Formatting.Indented);

            File.WriteAllText(currentUserPath, currentJson);
        }
    }
}
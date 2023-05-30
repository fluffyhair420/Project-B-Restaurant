using Newtonsoft.Json;
using System;
using System.Threading.Tasks;


namespace Restaurant
{
    class UserRegister : UserInfo
    {
        static string currentUserPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/CurrentUser.json"));
        static string currentUserJson = File.ReadAllText(currentUserPath);
        static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/User.json"));
        static string json = File.ReadAllText(path);

        public void Register()
        {
            // Email
            Console.WriteLine("\n=== Register ===");
            string userEmail = "";
            bool emailValid = false;
            while (emailValid == false)
            {
                Console.Write("Email: ");
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
                Console.Write("Enter your email again to make sure you are using the correct email: ");
                userEmailCheck = Console.ReadLine();
                if (userEmailCheck == userEmail)
                {
                    emailVerification = true;
                }
            }
            base.Email = userEmail;


            // Password
            string userPassWord = "";
            bool passwordValid = false;
            while (passwordValid == false)
            {
                Console.WriteLine(@"
Password should contain at least
- 8 characters
- 1 capital letter
- 1 number
");
                Console.Write("Password: ");
                userPassWord = Console.ReadLine();
                passwordValid = UserCheck.PasswordCheck(userPassWord);
            }

            // Password check of password overeenkomt met vorig ingevoerde password
            string userPassCheck = "";
            bool passVerification = false;
            while (passVerification == false)
            {
                Console.Write("Enter your password again to make sure you are using the correct password: ");
                userPassCheck = Console.ReadLine();
                if (userPassCheck == userPassWord)
                {
                    passVerification = true;
                }
            }
            base.PassWord = userPassWord;

            // Check if Email already exists in JSON file
            dynamic data = JsonConvert.DeserializeObject(json);
            bool emailAlreadyExists = false;
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
                                    MainMenu.Main();
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
                string userFirst = UserCheck.GetValidInput("\nFirst name: ", UserCheck.isNameAlphabetic, "Invalid input. Please enter a first name containing only letters.");
                base.FirstName = userFirst;

                string userLast = UserCheck.GetValidInput("Last name: ", UserCheck.isNameAlphabetic, "Invalid input. Please enter a last name containing only letters.\n");
                base.LastName = userLast;

                string usernameCheck = UserCheck.GetValidInput("Username: ", UserCheck.IsUsernameValid, "");
                base.UserName = usernameCheck;

                string userPhoneNumber = UserCheck.GetValidInput("Phonenumber: ", userInput => UserCheck.IsNumeric(userInput) && userInput.Length == 10, "Invalid input. Please enter a phonenumber that is 10 numbers long.\n");
                base.PhoneNumber = userPhoneNumber;

                string userAddressCity = UserCheck.GetValidInput("City: ", UserCheck.isCityAlphabetic, "Invalid input. Please enter a city containing only letters.\n");
                string userAddressStreet = UserCheck.GetValidInput("Street: ", UserCheck.IsAlphabetic, "Invalid input. Please enter a streetname containing only letters.\n");
                string userAddressHousenumber = UserCheck.GetValidInput("Housenumber: ", UserCheck.IsNumeric, "Invalid input. Please enter a housenumber containing only numbers.\n");
                string userAddressZipcode = UserCheck.GetValidInput("Zipcode: ", UserCheck.IsZipCodeValid, "Invalid input. Zipcode format should be like 1234AB\n");

                UserInfo newUser = new UserInfo
                {
                    UserName = UserName,
                    PassWord = PassWord,
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    PhoneNumber = PhoneNumber,
                    // Create List object for address
                    Address = new List<Address>()
                    {
                        new Address
                        {
                            City = userAddressCity,
                            Street = userAddressStreet,
                            HouseNumber = userAddressHousenumber,
                            ZipCode = userAddressZipcode
                        }
                    }
                };

                // Write all user data to JSON file
                UpdateJson newUserJson = new UpdateJson();
                newUserJson.writeToJson(newUser, path);
                CurrentUserJson.WriteCurrentUserToJson(newUser);
                UserLogin.userLoggedIn = true;
                Console.WriteLine("\n- Successfully created account! -");
            }
            MainMenu.Main();
        }
    }
}
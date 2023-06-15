using Newtonsoft.Json;

namespace Restaurant
{
    public static class AdminMainMenu
    {

        public static bool adminLoggedIn { get; set; }

        static string adminPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/AdminUser.json"));
        static string adminJson = File.ReadAllText(adminPath);

        public static void Menu() // This only ever gets displayed if the Admin is logged in
        {
            
            Console.WriteLine(@"
=== Admin Homepage ===
1. My Account
2. View/Change menu
3. Book a table
4. Remove reviews
5. Logout
");
            bool wrongInput = true;
                while (wrongInput)
                {
                    string userChoice = Console.ReadLine();
                    switch (userChoice)
                    {
                        case "1":
                            Console.Clear();
                            wrongInput = false;
                            Info();
                            break;
                        case "2":
                            Console.Clear();
                            wrongInput = false;
                            Menu menu = new Menu();
                            menu.Show();
                            break;

                        case "3":
                            Console.Clear();
                            wrongInput = false;
                            AdminReserve reserve = new AdminReserve();
                            reserve.AdminMainReserve();
                            break;

                        case "4":
                            Console.Clear();
                            wrongInput = false;
                            Review.RemoveReviewAdmin();
                            break;
                        case "5":
                            adminLoggedIn = false;
                            string logoutSuccesDot = "...";
                            foreach (char c in logoutSuccesDot)
                            {
                                Console.Write(c);
                                Thread.Sleep(50);
                            }
                            Thread.Sleep(1000);
                            string logoutSuccesDot2 = " ...";
                            foreach (char c in logoutSuccesDot2)
                            {
                                Console.Write(c);
                                Thread.Sleep(50);
                            }
                            Thread.Sleep(1000);
                            string logoutSuccessfull = " Successfully logged out.\n";
                            foreach (char c in logoutSuccessfull)
                            {
                                Console.Write(c);
                                Thread.Sleep(50);
                            }
                            Console.Clear();
                            Program.Main();
                            break;
                        default:
                            Console.WriteLine("Invalid input. Please enter a number between and including 1-5.\n");
                            break;
                    }
                }
        }

        public static void Login()
        {
            Console.WriteLine("\n=== Admin Login ===");
            // Email
            Console.Write("Email: ");
            string adminEmail = Console.ReadLine();
            // Password
            Console.Write("Password: ");
            string adminPassword = Console.ReadLine();

            dynamic data = JsonConvert.DeserializeObject(adminJson);
            bool emailInJson = false;
            foreach (var item in data)
            {
                
                // Email in JSON file
                if (item.Email == adminEmail)
                {
                    emailInJson = true;
                    // Password matches Email
                    if (item.PassWord == adminPassword)
                    {
                        Console.WriteLine($"\nWelcome, {item.UserName}");
                        adminLoggedIn = true;
                        AdminMainMenu.Menu();
                        break;
                    }

                    // Password doesn't match Email
                    else
                    {
                        Console.WriteLine(@"
Invalid password.");
                        //userLoggedIn = false;
                        Program.Main();
                        break;
                    }
                } 
            }


            if (!emailInJson)
            {
                Console.Write(@"
There's no admin account using this email address.
Go back to homepage? Y/N: ");
                bool wrongInput = true;
                while (wrongInput)
                {
                    string userInput = Console.ReadLine().ToUpper();
                    switch (userInput)
                    {
                        case "Y":
                            wrongInput = false;
                            Program.Main();
                            break;

                        case "N":
                            wrongInput = false;
                            AdminMainMenu.Login();
                            break;

                        default:
                            Console.Write("\nInvalid input. Please typ \"Y\" or \"N\": ");
                            break;
                    }
                }
            }
        }

        public static void Info()
        {
            dynamic data = JsonConvert.DeserializeObject(adminJson);
            Console.WriteLine("\n\n=== Admin Information ===");
            foreach (var item in data)
            {
                Console.WriteLine($@"
Username: {item.UserName}
Password: {item.PassWord}
Email: {item.Email}
Phonenumber: {item.PhoneNumber}

Address:
City: {item.Address[0].City}
Street: {item.Address[0].Street}
Housenumber: {item.Address[0].HouseNumber}
Zipcode: {item.Address[0].ZipCode}");

            }

            Console.WriteLine("\n1. Edit info\n2. Back\n");
            int userInput = Convert.ToInt32(Console.ReadLine());
            switch (userInput)
            {
                case 1:
                    Console.Clear();
                    ChangeInfo();
                    break;
                case 2:
                    Console.Clear();
                    AdminMainMenu.Menu();
                    break;
                default:
                    Console.WriteLine("\nInvalid input. PLease typ 1 or 2.");
                    break;
            }
        }

        // Change admin account's info
        public static void ChangeInfo()
        {
            dynamic data = JsonConvert.DeserializeObject(adminJson);
            List<AdminInfo> admins = JsonConvert.DeserializeObject<List<AdminInfo>>(adminJson);

            AdminInfo adminToUpdate = null;
            foreach (var item in data)
            {
                string adminEmailToUpdate = item.Email;
                adminToUpdate = admins.Find(admin => admin.Email == adminEmailToUpdate);
            }


            if (adminToUpdate != null)
            {
                // Step 3: Update the properties of the found object
                bool wrongInput = true;
                while (wrongInput)
                {
                    Console.WriteLine("\n=== Change info ===");
                    Console.WriteLine("What do you want to update?");
                    Console.WriteLine($@"
1. Username
2. Password
3. Email
4. Phonenumber
5. City
6. Street
7. Housenumber
8. Zipcode
=== Press any other button to quit ===
");

                    string userInput = Console.ReadLine();
                    foreach (var item in data)
                    {
                        switch (userInput)
                        {
                            case "1":
                                string usernameCheck = "";
                                bool userValid = false;
                                while (userValid == false)
                                {
                                    Console.Write("Enter a new username (or press Enter to cancel): ");
                                    usernameCheck = Console.ReadLine();

                                    if (string.IsNullOrEmpty(usernameCheck))
                                    {
                                        break; // Exit the loop without updating variables
                                    }
                                    userValid = UserCheck.IsUsernameValid(usernameCheck, true);
                                }
                                if (!string.IsNullOrEmpty(usernameCheck))
                                {
                                    adminToUpdate.UserName = usernameCheck;
                                    item.UserName = adminToUpdate.UserName;
                                }
                                break;
                            case "2":
                                string adminPassword = "";
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
                                    Console.Write("Enter a new password (or press Enter to cancel): ");
                                    adminPassword = Console.ReadLine();
                                    if (string.IsNullOrEmpty(adminPassword))
                                    {
                                        break; 
                                    }
                                    
                                    passwordValid = UserCheck.PasswordCheck(adminPassword);
                                    
                                }
                                if (!string.IsNullOrEmpty(adminPassword))
                                {
                                    adminToUpdate.PassWord = adminPassword;
                                    item.PassWord = adminToUpdate.PassWord;
                                }
                                break;
                            case "3":
                                Console.Write("Enter a new email (or press Enter to cancel): ");
                                bool emailValid = false;
                                string adminEmail = "";
                                while (emailValid == false)
                                {
                                    adminEmail = Console.ReadLine();
                                    if (string.IsNullOrEmpty(adminEmail))
                                    {
                                        break; 
                                    }
                                    emailValid = UserCheck.EmailCheck(adminEmail);
                                    if (emailValid == false)
                                    {
                                        Console.WriteLine(@"
Email address should only contain:
- Letters
- Numbers
- An @
- Ends with .com or similar
                                        ");
                                    }
                                }
                                // Only update email if it's not empty
                                if (!string.IsNullOrEmpty(adminEmail))
                                {
                                    bool emailAlreadyExists = false;
                                    if (admins != null)
                                    {
                                        foreach (var items in admins)
                                        {
                                            // Email already exists
                                            if (items.Email == adminEmail)
                                            {
                                                Console.Write(@"
An account using this email address already exists.
");
                                                emailAlreadyExists = true;
                                            }
                                        }
                                    }

                                    if (!emailAlreadyExists)
                                    {   
                                        adminToUpdate.Email = adminEmail;
                                        item.Email = adminToUpdate.Email;
                                    }
                                }
                                break;
                            case "4":
                                string adminPhoneNumber = UserCheck.GetValidInput("Enter a new phonenumber (or press Enter to cancel): ", userInput => UserCheck.IsNumeric(userInput) && userInput.Length == 10, "Invalid input. Please enter a phonenumber that is 10 numbers long.\n", true);
                                if (string.IsNullOrEmpty(adminPhoneNumber))
                                {
                                    break; // Exit the loop without updating variables
                                }
                                adminToUpdate.PhoneNumber = adminPhoneNumber;
                                item.PhoneNumber = adminToUpdate.PhoneNumber;
                                break;
                            case "5":
                                string adminAddressCity = UserCheck.GetValidInput("Enter a new city (or press Enter to cancel): ", UserCheck.IsAlphabetic, "Invalid input. Please enter a city containing only letters.\n", true);
                                if (!string.IsNullOrEmpty(adminAddressCity))
                                {
                                    adminToUpdate.Address[0].City = adminAddressCity;
                                    item.Address[0].City = adminToUpdate.Address[0].City;
                                }
                                break;
                            case "6":
                                string adminAddressStreet = UserCheck.GetValidInput("Enter a new street (or press Enter to cancel): ", UserCheck.IsAlphabetic, "Invalid input. Please enter a streetname containing only letters.\n", true);
                                if (!string.IsNullOrEmpty(adminAddressStreet))
                                {
                                    adminToUpdate.Address[0].Street = adminAddressStreet;
                                    item.Address[0].Street = adminToUpdate.Address[0].Street;
                                }
                                break;
                            case "7":
                                string adminAddressHousenumber = UserCheck.GetValidInput("Enter a new housenumber (or press Enter to cancel): ", UserCheck.IsNumericAlphabetic, "Invalid input. Please enter a housenumber containing only numbers.\n", true);
                                if (!string.IsNullOrEmpty(adminAddressHousenumber))
                                {
                                    adminToUpdate.Address[0].HouseNumber = adminAddressHousenumber;
                                    item.Address[0].HouseNumber = adminToUpdate.Address[0].HouseNumber;
                                }
                                break;
                            case "8":
                                string adminAddressZipcode = UserCheck.GetValidInput("Enter a new zipcode (or press Enter to cancel): ", UserCheck.IsZipCodeValid, "Invalid input. Zipcode format should be like 1234AB\n", true);
                                if (!string.IsNullOrEmpty(adminAddressZipcode))
                                {
                                    adminToUpdate.Address[0].ZipCode = adminAddressZipcode;
                                    item.Address[0].ZipCode = adminToUpdate.Address[0].ZipCode;
                                }
                                break;
                            default:
                                string updateSuccesDot = "...";
                                string updateSuccessfull = " Information updated.\n";
                                foreach (char c in updateSuccesDot)
                                {
                                    Console.Write(c);
                                    Thread.Sleep(50);
                                }
                                Thread.Sleep(1000);
                                foreach (char c in updateSuccessfull)
                                {
                                    Console.Write(c);
                                    Thread.Sleep(50);
                                }
                                wrongInput = false;
                                Console.Clear();
                                Info();
                                break;
                        }
                    }
                    

                    // Write the modified list back to the User.JSON file
                    string updatedJsonContents = JsonConvert.SerializeObject(admins, Formatting.Indented);
                    File.WriteAllText(adminPath, updatedJsonContents);
                    // Read the updated JSON data again
                    adminJson = File.ReadAllText(adminPath);
                }
            }
        }
    }
}
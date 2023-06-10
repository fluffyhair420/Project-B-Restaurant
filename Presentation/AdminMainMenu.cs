using Newtonsoft.Json;

namespace Restaurant
{
    public static class AdminMainMenu
    {

        public static bool adminLoggedIn { get; set; }

        static string adminPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/AdminUser.json"));
        static string adminJson = File.ReadAllText(adminPath);

        public static void Menu()
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
                            wrongInput = false;
                            Info();
                            break;
                        case "2": // VIEW / CHANGE MENU GOES HERE
                            wrongInput = false;
                            // Menu menu = new Menu();
                            // menu.Show();
                            break;

                        case "3": // ADMIN BOOK A TABLE GOES HERE
                            wrongInput = false;
                            // Reserve reserve = new Reserve();
                            // reserve.MainReserve();
                            break;

                        case "4": // ADMIN REMOVE REVIEW GOES HERE
                            wrongInput = false;
                            Review.RemoveReviewAdmin();
                            break;
                        case "5":
                            adminLoggedIn = false;
                            MainMenu.Main();
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
            string userEmail = Console.ReadLine();
            // Password
            Console.Write("Password: ");
            string userPassWord = Console.ReadLine();

            dynamic data = JsonConvert.DeserializeObject(adminJson);
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
                        //CurrentUserJson.WriteCurrentUserToJson(item);
                        adminLoggedIn = true;
                        //MainMenu.Main();
                        AdminMainMenu.Menu();
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
                            MainMenu.Main();
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
            // path for currently logged in user
            // string currentUserPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/CurrentUser.json"));
            // string currentUserJson = File.ReadAllText(currentUserPath);
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
                    ChangeInfo();
                    break;
                case 2:
                    AdminMainMenu.Menu();
                    break;
                default:
                    Console.WriteLine("\nInvalid input. PLease typ 1 or 2.");
                    break;
            }
        }

        public static void ChangeInfo()
        {
            dynamic data = JsonConvert.DeserializeObject(adminJson);
            List<AdminInfo> admins = JsonConvert.DeserializeObject<List<AdminInfo>>(adminJson);

            //var allData = JsonConvert.DeserializeObject<List<UserInfo>>(userJson);
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
                            bool userInUse = true;
                            // while (userInUse == true)
                            // {
                                Console.Write("Enter a new username: ");
                                usernameCheck = Console.ReadLine();
    //                             userInUse = UserCheck.UsernameCheck(usernameCheck);
    //                             if (userInUse == true)
    //                             {
    //                                 Console.Write(@"
    // This username is already in use. Please pick another username.
    // ");
    //                             }
                            // }
                                adminToUpdate.UserName = usernameCheck;
                                item.UserName = adminToUpdate.UserName;
                                break;
                            case "2":
                                Console.Write("Enter a new password: ");
                                adminToUpdate.PassWord = Console.ReadLine();
                                item.PassWord = adminToUpdate.PassWord;
                                break;
                            case "3":
                                Console.Write("Enter a new email: ");
                                bool emailValid = false;
                                string userEmail = "";
                                while (emailValid == false)
                                {
                                    userEmail = Console.ReadLine();
                                    emailValid = UserCheck.EmailCheck(userEmail);
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
                                //checkEmail(allData, userEmail);
                                bool emailAlreadyExists = false;
                                if (admins != null)
                                {
                                    foreach (var items in admins)
                                    {
                                        // Email already exists
                                        if (items.Email == userEmail)
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
                                    adminToUpdate.Email = userEmail;
                                    item.Email = adminToUpdate.Email;
                                }
                                break;
                            case "4":
                                Console.Write("Enter a new phonenumber: ");
                                adminToUpdate.PhoneNumber = Console.ReadLine();
                                item.PhoneNumber = adminToUpdate.PhoneNumber;
                                break;
                            case "5":
                                Console.Write("Enter a new city: ");
                                adminToUpdate.Address[0].City = Console.ReadLine();
                                item.Address[0].City = adminToUpdate.Address[0].City;
                                break;
                            case "6":
                                Console.Write("Enter a new street: ");
                                adminToUpdate.Address[0].Street = Console.ReadLine();
                                item.Address[0].Street = adminToUpdate.Address[0].Street;
                                break;
                            case "7":
                                Console.Write("Enter a new housenumber: ");
                                adminToUpdate.Address[0].HouseNumber = Console.ReadLine();
                                item.Address[0].HouseNumber = adminToUpdate.Address[0].HouseNumber;
                                break;
                            case "8":
                                Console.Write("Enter a new zipcode: ");
                                adminToUpdate.Address[0].ZipCode = Console.ReadLine();
                                item.Address[0].ZipCode = adminToUpdate.Address[0].ZipCode;
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
                                Info();
                                break;
                        }
                    }
                    

                    // Step 4: Write the modified list back to the User.JSON file
                    string updatedJsonContents = JsonConvert.SerializeObject(admins, Formatting.Indented);
                    File.WriteAllText(adminPath, updatedJsonContents);
                    // Read the updated JSON data again
                    adminJson = File.ReadAllText(adminPath);
                }
            }
        }
    }
}
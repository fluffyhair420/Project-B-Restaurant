using Newtonsoft.Json;
namespace Restaurant
{
    class CurrentUser : UserInfo
    {
        // path for json file that stores all user's information
        static string userPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/User.json"));
        static string userJson = File.ReadAllText(userPath);

        public CurrentUser()
        {
            
        }

        public void Info()
        {
            // path for currently logged in user
            string currentUserPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/CurrentUser.json"));
            string currentUserJson = File.ReadAllText(currentUserPath);
            dynamic data = JsonConvert.DeserializeObject(currentUserJson);
            Console.WriteLine("\n=== My Information ===");
            Console.WriteLine($@"
Username: {data.UserName}
Password: {data.PassWord}
First name: {data.FirstName}
Last name: {data.LastName}
Email: {data.Email}
Phonenumber: {data.PhoneNumber}

Address:
City: {data.Address[0].City}
Street: {data.Address[0].Street}
Housenumber: {data.Address[0].HouseNumber}
Zipcode: {data.Address[0].ZipCode}");

            Console.WriteLine("\n1. Edit info\n2. Back\n");
            int userInput = Convert.ToInt32(Console.ReadLine());
            switch (userInput)
            {
                case 1:
                    ChangeInfo();
                    break;
                case 2:
                    MainMenu.Main();
                    break;
                default:
                    Console.WriteLine("\nInvalid input.");
                    break;
            }
        }

        public void ChangeInfo()
        {
            string currentUserPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/CurrentUser.json"));
            string currentUserJson = File.ReadAllText(currentUserPath);
            dynamic data = JsonConvert.DeserializeObject(currentUserJson);

            var allData = JsonConvert.DeserializeObject<List<UserInfo>>(userJson);
            
            string userEmailToUpdate = data.Email;
            var userToUpdate = allData.Find(user => user.Email == userEmailToUpdate);

            if (userToUpdate != null)
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
3. First name
4. Last name
5. Email
6. Phonenumber
7. City
8. Street
9. Housenumber
10. Zipcode
=== Press any other button to quit ===
");
                    string userInput = Console.ReadLine();
                    switch (userInput)
                    {
                        case "1":
                        string usernameCheck = "";
                        bool userInUse = true;
                        while (userInUse == true)
                        {
                            Console.Write("Enter a new username: ");
                            usernameCheck = Console.ReadLine();
                            userInUse = UserCheck.UsernameCheck(usernameCheck);
                            if (userInUse == true)
                            {
                                Console.Write(@"
This username is already in use. Please pick another username.
");
                            }
                        }
                            userToUpdate.UserName = usernameCheck;
                            data.UserName = userToUpdate.UserName;
                            break;
                        case "2":
                            Console.Write("Enter a new password: ");
                            userToUpdate.PassWord = Console.ReadLine();
                            data.PassWord = userToUpdate.PassWord;
                            break;
                        case "3":
                            Console.Write("Enter a new first name: ");
                            userToUpdate.FirstName = Console.ReadLine();
                            data.FirstName = userToUpdate.FirstName;
                            break;
                        case "4":
                            Console.Write("Enter a new last name: ");
                            userToUpdate.LastName = Console.ReadLine();
                            data.LastName = userToUpdate.LastName;
                            break;
                        case "5":
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
                            if (allData != null)
                            {
                                foreach (var item in allData)
                                {
                                    // Email already exists
                                    if (item.Email == userEmail)
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
                                userToUpdate.Email = userEmail;
                                data.Email = userToUpdate.Email;
                            }
                            break;
                        case "6":
                            Console.Write("Enter a new phonenumber: ");
                            userToUpdate.PhoneNumber = Console.ReadLine();
                            data.PhoneNumber = userToUpdate.PhoneNumber;
                            break;
                        case "7":
                            Console.Write("Enter a new city: ");
                            userToUpdate.Address[0].City = Console.ReadLine();
                            data.Address[0].City = userToUpdate.Address[0].City;
                            break;
                        case "8":
                            Console.Write("Enter a new street: ");
                            userToUpdate.Address[0].Street = Console.ReadLine();
                            data.Address[0].Street = userToUpdate.Address[0].Street;
                            break;
                        case "9":
                            Console.Write("Enter a new housenumber: ");
                            userToUpdate.Address[0].HouseNumber = Console.ReadLine();
                            data.Address[0].HouseNumber = userToUpdate.Address[0].HouseNumber;
                            break;
                        case "10":
                            Console.Write("Enter a new zipcode: ");
                            userToUpdate.Address[0].ZipCode = Console.ReadLine();
                            data.Address[0].ZipCode = userToUpdate.Address[0].ZipCode;
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

                    // Step 4: Write the modified list back to the User.JSON file
                    string updatedJsonContents = JsonConvert.SerializeObject(allData, Formatting.Indented);
                    File.WriteAllText(userPath, updatedJsonContents);

                    // Step 5: Write the modified list back to the CurrentUser.JSON file
                    string updatedCurrentJsonContents = JsonConvert.SerializeObject(data, Formatting.Indented);
                    File.WriteAllText(currentUserPath, updatedCurrentJsonContents);

                    //Console.WriteLine("User updated successfully.");

                }
            }

            else
            {
                Console.WriteLine("User not found.");
            }    
        }

        public bool checkEmail(dynamic allData, string email)
        {
            bool emailAlreadyExists = false;
            if (allData != null)
            {
                foreach (var item in allData)
                {
                    // Email already exists
                    if (item.Email == email)
                    {
                        Console.Write(@"
An account using this email address already exists.
Do you want to login instead?
typ ""Y"" or ""N"": ");
                        emailAlreadyExists = true;
                    }
                }
            }
            return emailAlreadyExists;
        }
    }
}

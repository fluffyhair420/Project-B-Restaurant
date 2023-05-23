using Newtonsoft.Json;
namespace Restaurant
{
    class CurrentUser : UserInfo
    {
        // path for json file that stores all user's information
        static string userPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/User.json"));
        static string userJson = File.ReadAllText(userPath);

        // path for currently logged in user
        static string currentUserPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/CurrentUser.json"));
        static string currentUserJson = File.ReadAllText(currentUserPath);
        dynamic data = JsonConvert.DeserializeObject(currentUserJson);


        //
        // BUG:
        // When registering, the user is automatically logged in > works correctly
        // UserID is always set to 0 in the currentUser.json.
        // Does not happen when the user logs in themselves.
        //
        // FIXED (for now??)
        //

        public CurrentUser()
        {
            
        }

        public void Info()
        {
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
            // WORKS: updates information in User.json
            // Next step > also update in CurrentUser.json
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
8. Street:
9. Housenumber
10. Zipcode
=== Press any other button to quit ===
");
                    string userInput = Console.ReadLine();
                    switch (userInput)
                    {
                        case "1":
                            Console.Write("Enter a new username: ");
                            userToUpdate.UserName = Console.ReadLine();
                            break;
                        case "2":
                            Console.Write("Enter a new password: ");
                            userToUpdate.PassWord = Console.ReadLine();
                            break;
                        case "3":
                            Console.Write("Enter a new first name: ");
                            userToUpdate.FirstName = Console.ReadLine();
                            break;
                        case "4":
                            Console.Write("Enter a new last name: ");
                            userToUpdate.LastName = Console.ReadLine();
                            break;
                        case "5":
                            Console.Write("Enter a new email: ");
                            userToUpdate.Email = Console.ReadLine();
                            break;
                        case "6":
                            Console.Write("Enter a new phonenumber: ");
                            userToUpdate.PhoneNumber = Console.ReadLine();
                            break;
                        case "7":
                            Console.Write("Enter a new city: ");
                            userToUpdate.Address[0].City = Console.ReadLine();
                            break;
                        case "8":
                            Console.Write("Enter a new street: ");
                            userToUpdate.Address[0].Street = Console.ReadLine();
                            break;
                        case "9":
                            Console.Write("Enter a new housenumber: ");
                            userToUpdate.Address[0].HouseNumber = Console.ReadLine();
                            break;
                        case "10":
                            Console.Write("Enter a new zipcode: ");
                            userToUpdate.Address[0].ZipCode = Console.ReadLine();
                            break;
                        default:
                            Console.WriteLine("Information updated.");
                            wrongInput = false;
                            Info();
                            break;
                    }
                    // Step 4: Write the modified list back to the JSON file
                    string updatedJsonContents = JsonConvert.SerializeObject(allData, Formatting.Indented);
                    File.WriteAllText(userPath, updatedJsonContents);

                    Console.WriteLine("User updated successfully.");
                }
            }

            else
            {
                Console.WriteLine("User not found.");
            }    
        }
    }
}

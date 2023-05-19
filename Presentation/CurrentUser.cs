using Newtonsoft.Json;
namespace Restaurant
{
    class CurrentUser : UserInfo
    {
        private string City;
        private string Street;
        private string HouseNumber;
        private string ZipCode;
        // path for json file that stores all user's information
        static string userPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/User.json"));
        string userJson = File.ReadAllText(userPath);
        // path for currently logged in user
        static string currentUserPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/CurrentUser.json"));
        static string currentUserJson = File.ReadAllText(currentUserPath);
        dynamic data = JsonConvert.DeserializeObject(currentUserJson);
        public CurrentUser()
        {

        }

        public void Info()
        {
            foreach (var addressItem in data.Address)
            {
                City = addressItem["City"];
                Street = addressItem["Street"];
                HouseNumber = addressItem["HouseNumber"];
                ZipCode = addressItem["ZipCode"];
            }
            Console.WriteLine("\n=== My Information ===");
            Console.WriteLine($@"
Username: {data.UserName}
Password: {data.PassWord}
First name: {data.FirstName}
Last name: {data.LastName}
Email: {data.Email}
Phonenumber: {data.PhoneNumber}

Address:
City: {City}
Street: {Street}
Housenumber: {HouseNumber}
Zipcode: {ZipCode}");

            Console.WriteLine("\n1. Edit info\n2. Back\n");
            int userInput = Convert.ToInt32(Console.ReadLine());
            switch (userInput)
            {
                case 1:
                    PracticeJson();
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
            
            Console.WriteLine("Current value: " + data.UserName);
            Console.WriteLine("Enter a new value: ");
            string newUsername = Console.ReadLine();
            data.UserName = newUsername;
            Console.WriteLine("New value: " + data.UserName);

            string updatedJsonContents = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(currentUserPath, updatedJsonContents);
        }

        public void PracticeJson()
        {
            // WORKS: updates information in User.json
            // Next step > implement it correctly (let user decide what to update and put in their own values)
            var olddata = JsonConvert.DeserializeObject<List<UserInfo>>(userJson);
            string userEmailToUpdate = data.Email;
            var userToUpdate = olddata.Find(user => user.Email == userEmailToUpdate);

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
                }

                // Step 4: Write the modified list back to the JSON file
                string updatedJsonContents = JsonConvert.SerializeObject(olddata, Formatting.Indented);
                File.WriteAllText(userPath, updatedJsonContents);

                Console.WriteLine("User updated successfully.");
            }    
            else
            {
                Console.WriteLine("User not found.");
            }    
        }
    }
}

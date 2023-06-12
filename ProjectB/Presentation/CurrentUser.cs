using Newtonsoft.Json;
namespace Restaurant
{
    class CurrentUser //: UserInfo
    {
        
            static string userPath = "DataSources/User.json"; // Path to the json files
            static string currentUserPath = "DataSources/CurrentUser.json";
            static string tablePath = "DataSources/Table.json";
            // path for json file that contains currently logged in user
            dynamic data = ReadCurrentUser.LoadCurrentUser();

            // path for json file that contains all bookings
            dynamic bookingData = ReadTableJson.LoadTableJson();

            // path for json file that stores all user's information
            List<UserInfo> allData = ReadUser.LoadUsersList();

        public CurrentUser()
        {
            
        }

        public void Info()
        {
            // path for currently logged in user
            //dynamic data = ReadCurrentUser.LoadCurrentUser();
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

            bool newInput = false;
            while (newInput == false)
            {
                Console.WriteLine("\n1. Edit info\n2. Back");
                string userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        newInput = true;
                        ChangeInfo();
                        break;
                    case "2":
                        newInput = true;
                        Program.Main();
                        break;
                    default:
                        Console.WriteLine("\nInvalid input.");
                        break;
                }
            }
        }

        public void ChangeInfo()
        {
            
            string userEmailToUpdate = data.Email;
            var userToUpdate = allData.Find(user => user.Email == userEmailToUpdate);

            if (userToUpdate != null)
            {
                // Update the properties of the found object
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
=== Press Enter to quit ===
");
                    string userInput = Console.ReadLine();
                    switch (userInput)
                    {
                        case "1":
                            string usernameCheck = "";
                            bool userInUse = false;
                            while (userInUse == false)
                            {
                                Console.Write("Enter a new username (or press Enter to cancel): ");
                                usernameCheck = Console.ReadLine();
                                // Check if the user pressed Enter without entering any text
                                if (string.IsNullOrEmpty(usernameCheck))
                                {
                                    break; // Exit the loop without updating variables
                                }
                                
                                userInUse = UserCheck.IsUsernameValid(usernameCheck);
                                if (userInUse == false)
                                {
                                    
                                }
                                
                            }
                            if (!string.IsNullOrEmpty(usernameCheck))
                            {
                                userToUpdate.UserName = usernameCheck;
                                data.UserName = userToUpdate.UserName;
                            }
                            break;

                        case "2":
                            string userPassWord = "";
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
                                userPassWord = Console.ReadLine();
                                if (string.IsNullOrEmpty(userPassWord))
                                {
                                    break; 
                                }
                                
                                passwordValid = UserCheck.PasswordCheck(userPassWord);
                                if (passwordValid == false)
                                {
                                    
                                }
                                
                            }
                            if (!string.IsNullOrEmpty(userPassWord))
                            {
                                userToUpdate.PassWord = userPassWord;
                                data.PassWord = userToUpdate.PassWord;
                            }
                            break;

                        case "3":
                            string userFirst = UserCheck.GetValidInput("Enter a new first name (or press Enter to cancel): ", UserCheck.IsAlphabetic, "Invalid input. Please enter a first name containing only letters.", true);
                            if (!string.IsNullOrEmpty(userFirst))
                            {
                                userToUpdate.FirstName = userFirst;
                                data.FirstName = userToUpdate.FirstName;
                            }
                            break;

                        case "4":
                            string userLast = UserCheck.GetValidInput("Enter a new last name (or press Enter to cancel): ", UserCheck.IsAlphabetic, "Invalid input. Please enter a last name containing only letters.\n", true);
                            if (!string.IsNullOrEmpty(userLast))
                            {
                                userToUpdate.LastName = userLast;
                                data.LastName = userToUpdate.LastName;
                            }
                            break;

                        case "5":
                            Console.Write("Enter a new email (or press Enter to cancel): ");
                            bool emailValid = false;
                            string userEmail = "";
                            while (emailValid == false)
                            {
                                userEmail = Console.ReadLine();
                                if (string.IsNullOrEmpty(userEmail))
                                {
                                    break; 
                                }
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
                            // Only update email if it's not empty
                            if (!string.IsNullOrEmpty(userEmail))
                            {
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
                                    if (bookingData != null)
                                    {
                                        foreach (var booking in bookingData)
                                        {
                                            if (data.Email == booking.ReservationEmail)
                                            {
                                                booking.ReservationEmail = userToUpdate.Email;
                                            }
                                        }
                                    }
                                    data.Email = userToUpdate.Email;
                                }
                            }
                            break;

                        case "6":
                            string userPhoneNumber = UserCheck.GetValidInput("Enter a new phonenumber (or press Enter to cancel): ", userInput => UserCheck.IsNumeric(userInput) && userInput.Length == 10, "Invalid input. Please enter a phonenumber that is 10 numbers long.\n", true);
                            if (string.IsNullOrEmpty(userPhoneNumber))
                            {
                                break; // Exit the loop without updating variables
                            }
                            
                            userToUpdate.PhoneNumber = userPhoneNumber;
                            
                            if (bookingData != null)
                            {
                                foreach (var booking in bookingData)
                                {
                                    if (data.PhoneNumber == booking.ReservationPhoneNumber && data.Email == booking.ReservationEmail)
                                    {
                                        booking.ReservationPhoneNumber = userToUpdate.PhoneNumber;
                                    }
                                }
                            }
                            data.PhoneNumber = userToUpdate.PhoneNumber;
                            break;


                        case "7":
                            string userAddressCity = UserCheck.GetValidInput("Enter a new city (or press Enter to cancel): ", UserCheck.IsAlphabetic, "Invalid input. Please enter a city containing only letters.\n", true);
                            if (!string.IsNullOrEmpty(userAddressCity))
                            {
                                userToUpdate.Address[0].City = userAddressCity;
                                data.Address[0].City = userToUpdate.Address[0].City;
                            }
                            break;
                        case "8":
                            string userAddressStreet = UserCheck.GetValidInput("Enter a new street (or press Enter to cancel): ", UserCheck.IsAlphabetic, "Invalid input. Please enter a streetname containing only letters.\n", true);
                            if (!string.IsNullOrEmpty(userAddressStreet))
                            {
                                userToUpdate.Address[0].Street = userAddressStreet;
                                data.Address[0].Street = userToUpdate.Address[0].Street;
                            }
                            break;
                        case "9":
                            string userAddressHousenumber = UserCheck.GetValidInput("Enter a new housenumber (or press Enter to cancel): ", UserCheck.IsNumeric, "Invalid input. Please enter a housenumber containing only numbers.\n", true);
                            if (!string.IsNullOrEmpty(userAddressHousenumber))
                            {
                                userToUpdate.Address[0].HouseNumber = userAddressHousenumber;
                                data.Address[0].HouseNumber = userToUpdate.Address[0].HouseNumber;
                            }
                            break;
                        case "10":
                            string userAddressZipcode = UserCheck.GetValidInput("Enter a new zipcode (or press Enter to cancel): ", UserCheck.IsZipCodeValid, "Invalid input. Zipcode format should be like 1234AB\n", true);
                            if (!string.IsNullOrEmpty(userAddressZipcode))
                            {
                                userToUpdate.Address[0].ZipCode = userAddressZipcode;
                                data.Address[0].ZipCode = userToUpdate.Address[0].ZipCode;
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
                            Info();
                            break;
                    }

                    int index = allData.IndexOf(userToUpdate);
                    allData[index] = userToUpdate;

                    // Step 5: Write the modified list back to the User.JSON file
                    string updatedJsonContents = JsonConvert.SerializeObject(allData, Formatting.Indented);
                    File.WriteAllText(userPath, updatedJsonContents);

                    // Step 6: Write the modified list back to the CurrentUser.JSON file
                    string updatedCurrentJsonContents = JsonConvert.SerializeObject(data, Formatting.Indented);
                    File.WriteAllText(currentUserPath, updatedCurrentJsonContents);

                    string updatedBookingJsonContents = JsonConvert.SerializeObject(bookingData, Formatting.Indented);
                    File.WriteAllText(tablePath, updatedBookingJsonContents);
                    //Console.WriteLine("User updated successfully.");

                }
            }

            else
            {
                Console.WriteLine("User not found.");
            }    
        }
    }
}




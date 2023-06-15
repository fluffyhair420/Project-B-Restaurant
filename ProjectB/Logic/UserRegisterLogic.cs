namespace Restaurant
{
    public class UserRegisterLogic : UserInfo
    {
        public UserRegisterLogic()
        {

        }
        // Checks if the user's input is correct
        public UserInfo newUserCheck(string userEmail, string userPassWord)
        {
            base.Email = userEmail;
            base.PassWord = userPassWord;
            string userFirst = UserCheck.GetValidInput("\nFirst name*: ", UserCheck.isNameAlphabetic, "Invalid input. Please enter a first name containing only letters.", false);
            base.FirstName = userFirst;

            string userLast = UserCheck.GetValidInput("Last name*: ", UserCheck.isNameAlphabetic, "Invalid input. Please enter a last name containing only letters.\n", false);
            base.LastName = userLast;

            string usernameCheck = UserCheck.GetValidInput("Username*: ", UserCheck.IsUsernameValid, "", false);
            base.UserName = usernameCheck;

            string userPhoneNumber = UserCheck.GetValidInput("Phonenumber*: ", userInput => UserCheck.IsNumeric(userInput) && userInput.Length == 10, "Invalid input. Please enter a phonenumber that is 10 numbers long.\n", false);
            base.PhoneNumber = userPhoneNumber;

            string userAddressCity = UserCheck.GetValidInput("City*: ", UserCheck.isCityAlphabetic, "Invalid input. Please enter a city containing only letters.\n", false);
            string userAddressStreet = UserCheck.GetValidInput("Street*: ", UserCheck.IsAlphabetic, "Invalid input. Please enter a streetname containing only letters.\n", false);
            string userAddressHousenumber = UserCheck.GetValidInput("Housenumber*: ", UserCheck.IsNumericAlphabetic, "Invalid input. Please enter a housenumber containing only numbers.\n", false);
            string userAddressZipcode = UserCheck.GetValidInput("Zipcode*: ", UserCheck.IsZipCodeValid, "Invalid input. Zipcode format should be like 1234AB\n", false);

            // Creates the object that will be written to the User JSON
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

            return newUser;
        }
    }
}
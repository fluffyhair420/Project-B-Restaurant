namespace Restaurant
{
    class UserLogout
    {
        public UserLogout()
        {

        }
        public void Logout()
        {
            Console.WriteLine("=== Logout ===");
            Console.WriteLine("Do you want to logout? Y/N");
            string userInput = Console.ReadLine().ToUpper();
            switch (userInput)
            {
                case "Y":
                    UserLogin.userLoggedIn = false;
                    // !! Maybe move these small lines of code for the json to a new class in the DataAccess folder !!
                    string filePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/CurrentUser.json"));
                    string jsonContent = File.ReadAllText(filePath);
                    jsonContent = string.Empty;
                    File.WriteAllText(filePath, jsonContent);
                    // !!
                    Console.WriteLine("...Succesfully logged out.");
                    MainMenu.Main();
                    break;

                case "N":
                    MainMenu.Main();
                    break;

                default:
                    Console.Write("\nInvalid input. Please typ \"Y\" or \"N\": ");
                    break;
            }
        }
    }
}
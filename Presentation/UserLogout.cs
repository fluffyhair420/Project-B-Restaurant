namespace Restaurant
{
    class UserLogout
    {
        public UserLogout()
        {

        }
        public void Logout()
        {
            Console.WriteLine("\n=== Logout ===");
            Console.Write("Do you want to logout? Y/N: ");
            string userInput = Console.ReadLine().ToUpper();
            switch (userInput)
            {
                case "Y":
                    CurrentUserJson.CurrentUserLogout();

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
                    string logoutSuccessfull = " Succesfully logged out.\n";
                    foreach (char c in logoutSuccessfull)
                    {
                        Console.Write(c);
                        Thread.Sleep(50);
                    }
                    UserLogin.userLoggedIn = false;
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
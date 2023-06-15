namespace Restaurant
{
    class Program
    {

        public static void Main()
        {
            // If a user is currently logged in, display this menu
            if (UserLogin.userLoggedIn)
            {
                Console.WriteLine(@"
=== Homepage ===
1. My Account
2. What's on the menu
3. Book a table
4. Reviews
5. About/Contact
6. Quit program
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
                            bool newInput = false;
                            while (newInput == false)
                            {
                Console.WriteLine(@"
=== My Account ===
Press Q to go back

1. My Information
2. My Bookings
3. Logout
");         
                                userChoice = Console.ReadLine();
                                switch (userChoice)
                                {
                                    case "1":
                                        Console.Clear();
                                        newInput = true;
                                        CurrentUser currUser = new CurrentUser();
                                        currUser.Info();
                                        break;
                                    case "2":
                                        Console.Clear();
                                        MyBookings.MyBooking();
                                        break;
                                    case "3":
                                        Console.Clear();
                                        UserLogout logout = new UserLogout();
                                        logout.Logout();
                                        break;
                                    case "q":
                                    case "Q":
                                        Console.Clear();
                                        Main();
                                        break;
                                    default:
                                        Console.Clear();
                                        Console.WriteLine("Please typ 1, 2 or 3");
                                        break;
                                }
                            }
                            break;
                        case "2":
                            Console.Clear();
                            wrongInput = false;
                            Menu menu = new Menu();
                            menu.Show();
                            break;
                        case "3":
                            Console.Clear();
                            Reserve reserve = new Reserve();
                            reserve.MainReserve();
                            break;
                        case "4":
                            Console.Clear();
                            Review.Start();
                            break;
                        case "5":
                            Console.Clear();
                            wrongInput = false;
                            RestaurantContact.Info();
                            break;
                        case "6":
                            Console.Clear();
                            Console.Write("Are you sure you want to quit? Y/N: ");
                            bool input = false;
                            while (input == false)
                            {
                                string quit = Console.ReadLine();
                                switch (quit)
                                {
                                    case "y":
                                    case "Y":
                                        Environment.Exit(0);
                                        break;
                                    case "n":
                                    case "N":
                                        Console.Clear();
                                        Program.Main();
                                        break;
                                    default:
                                        Console.WriteLine("Please type Y or N");
                                        break;
                                }
                            }
                            break;
                        default:
                            Console.WriteLine("Invalid input. Please enter a number between and including 1-6.\n");
                            break;
                    }
                }
            }

            // If a user is not currently logged in, display this menu
            else
            {
                //Console.Clear();
                Console.WriteLine(@"
=== Homepage ===
1. Login/Register
2. What's on the menu
3. Book a table
4. Reviews
5. About/Contact
6. Admin login
7. Quit program
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
                            bool newInput = false;
                            while (newInput == false)
                            {
                                Console.WriteLine(@"
=== My Account ===
Press Q, followed by Enter to go back to the Homepage at any given time.
1. Login
2. Register
");
                                userChoice = Console.ReadLine();
                                switch (userChoice)
                                {
                                    case "1":
                                        Console.Clear();
                                        newInput = true;
                                        UserLogin userLogin = new UserLogin();
                                        userLogin.Login();
                                        break;
                                    case "2":
                                        Console.Clear();
                                        newInput = true;
                                        UserRegister userRegister = new UserRegister();
                                        userRegister.Register();
                                        break;
                                    case "Q":
                                    case "q":
                                        Console.Clear();
                                        newInput = true;
                                        Main();
                                        break;
                                    default:
                                        Console.WriteLine("Please typ 1 or 2");
                                        break;
                                }
                            }
                            break;
                        case "2":
                            Console.Clear();
                            wrongInput = false;
                            Menu menu = new Menu();
                            menu.Show();
                            break;
                        case "3":
                            Console.Clear();
                            Reserve reserve = new Reserve();
                            reserve.MainReserve();
                            break;
                        case "4":
                            Console.Clear();
                            Review.Start();
                            break;
                        case "5":
                            Console.Clear();
                            wrongInput = false;
                            RestaurantContact.Info();
                            break;
                        case "6":
                            Console.Clear();
                            wrongInput = false;
                            AdminMainMenu.Login();
                            break;
                        case "7":
                            Console.Clear();
                            Console.Write("Are you sure you want to quit? Y/N: ");
                            bool input = false;
                            while (input == false)
                            {
                                string quit = Console.ReadLine();
                                switch (quit)
                                {
                                    case "y":
                                    case "Y":
                                        Environment.Exit(0);
                                        break;
                                    case "n":
                                    case "N":
                                        Console.Clear();
                                        Program.Main();
                                        break;
                                    default:
                                        Console.WriteLine("Please type Y or N");
                                        break;
                                }
                            }
                            break;
                        default:
                            Console.WriteLine("Invalid input. Please enter a number between and including 1-7.\n");
                            break;
                    }
                }
            }
        }
    }
}
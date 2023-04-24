namespace Restaurant
{
    class MainMenu{

        public static void Main(){

            Console.WriteLine(@"
=== Welcome ===
1. My Account
2. What's on the menu
3. Book a table
4. Reviews
5. Contact
");

            bool wrongInput = true;
            while (wrongInput){
                int userChoice = Convert.ToInt32(Console.ReadLine());
                switch (userChoice)
                {
                    case 1:
                        wrongInput = false;
                        bool newInput = false;
                        while (newInput == false)
                        {
                            Console.WriteLine(@"
=== My Account ===
1. Login
2. Register
");
                            userChoice = Convert.ToInt32(Console.ReadLine());
                            switch (userChoice)
                            {
                                case 1:
                                    newInput = true;
                                    UserLogin userLogin = new UserLogin();
                                    userLogin.Login();
                                    break;
                                case 2:
                                    newInput = true;
                                    UserRegister userRegister = new UserRegister();
                                    userRegister.Register();
                                    break;
                                default:
                                    Console.WriteLine("Please typ 1 or 2");
                                    break;
                            }
                        }
                        break;
                    case 2:
                        wrongInput = false;
                        Menu menu = new Menu();
                        menu.Show();
                        break;
                    case 3:
                        Reserve reserve = new Reserve();
                        reserve.MainReserve();
                        break;
                    case 4:
                        wrongInput = false;
                        break;
                    case 5:
                        wrongInput = false;
                        break;
                    default:
                       Console.WriteLine("Invalid input. Please enter a number between and including 1-5.\n");
                       break; 
                }

                // if (userChoice == "1"){
                //     wrongInput = false;
                //     UserLogin userLogin = new UserLogin();
                //     userLogin.Login();
                // }
                // else if (userChoice == "2"){
                //     wrongInput = false;
                // }
                // else if (userChoice == "3"){
                //     wrongInput = false;
                // }
                // else if (userChoice == "4"){
                //     wrongInput = false;
                // }
                // else if (userChoice == "5"){
                //     wrongInput = false;
                // }
                // else {
                //     Console.WriteLine("Invalid input. Please enter a number between and including 1-5.\n");
                // }
            }
        }
    }
}
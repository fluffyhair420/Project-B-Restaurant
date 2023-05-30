
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Restaurant
{
    public class Menu : Screen
    {
        //private readonly Screen _option;
        public menuDis menushow = new menuDis();
        public override void Show()
        {
            ConsoleKeyInfo input;
            ConsoleKeyInfo input_;
            do
            {
                Console.Clear();
                menushow.menuDisplay(0); //this displays the main menu
                input = Console.ReadKey(true);
            }
            while (input.Key != ConsoleKey.D9 && input.Key != ConsoleKey.D1 && input.Key != ConsoleKey.Q && input.Key != ConsoleKey.D8 && input.Key != ConsoleKey.D7);

            //this is for the details of the dishes
            if (input.Key == ConsoleKey.D1)
            {
                int userInput;
                bool validInput = false;
                do
                {
                    //this if statement checks if the input is valid
                    Console.WriteLine("\ninput the number of the dish you want to see the description of");
                    if (int.TryParse(Console.ReadLine(), out userInput))
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                    }
                } 
                while (!validInput);
                //takes the given input and gives it to the dishDetails methode
                do
                {
                    Dish.dishDetails(userInput);
                    Console.WriteLine("press escape to go back to the main menu");
                    input = Console.ReadKey();  
                }
                while (input.Key != ConsoleKey.Escape);

                if (input.Key == ConsoleKey.Escape){
                    var Menu = new Menu();
                    Menu.Show();
                } 
            }

            //CHANGE SOMETHING ABOUT A DISH THIS IS HIGHLY NEW  
            if(input.Key == ConsoleKey.D7){
                Console.Clear();
                Console.WriteLine("Give the Month of what menu you want to change");
                string month = Console.ReadLine();
                if (!menuDis.months.Contains(month)){
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("this is not a valid month");
                        Console.WriteLine("Give the Month of what menu you want to change");
                        month = Console.ReadLine();
                    }while (!menuDis.months.Contains(month));
                }
               
                Console.WriteLine("Give the ID of the dish you want to change");
                string userInput1 = Console.ReadLine();
                int Id;
                while (!int.TryParse(userInput1, out Id) || Id < 1 || Id > 8)
                {
                    Console.WriteLine("Invalid input. Please enter a valid option.");
                    Console.WriteLine("Give the ID of the dish you want to change");
                    userInput1 = Console.ReadLine();
                }

                Console.WriteLine("What do you want to change?\n1. Name\n2. Price (input like: 00,00 NOT 00.00)\n3. Description\n4. Has Fish\n5. Has Shellfish\n6. Has Meat\n7. Is Vegan\n8. Is Gluten-Free");
                string userInput = Console.ReadLine();
                int what;

                while (!int.TryParse(userInput, out what) || what < 1 || what > 8)
                {
                    Console.WriteLine("Invalid input. Please enter a valid option.");
                    Console.WriteLine("What do you want to change?\n1. Name\n2. Price (input like: 00,00 NOT 00.00)\n3. Description\n4. Has Fish\n5. Has Shellfish\n6. Has Meat\n7. Is Vegan\n8. Is Gluten-Free");
                    userInput = Console.ReadLine();
                }

                Console.WriteLine("into what do you want to change it?");
                var change = Console.ReadLine();
                if (change == "true") Dish.ChangeDish(Id, month, what, true);
                if (change == "false") Dish.ChangeDish(Id, month, what, false);
                if (what == 2) Dish.ChangeDish(Id, month, what, Convert.ToDecimal(change));
                else Dish.ChangeDish(Id, month, what, change);
                
                do
                {
                    Console.WriteLine("Press escape to go back to the mainmenu");                    
                    input_ = Console.ReadKey(true);
                }
                while (input_.Key != ConsoleKey.Escape);    

                if (input_.Key == ConsoleKey.Escape)
                {
                    var Menu = new Menu();
                    Menu.Show();
                }            
            }

            //IT WORKSSSSSSSS
            if(input.Key == ConsoleKey.D8){
                Console.Clear();
                Console.WriteLine("input the month");
                string month = Console.ReadLine();
                do
                {
                    menuDis.changeMonth(month);
                    input_ = Console.ReadKey(true);
                }
                while (input_.Key != ConsoleKey.Escape);    

                if (input_.Key == ConsoleKey.Escape)
                {
                    var Menu = new Menu();
                    Menu.Show();
                }            
            }

            if (input.Key == ConsoleKey.D9)
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("\nWhat would you like to filter on?\n1. Vegan\n2. Gluten free\n3. Vegetarian\n4. sorted by price");
                    input_ = Console.ReadKey(true);
                }while (input_.Key != ConsoleKey.Escape && input_.Key != ConsoleKey.D1 && input_.Key != ConsoleKey.D2 && input_.Key != ConsoleKey.D3 && input_.Key != ConsoleKey.D4);

                //vegan menu
                if (input_.Key == ConsoleKey.D1)
                {
                    do
                    {
                        Console.Clear();
                        menushow.menuDisplay(1);
                        input = Console.ReadKey(true);
                    }while (input.Key != ConsoleKey.Escape && input.Key != ConsoleKey.D1); //input.Key != ConsoleKey.Escape &&

                    if (input.Key == ConsoleKey.D1)
                    {
                        int userInput;
                        bool validInput = false;
                        do
                        {
                            Console.WriteLine("\ninput the number of the dish you want to see the description of");
                            if (int.TryParse(Console.ReadLine(), out userInput))
                            {
                                validInput = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a valid number.");
                            }
                        } while (!validInput);

                        do
                        {
                            Dish.dishDetails(userInput);
                            Console.WriteLine("press escape to go back to the main menu");
                            input = Console.ReadKey();  
                        }while (input.Key != ConsoleKey.Escape);

                        if (input.Key == ConsoleKey.Escape){
                            var Menu = new Menu();
                            Menu.Show();
                        } 
                    }
                    if(input.Key == ConsoleKey.Escape){
                        var Menu = new Menu();
                        Menu.Show();
                    }
                }

                //gluten free menu
                if (input_.Key == ConsoleKey.D2)
                {
                    do
                    {
                        Console.Clear();
                        menushow.menuDisplay(2);
                        input = Console.ReadKey(true);
                        
                    }while (input.Key != ConsoleKey.Escape && input.Key != ConsoleKey.D1);

                    if (input.Key == ConsoleKey.D1)
                    {
                        int userInput;
                        bool validInput = false;
                        do
                        {
                            // Prompt the user to enter the number of the dish they want to see the details of
                            Console.WriteLine("\ninput the number of the dish you want to see the description of");

                            // Read the user input and validate it
                            if (int.TryParse(Console.ReadLine(), out userInput))
                            {
                                validInput = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a valid number.");
                            }
                        } while (!validInput);
                        
                        // Display the details of the selected dish
                        do
                        {
                            Dish.dishDetails(userInput);
                            Console.WriteLine("press escape to go back to the main menu");
                            input = Console.ReadKey();  
                        }while (input.Key != ConsoleKey.Escape);

                        if (input.Key == ConsoleKey.Escape){
                            var Menu = new Menu();
                            Menu.Show();
                        }    
                    }

                    if(input.Key == ConsoleKey.Escape){
                        var Menu = new Menu();
                        Menu.Show();
                    }                
                }

                //vegatarian menu
                if (input_.Key == ConsoleKey.D3)
                {
                    do
                    {
                        Console.Clear();
                        menushow.menuDisplay(3);
                        input = Console.ReadKey(true);
                    
                    }
                    while (input.Key != ConsoleKey.Escape && input.Key != ConsoleKey.D1);

                    if (input.Key == ConsoleKey.D1)
                    {
                        int userInput;
                        bool validInput = false;

                        do
                        {
                            // Prompt the user to enter the number of the dish they want to see the details of
                            Console.WriteLine("\ninput the number of the dish you want to see the description of");

                            // Read the user input and validate it
                            if (int.TryParse(Console.ReadLine(), out userInput))
                            {
                                validInput = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a valid number.");
                            }
                        } 
                        while (!validInput);
                        
                        // Display the details of the selected dish
                        do
                        {
                            Dish.dishDetails(userInput);
                            Console.WriteLine("press escape to go back to the main menu");
                            input = Console.ReadKey();  
                        }
                        while (input.Key != ConsoleKey.Escape);

                        if (input.Key == ConsoleKey.Escape){
                            var Menu = new Menu();
                            Menu.Show();
                        }  
                    }

                    if(input.Key == ConsoleKey.Escape){
                        var Menu = new Menu();
                        Menu.Show();
                    }
                }

                //sorted by price
                if (input_.Key == ConsoleKey.D4){
                    do
                    {
                        Console.Clear();
                        menushow.menuDisplay(4);
                        input = Console.ReadKey(true);
                    }
                    while (input.Key != ConsoleKey.Escape && input.Key != ConsoleKey.D1);

                    if (input.Key == ConsoleKey.D1)
                    {
                        int userInput;
                        bool validInput = false;

                        do
                        {
                            // Prompt the user to enter the number of the dish they want to see the details of
                            Console.WriteLine("\ninput the number of the dish you want to see the description of");

                            // Read the user input and validate it
                            if (int.TryParse(Console.ReadLine(), out userInput))
                            {
                                validInput = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a valid number.");
                            }
                        } 
                        while (!validInput);
                        
                        // Display the details of the selected dish
                        do
                        {
                            Dish.dishDetails(userInput);
                            Console.WriteLine("press escape to go back to the main menu");
                            input = Console.ReadKey();  
                        }
                        while (input.Key != ConsoleKey.Escape);

                        if (input.Key == ConsoleKey.Escape){
                            var Menu = new Menu();
                            Menu.Show();
                        }
                          
                    }
                    if(input.Key == ConsoleKey.Escape){
                        var Menu = new Menu();
                        Menu.Show();
                    }
                }

                //allows the code to go back to the screen before this one.
                if (input_.Key == ConsoleKey.Escape)
                {
                    var Menu = new Menu();
                    Menu.Show();
                }
            }
            //exit the program completly
            if (input.Key == ConsoleKey.Q){
                Console.Clear();
                MainMenu.Main();
            }
        }
    }
}
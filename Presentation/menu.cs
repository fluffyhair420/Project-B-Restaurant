
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Restaurant
{

    public class Menu : Screen
    {
        private readonly Screen _option;

        public string Month = "January";
        public menuDis menushow = new menuDis();
        public Menu()
        {
            // _option = new Menu();
            //_option.SetPrevious(this);
        }

        public override void Show()
        {
            Log("Menu");
            ConsoleKeyInfo input;
            ConsoleKeyInfo input_;
            do
            {
                Console.Clear();
                menushow.menuDisplay(0); //this displays the main menu
                input = Console.ReadKey(true);
            }
            while (input.Key != ConsoleKey.Escape && input.Key != ConsoleKey.D9 && input.Key != ConsoleKey.D1 && input.Key != ConsoleKey.Q && input.Key != ConsoleKey.D8);

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
                dishDetails(userInput);
            }

            //IT WORKSSSSSSSS
            if (input.Key == ConsoleKey.D8)
            {
                Console.Clear();
                Console.WriteLine("input the month");
                string month = Console.ReadLine();
                do
                {
                    menuDis.changeMonth(month);
                    input_ = Console.ReadKey();
                }
                while (input_.Key != ConsoleKey.Escape);]
                if (input_.Key == ConsoleKey.Escape)
                {
                    var Menu = new Menu();
                    Menu.Show();
                }
            }

            if (input.Key == ConsoleKey.D9)
            {
                Console.Clear();
                do
                {
                    Console.WriteLine("\nWhat would you like to filter on?\n1. Vegan\n2. Gluten free\n3. Vegetarian");
                    input_ = Console.ReadKey(true);
                }
                while (input_.Key != ConsoleKey.Escape && input_.Key != ConsoleKey.D1 && input_.Key != ConsoleKey.D2 && input_.Key != ConsoleKey.D3);


                //vegan menu
                if (input_.Key == ConsoleKey.D1)
                {
                    do
                    {
                        Console.Clear();
                        menushow.menuDisplay(1);
                        input = Console.ReadKey(true);
                    }
                    while (input.Key != ConsoleKey.D1); //input.Key != ConsoleKey.Escape &&
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
                        }
                        while (!validInput);
                        dishDetails(userInput);
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
                    }
                    while (input.Key != ConsoleKey.D1);

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
                        dishDetails(userInput);
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
                    while (input.Key != ConsoleKey.D1);

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
                        dishDetails(userInput);
                    }
                }

                //allows the code to go back to the screen before this one.
                if (input_.Key == ConsoleKey.Escape)
                {
                    base.Back();
                }
            }
            //exit the program completly
            if (input.Key == ConsoleKey.Q)
            {
                Console.Clear();
                MainMenu.Main();
            }

            //allows the code to go back to the screen before this one.
            if (input.Key == ConsoleKey.Escape)
            {
                base.Back();
            }
        }

        //after using DishByID to get the dish, this methode prints out the details of the dish.
        public void dishDetails(int Id)
        {
            ConsoleKeyInfo input;
            do
            {
                Dish dish = dishByID(Id);
                if (dish == null)
                {
                    do
                    {
                        Console.WriteLine($"No dish found with Id: {Id}");
                        int ID = Convert.ToInt32(Console.ReadLine());
                        dish = dishByID(ID);
                    }
                    while (dish == null);
                }
                if (dish != null)
                {
                    Console.WriteLine($"{dish.Name}");
                    Console.WriteLine($"\n{dish.Description}");
                    if (dish.isVegan)
                    {
                        Console.WriteLine($"this dish is Vegan");
                    }
                    if (dish.isGlutenFree)
                    {
                        Console.WriteLine($"This dish is gluten free");
                    }
                    if (dish.hasFish)
                    {
                        Console.WriteLine("This dish has fish in it");
                    }
                    if (dish.hasMeat)
                    {
                        Console.WriteLine("this dish has meat in it");
                    }
                }
                Console.WriteLine("press escape to go back to the main menu");
                input = Console.ReadKey();
            }
            while (input.Key != ConsoleKey.Escape);
            if (input.Key == ConsoleKey.Escape)
            {
                var Menu = new Menu();
                Menu.Show();
            }
        }

        // with the user input (id) this methode can use that to get the whole dish
        public Dish dishByID(int Id)
        {
            List<Dish> dishes = Dish.LoadDishesFromJson(menuDis.Month);
            foreach (Dish dish in dishes)
            {
                if (dish.ID == Id)
                {
                    return dish;
                }
            }
            return null;
        }
    }
}

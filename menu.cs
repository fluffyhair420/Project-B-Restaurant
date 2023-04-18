
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ScreenManagement
{

    public class Menu : Screen
    {
        //private readonly Screen _option;
        public string Month = "January"; //this is the Month the menu displays, I will later implement a way for the admin to edit this.

        public Menu()
        {
            // _option = new Screen_2_Options();

            //_option.SetPrevious(this);
        }

        public override void Show()
        {
            Log("Menu");
            ConsoleKeyInfo input;
            ConsoleKeyInfo input_;
            do
            {
                // make a list of the json info of this month, print that stuff in a for loop with name, and price.
                Console.Clear();
                //use the json function to get a list of this months dishes
                List<Dish> dishes = Dish.LoadDishesFromJson(Month); 
                //using string menu we add the id, name and price of every item in the list dishes to it so we can display all this information.
                string menu = "";
                foreach (Dish dish in dishes)
                {
                    menu += $"{dish.ID,-3} {dish.Name,-30} ${dish.Price,4}\n";
                }
                Display("Our Current Menu:");
                Display("========{food}========");
                Display(menu);
                Display("For information on dishes/drinks press 1 and then input the number assosiated with it.");
                Display("To filter (for vegan/glutenfree/vegetarian options) press 9");
                Display("Press Q to exit the program");


                input = Console.ReadKey();
            }
            while (input.Key != ConsoleKey.Escape && input.Key != ConsoleKey.D9 && input.Key != ConsoleKey.D1 && input.Key != ConsoleKey.Q);


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

            if (input.Key == ConsoleKey.D9)
            {
                Console.Clear();
                do
                {
                    Console.WriteLine("\nWhat would you like to filter on?");
                    Console.WriteLine("1. Vegan");
                    Console.WriteLine("2. Gluten free");
                    Console.WriteLine("3. Vegetarian");
                    input_ = Console.ReadKey();

                }
                while (input_.Key != ConsoleKey.Escape && input_.Key != ConsoleKey.D1 && input_.Key != ConsoleKey.D2 && input_.Key != ConsoleKey.D3);


                //vegan menu
                if (input_.Key == ConsoleKey.D1)
                {
                    do
                    {
                        Console.Clear();
                        List<Dish> dishes = Dish.LoadDishesFromJson(Month); 
                        string menu = "";
                        foreach (Dish dish in dishes){
                            if (dish.isVegan){
                                menu += $"{dish.ID,-3} {dish.Name,-30} ${dish.Price,4}\n";
                            }
                        }
                        Display("\nOur vegan Menu:");
                        Display("========{food}========");
                        Display(menu);
                        Display("For information on dishes/drinks press 1 and then input the number assosiated with it.");
                        input = Console.ReadKey();
                    
                    }
                    while (input.Key != ConsoleKey.Escape && input.Key != ConsoleKey.D1);

                    if (input.Key == ConsoleKey.D1)
                    {
                        Console.WriteLine("\ninput the number if the dish you want to see the description of");
                        int userInput = Convert.ToInt32(Console.ReadLine());
                        dishDetails(userInput);    
                    }

                    if (input_.Key == ConsoleKey.Escape)
                    {
                        base.Back();
                    }
                }

                //gluten free menu
                if (input_.Key == ConsoleKey.D2)
                {
                    do
                    {
                        Console.Clear();
                        List<Dish> dishes = Dish.LoadDishesFromJson(Month); 
                        string menu = "";
                        foreach (Dish dish in dishes){
                            if (dish.isGlutenFree){
                                menu += $"{dish.ID,-3} {dish.Name,-30} ${dish.Price,4}\n";
                            }
                        }
                        Display("\nOur Gluten free Menu:");
                        Display("========{food}========");
                        Display(menu);
                        Display("For information on dishes/drinks press 1 and then input the number assosiated with it.");
                        input = Console.ReadKey();
                        
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
                        dishDetails(userInput);    
                    }

                    if (input_.Key == ConsoleKey.Escape)
                    {
                        base.Back();
                    }

                    
                }

                //vegatarian menu
                if (input_.Key == ConsoleKey.D3)
                {
                    do
                    {
                        Console.Clear();
                        List<Dish> dishes = Dish.LoadDishesFromJson(Month); 
                        string menu = "";
                        foreach (Dish dish in dishes){
                            if (!dish.hasMeat && !dish.hasFish && !dish.hasShellFish){
                                menu += $"{dish.ID,-3} {dish.Name,-30} ${dish.Price,4}\n";
                            }
                        }
                        Display("\nOur vegatarian Menu:");
                        Display("========{food}========");
                        Display(menu);
                        Display("For information on dishes/drinks press 1 and then input the number assosiated with it.");
                        input = Console.ReadKey();
                    
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
                        dishDetails(userInput);    
                    }

                    if (input_.Key == ConsoleKey.Escape)
                    {
                        base.Back();
                    }
                }



                //allows the code to go back to the screen before this one.
                if (input_.Key == ConsoleKey.Escape)
                {
                    base.Back();
                }
            }
            //exit the program completly
            if (input.Key == ConsoleKey.Q){
                Environment.Exit(0);
            }

            //allows the code to go back to the screen before this one.
            if (input.Key == ConsoleKey.Escape)
            {
                base.Back();
            }



        }

        //after using DishByID to get the dish, this methode prints out the details of the dish.
        public void dishDetails(int Id){
            ConsoleKeyInfo input;
            do{
                Dish dish = dishByID(Id);
                if (dish != null) {
                    Console.WriteLine($"{dish.Name}");
                    Console.WriteLine($"\n{dish.Description}");
                    if (dish.isVegan){
                        Console.WriteLine($"this dish is Vegan");}
                    if (dish.isGlutenFree){
                        Console.WriteLine($"This dish is gluten free");
                    }
                    if (dish.hasFish){
                        Console.WriteLine("This dish has fish in it");
                    }
                    if (dish.hasMeat){
                        Console.WriteLine("this dish has meat in it");
                    }
                }
                else {
                    Console.WriteLine($"No dish found with Id: {Id}");
                }
                input = Console.ReadKey();
            }
            while (input.Key != ConsoleKey.Escape);

            if (input.Key == ConsoleKey.Escape){
                var Menu = new Menu();
                Menu.Show();
            }
        }

        // with the user input (id) this methode can use that to get the whole dish
        public Dish dishByID(int Id){
            List<Dish> dishes = Dish.LoadDishesFromJson(Month); 
            foreach (Dish dish in dishes){
            if (dish.ID == Id){
                return dish;
                }
            }
            return null;
        }
    }
}
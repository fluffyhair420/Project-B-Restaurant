
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Restaurant
{
    public class Menu : Screen
    {
        //create a reference to the menuDis class so we can use it and its methods later in this class
        public menuDis menushow = new menuDis();
        public override void Show()
        {
            ConsoleKeyInfo input;
            do
            {
                Console.Clear();
                menushow.menuDisplay(0); //this displays the main menu
                input = Console.ReadKey(true);
            }
            while (input.Key != ConsoleKey.D9 && input.Key != ConsoleKey.D1 && input.Key != ConsoleKey.D2 &&input.Key != ConsoleKey.Q && input.Key != ConsoleKey.D8 && input.Key != ConsoleKey.D7);

            if (input.Key == ConsoleKey.D1)
            {
                //checks the input and calls the dishDetails method.
                MenuChecks.dishDetailsCheck();
            }

            //inplementation of the add/remove a dish
            if (input.Key == ConsoleKey.D2 && AdminMainMenu.adminLoggedIn && !UserLogin.userLoggedIn){
                //menu with options add and remove
                do
                {
                    Console.Clear();
                    Console.WriteLine("What would u like to do?\n1. Add dish\n2. Delete dish");
                    Console.WriteLine("Press escape to go back to the menu");
                    input = Console.ReadKey(true);
                }
                while (input.Key != ConsoleKey.D1 && input.Key != ConsoleKey.D2 && input.Key != ConsoleKey.Escape);
                
                if (input.Key == ConsoleKey.D1){
                    //add dish
                    Console.Clear();
                    Console.WriteLine("To what month do you want to add another dish? (Example: January)");
                    string month = Console.ReadLine();
                    if (!menuDis.months.Contains(month)){
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("This is not a valid month");
                            Console.WriteLine("Give the Month of what menu you want to change");
                            month = Console.ReadLine();
                        }while (!menuDis.months.Contains(month));
                    }

                    int dishID =  Dish.GetDishCount(month) + 1;
                    Console.WriteLine("Enter the dish name:");
                    string dishName = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(dishName))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid dish description:");
                        dishName = Console.ReadLine();
                    }

                    Console.WriteLine("Enter the dish price (00,00 format):");
                    decimal dishPrice;
                    while (!decimal.TryParse(Console.ReadLine(), out dishPrice))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid dish price:");
                    }

                    Console.WriteLine("Enter the dish description:");
                    string dishDescription = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(dishDescription))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid dish description:");
                        dishDescription = Console.ReadLine();
                    }

                    Console.WriteLine("Does the dish have fish? (true/false):");
                    bool dishHasFish;
                    while (!bool.TryParse(Console.ReadLine(), out dishHasFish))
                    {
                        Console.WriteLine("Invalid input. Please enter 'true' or 'false':");
                    }

                    Console.WriteLine("Does the dish have shellfish? (true/false):");
                    bool dishHasShellFish;
                    while (!bool.TryParse(Console.ReadLine(), out dishHasShellFish))
                    {
                        Console.WriteLine("Invalid input. Please enter 'true' or 'false':");
                    }

                    Console.WriteLine("Does the dish have meat? (true/false):");
                    bool dishHasMeat;
                    while (!bool.TryParse(Console.ReadLine(), out dishHasMeat))
                    {
                        Console.WriteLine("Invalid input. Please enter 'true' or 'false':");
                    }

                    Console.WriteLine("Is the dish vegan? (true/false):");
                    bool dishIsVegan;
                    while (!bool.TryParse(Console.ReadLine(), out dishIsVegan))
                    {
                        Console.WriteLine("Invalid input. Please enter 'true' or 'false':");
                    }

                    Console.WriteLine("Is the dish gluten-free? (true/false):");
                    bool dishIsGlutenFree;
                    while (!bool.TryParse(Console.ReadLine(), out dishIsGlutenFree))
                    {
                        Console.WriteLine("Invalid input. Please enter 'true' or 'false':");
                    }

                    // Create the new dish object and give it too addDish with the given month.
                    Dish newDish = new Dish(dishID, dishName, dishPrice, dishDescription, dishHasFish, dishHasShellFish, dishHasMeat, dishIsVegan, dishIsGlutenFree); 
                    Dish.addDish(month, newDish);
                    do
                    {
                        Console.WriteLine("Press escape to go back to the current menu");                    
                        input = Console.ReadKey(true);
                    }
                    while (input.Key != ConsoleKey.Escape);    

                    if (input.Key == ConsoleKey.Escape)
                    {
                        var Menu = new Menu();
                        Menu.Show();
                    } 
                }

                if (input.Key == ConsoleKey.D2){
                    //delete dish
                    Console.Clear();
                    Console.WriteLine("Input the month that you want to delete a dish from (Example: January)");
                    
                    string month = Console.ReadLine();
                    if (!menuDis.months.Contains(month)){
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("This is not a valid month");
                            Console.WriteLine("Give the Month of what menu you want to delete a dish from");
                            month = Console.ReadLine();
                        }while (!menuDis.months.Contains(month));
                    }
                    if (Dish.GetDishCount(month) == 1)
                    {
                        do
                        {
                            Console.WriteLine("The menu only has one dish, add a dish first before deleting this one so the menu wont be empty");
                            Console.WriteLine("Press escape to go back to the current menu");                    
                            input = Console.ReadKey(true);
                        }
                        while (input.Key != ConsoleKey.Escape);    

                        if (input.Key == ConsoleKey.Escape)
                        {
                            var Menu = new Menu();
                            Menu.Show();
                        }                        
                    }
                    List<Dish> dishes = Dish.LoadDishesFromJson(month);
                    string dishesThisMonth = "";
                    foreach(Dish dish in dishes){
                        dishesThisMonth += $"{dish.ID}. {dish.Name}\n";
                    }
                    Console.WriteLine(dishesThisMonth);

                    int Id;
                    string userInput = Console.ReadLine();
                    while (!int.TryParse(userInput, out Id) || Id < 1 || Id > Dish.GetDishCount(month))
                    {
                    
                        Console.WriteLine("Invalid input. Please enter a valid option.");
                        Console.WriteLine("Give the ID of the dish you want to remove");
                        userInput = Console.ReadLine();
                    }
                    Dish.removeDish(month, Id);
                    do
                    {
                        Console.WriteLine("Press escape to go back to the current menu");                    
                        input= Console.ReadKey(true);
                    }
                    while (input.Key != ConsoleKey.Escape);    

                    if (input.Key == ConsoleKey.Escape)
                    {
                        var Menu = new Menu();
                        Menu.Show();
                    } 
                }

                if (input.Key == ConsoleKey.Escape){
                    var Menu = new Menu();
                    Menu.Show();
                }

            }
            else if(input.Key == ConsoleKey.D2){
                var Menu = new Menu();
                Menu.Show();
            }

            //This calls op the method ChangeDish and checks the input of the admin  
            if(input.Key == ConsoleKey.D7 && AdminMainMenu.adminLoggedIn && !UserLogin.userLoggedIn){
                Console.Clear();
                Console.WriteLine("Give the Month of what menu you want to change (Example: January)");
                string month = Console.ReadLine();
                if (!menuDis.months.Contains(month)){
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("This is not a valid month");
                        Console.WriteLine("Give the Month of what menu you want to change");
                        month = Console.ReadLine();
                    }while (!menuDis.months.Contains(month));
                }
               
                Console.WriteLine("Give the ID of the dish you want to change");
                List<Dish> dishes = Dish.LoadDishesFromJson(month);
                Console.WriteLine("the dishes in this month:");
                string dishesThisMonth = "";
                foreach(Dish dish in dishes){
                    dishesThisMonth += $"{dish.ID}. {dish.Name}\n";
                }
                Console.WriteLine(dishesThisMonth);
                string userInput1 = Console.ReadLine();
                int Id;
                while (!int.TryParse(userInput1, out Id) || Id < 1 || Id > Dish.GetDishCount(menuDis.Month))
                {
                   
                    Console.WriteLine("Invalid input. Please enter a valid option.");
                    Console.WriteLine("Give the ID of the dish you want to change");
                    userInput1 = Console.ReadLine();
                }

                Console.WriteLine("What do you want to change?\n1. Name\n2. Price (input like: 00,00 NOT 00.00)\n3. Description\n4. Has Fish(true/false)\n5. Has Shellfish(true/false)\n6. Has Meat(true/false)\n7. Is Vegan(true/false)\n8. Is Gluten-Free(true/false)");
                string userInput = Console.ReadLine();
                int dishKey;
                while (!int.TryParse(userInput, out dishKey) || dishKey < 1 || dishKey > 8)
                {
                    Console.WriteLine("Invalid input. Please enter a valid option.");
                    Console.WriteLine("What do you want to change?\n1. Name\n2. Price (input like: 00,00 NOT 00.00)\n3. Description\n4. Has Fish(true/false)\n5. Has Shellfish(true/false)\n6. Has Meat(true/false)\n7. Is Vegan(true/false)\n8. Is Gluten-Free(true/false)");
                    userInput = Console.ReadLine();
                }

                Console.WriteLine("Into what do you want to change it?");
                var change = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(change))
                {
                    Console.WriteLine("Invalid input. Please enter a valid dish description:");
                    change = Console.ReadLine();
                }
                //these if/else if/else statements check if the user input is a bool, decimal or string and then make it the right value
                if (change == "true") Dish.ChangeDish(Id, month, dishKey, true);
                else if (change == "false") Dish.ChangeDish(Id, month, dishKey, false);
                else if (dishKey == 2) Dish.ChangeDish(Id, month, dishKey, Convert.ToDecimal(change)); 
                else Dish.ChangeDish(Id, month, dishKey, change);
                
                do
                {
                    Console.WriteLine("Press escape to go back to the current menu");                    
                    input = Console.ReadKey(true);
                }
                while (input.Key != ConsoleKey.Escape);    

                if (input.Key == ConsoleKey.Escape)
                {
                    var Menu = new Menu();
                    Menu.Show();
                }            
            }
            else if(input.Key == ConsoleKey.D7){
                var Menu = new Menu();
                Menu.Show();
            }

            //allows the admin to change the month that is being displayed.
            if(input.Key == ConsoleKey.D8 && AdminMainMenu.adminLoggedIn && !UserLogin.userLoggedIn){
                Console.Clear();
                Console.WriteLine("input the month (Example: January)");
                string month = Console.ReadLine();
                do
                {
                    menuDis.changeMonth(month);
                    input = Console.ReadKey(true);
                }
                while (input.Key != ConsoleKey.Escape);    

                if (input.Key == ConsoleKey.Escape)
                {
                    var Menu = new Menu();
                    Menu.Show();
                }            
            }
            else if(input.Key == ConsoleKey.D7){
                var Menu = new Menu();
                Menu.Show();
            }

            if (input.Key == ConsoleKey.D9)
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("\nWhat would you like to filter on?\n1. Vegan\n2. Gluten free\n3. Vegetarian\n4. sorted by price");
                    input = Console.ReadKey(true);
                }while (input.Key != ConsoleKey.Escape && input.Key != ConsoleKey.D1 && input.Key != ConsoleKey.D2 && input.Key != ConsoleKey.D3 && input.Key != ConsoleKey.D4);

                //vegan menu
                if (input.Key == ConsoleKey.D1)
                {
                    do
                    {
                        Console.Clear();
                        menushow.menuDisplay(1);
                        input = Console.ReadKey(true);
                    }while (input.Key != ConsoleKey.Escape && input.Key != ConsoleKey.D1); //input.Key != ConsoleKey.Escape &&

                    if (input.Key == ConsoleKey.D1)
                    {
                        //check the Dish class for more info on this method
                        MenuChecks.dishDetailsCheck();
                    }
                    if(input.Key == ConsoleKey.Escape){
                        var Menu = new Menu();
                        Menu.Show();
                    }
                }

                //gluten free menu
                if (input.Key == ConsoleKey.D2)
                {
                    do
                    {
                        Console.Clear();
                        menushow.menuDisplay(2);
                        input = Console.ReadKey(true);
                        
                    }while (input.Key != ConsoleKey.Escape && input.Key != ConsoleKey.D1);

                    if (input.Key == ConsoleKey.D1)
                    {
                        //check the Dish class for more info on this method
                        MenuChecks.dishDetailsCheck();    
                    }

                    if(input.Key == ConsoleKey.Escape){
                        var Menu = new Menu();
                        Menu.Show();
                    }                
                }

                //vegetarian menu
                if (input.Key == ConsoleKey.D3)
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
                        //check the Dish class for more info on this method
                        MenuChecks.dishDetailsCheck(); 
                    }

                    if(input.Key == ConsoleKey.Escape){
                        var Menu = new Menu();
                        Menu.Show();
                    }
                }

                //sorted by price
                if (input.Key == ConsoleKey.D4){
                    do
                    {
                        Console.Clear();
                        menushow.menuDisplay(4);
                        input = Console.ReadKey(true);
                    }
                    while (input.Key != ConsoleKey.Escape && input.Key != ConsoleKey.D1);

                    if (input.Key == ConsoleKey.D1)
                    {
                        //check the Dish class for more info on this method
                        MenuChecks.dishDetailsCheck();     
                    }
                    if(input.Key == ConsoleKey.Escape){
                        var Menu = new Menu();
                        Menu.Show();
                    }
                }

                //allows the code to go back to the screen before this one.
                if (input.Key == ConsoleKey.Escape)
                {
                    var Menu = new Menu();
                    Menu.Show();
                }
            }
            //exit the program completly
            if (input.Key == ConsoleKey.Q){
                if (AdminMainMenu.adminLoggedIn && !UserLogin.userLoggedIn){
                    Console.Clear();
                    AdminMainMenu.Menu();
                }
                else
                {
                    Console.Clear();
                    MainMenu.Main();
                }
                
            }
        }
    }
}
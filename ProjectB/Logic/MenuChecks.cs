namespace Restaurant{
    public static class MenuChecks{
    public static void dishDetailsCheck()
        {
            ConsoleKeyInfo input;
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
                Console.WriteLine("\npress escape to go back to the main menu");
                input = Console.ReadKey(true);
            }
            while (input.Key != ConsoleKey.Escape);

            if (input.Key == ConsoleKey.Escape){
                var Menu = new Menu();
                Menu.Show();
            } 
        }
}

}
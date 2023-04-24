
namespace Restaurant
{
    public class menuDis : Screen{
        public static string Month = "January";

        public void menuDisplay(int menuNumber){
            if (menuNumber == 0)
            {
                //regular menu
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
                Display("input 8 to change the month for the menu");
                Display("Press Q to go to main menu");
            }

            if (menuNumber == 1){
                //vegan menu
                List<Dish> dishes = Dish.LoadDishesFromJson(Month); 
                //using string menu we add the id, name and price of every item in the list dishes to it so we can display all this information.
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
            }

            if (menuNumber == 2){
                //gluten free menu
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
            }

            if (menuNumber == 3){
                //vegetarion menu
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
            }
           

        }

        public static void changeMonth(string month){
            List<string> months = new() {"January", "February"};
            if (!(months.Contains(month)))
            {
                do
                {
                    Console.WriteLine("this isnt a valid input, please input a month");
                    month = Console.ReadLine();
                }
                while(!(months.Contains(month)));
            }
            else
            {
                Month = month;
            }
           
        }
    }
}
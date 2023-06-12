using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Restaurant
{
    // class dish (with Name, Price, Description, hasFish, hasMeat, isVegan, hasShellFish, is glutenFree)
    public class Dish
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set;}
        public string Description { get; set; }
        public bool hasFish { get; set; }   
        public bool hasShellFish { get; set; }
        public bool hasMeat { get; set; }
        public bool isVegan { get; set; }
        public bool isGlutenFree { get; set; }
        
        public Dish(int Id, string name, decimal price, string description, bool hasfish, bool hasshellFish, bool hasmeat, bool isvegan, bool isgluttenfree)
        {
            this.ID = Id;
            this.Name = name;
            this.Price = price;
            this.Description = description;
            this.hasFish = hasfish;
            this.hasShellFish = hasshellFish;
            this.hasMeat = hasmeat;
            this.isVegan = isvegan;
            this.isGlutenFree = isgluttenfree;
        }

        public static List<Dish> LoadDishesFromJson(string Month)
        {
            using (StreamReader r = new StreamReader(@"DataSources/Dishes.json"))
            {
                string json = r.ReadToEnd();
                //List<Dish> dishes = JsonConvert.DeserializeObject<List<Dish>>(json);

                Dictionary<string, List<Dish>> menu = JsonConvert.DeserializeObject<Dictionary<string, List<Dish>>>(json);

                // Get the dishes for the "January" month
                List<Dish> thisMonthDishes = menu[Month];
                return thisMonthDishes;
            }
        }

        //this method checks the amount of dishes in a file.
        public static int GetDishCount(string Month)
        {
            using (StreamReader r = new StreamReader(@"DataSources/Dishes.json"))
            {
                string json = r.ReadToEnd();
                Dictionary<string, List<Dish>> menu = JsonConvert.DeserializeObject<Dictionary<string, List<Dish>>>(json);

                if (menu.ContainsKey(Month))
                {
                    List<Dish> thisMonthDishes = menu[Month];
                    return thisMonthDishes.Count;
                }
                else
                {
                    return 0; // Month not found in the menu
                }
            }
        }

        public static Dish dishByID(int Id)
        {
            List<Dish> dishes = Dish.LoadDishesFromJson(menuDis.Month); 
            foreach (Dish dish in dishes){
            if (dish.ID == Id){
                return dish;
                }
            }
            return null;
        }

        //this method checks if the input of the user is correct and then passes the input to DishDetails()
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
        public static void dishDetails(int Id)
        {
            Dish dish = dishByID(Id);
            if (dish == null)
            {
                do
                {
                    Console.WriteLine($"No dish found with Id: {Id}");
                    int ID = Convert.ToInt32(Console.ReadLine());
                    dish = dishByID(ID);
                } while(dish == null);
            }
            if (dish != null) {
                Console.WriteLine($"{dish.Name}\n");
                Console.WriteLine($"{dish.Description}");
                if (dish.isVegan) Console.WriteLine($"this dish is Vegan");
                else Console.WriteLine("this dish is not vegan");

                if (dish.isGlutenFree) Console.WriteLine($"This dish is gluten free");
                else Console.WriteLine("this dish is not gluten free");
                
                if (dish.hasFish) Console.WriteLine("This dish has fish in it");
                else Console.WriteLine("this dish doesnt have fish in it");

                if (dish.hasMeat) Console.WriteLine("this dish has meat in it");
                else Console.WriteLine("this dish does not have meat in it"); 
            }
        }

        //method that can change the dishes in the json
        public static void ChangeDish<T>(int Id, string Month, int dishKey, T change)
        {
            var jsonValue = "\"" + change + "\"";
            List<string> keys = new List<string>() { "Name", "Price", "Description", "hasFish", "hasShellFish", "hasMeat", "isVegan", "isGlutenFree" };
            // Load the dishes from the JSON file
            string json = File.ReadAllText(@"DataSources/Dishes.json");
            JObject menuData = JObject.Parse(json);
            JArray dishesArray = (JArray)menuData[Month];
            
            // Find the dish by ID
            JObject dishToUpdate = dishesArray.FirstOrDefault(d => (int)d["ID"] == Id) as JObject;
            
            if (dishToUpdate != null)
            { 
                //string key checks what the user wants to change
                string key = keys[dishKey - 1];
                //save what it was before so we can print that later
                var before = (string)dishToUpdate[key];
                if (key == "hasFish" || key == "hasShellFish" || key == "hasMeat" || key == "isVegan" || key == "isGlutenFree")
                {
                    if (change is bool)
                    {
                        dishToUpdate[key] = (bool)(object)change;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid value for {key}. The value must be a boolean.");
                        return;
                    }
                }
                if (key == "Price")
                {
                    if (change is decimal)
                    {
                        dishToUpdate[key] = (decimal)(object)change;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid value for {key}. The value must be a decimal (0.0).");
                        return;
                    }
                }
                if (key == "Name" || key == "Description" && change is string)
                {
                    dishToUpdate[key] = JToken.Parse(jsonValue);
                }
                // Save the modified dishes back to the JSON file
                File.WriteAllText(@"DataSources/Dishes.json", JsonConvert.SerializeObject(menuData, Formatting.Indented));
                // Print the updated value
                Console.WriteLine($"Updated {key}: {dishToUpdate[key]}, this was {before}");
            }
            else
            {
                Console.WriteLine($"Dish with ID {Id} not found in {Month}");
            }
        }

        //method to add dish ADD THAT
        public static void addDish(string month, Dish newDish)
        {
            string filePath = @"DataSources/Dishes.json";
            string json = File.ReadAllText(filePath);

            Dictionary<string, List<Dish>> menu = JsonConvert.DeserializeObject<Dictionary<string, List<Dish>>>(json);
            
            List<Dish> thisMonthDishes = menu[month];
            foreach( Dish dish in thisMonthDishes){
                if (dish.Name == newDish.Name){
                    Console.Clear();
                    Console.WriteLine("This dish already exists in this month");
                    Console.WriteLine("Check if you input the right month");
                    return;
                }
            }
            thisMonthDishes.Add(newDish);

            string updatedJson = JsonConvert.SerializeObject(menu, Formatting.Indented);
            File.WriteAllText(@"DataSources/Dishes.json", updatedJson);
        }

        //method to delete dish 
        public static void removeDish(string month, int ID)
        {
            string json = File.ReadAllText(@"DataSources/Dishes.json");
            Dictionary<string, List<Dish>> menu = JsonConvert.DeserializeObject<Dictionary<string, List<Dish>>>(json);
            
            List<Dish> dishes = menu[month];

            Dish dishToRemove = dishes.FirstOrDefault(dish => dish.ID == ID);
            if (dishToRemove != null)
            {
                dishes.Remove(dishToRemove);
                Console.WriteLine("Dish removed successfully!");

                // Find the month in the menu dictionary
                if (menu.TryGetValue(month, out List<Dish> monthDishes))
                {
                    // Remove the dish from the month's dish list
                    monthDishes.Remove(dishToRemove);
                }
                else
                {
                    Console.WriteLine("Month not found in the menu.");
                }
            }
            else
            {
                Console.WriteLine("Dish not found in the menu.");
            }

            string updatedJson = JsonConvert.SerializeObject(menu, Formatting.Indented);
            File.WriteAllText(@"DataSources/Dishes.json", updatedJson);
        }
    }
}

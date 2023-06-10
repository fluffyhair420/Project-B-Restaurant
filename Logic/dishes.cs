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
            using (StreamReader r = new StreamReader(@"DataSources\Dishes.json"))
                {
                    string json = r.ReadToEnd();
                    //List<Dish> dishes = JsonConvert.DeserializeObject<List<Dish>>(json);

                    Dictionary<string, List<Dish>> menu = JsonConvert.DeserializeObject<Dictionary<string, List<Dish>>>(json);

                    // Get the dishes for the "January" month
                    List<Dish> thisMonthDishes = menu[Month];
                    return thisMonthDishes;
                }
        }

        public static Dish dishByID(int Id){
            List<Dish> dishes = Dish.LoadDishesFromJson(menuDis.Month); 
            foreach (Dish dish in dishes){
            if (dish.ID == Id){
                return dish;
                }
            }
            return null;
        }

        public static void dishDetails(int Id){
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
                Console.WriteLine($"{dish.Name}");
                Console.WriteLine($"\n{dish.Description}");
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

        //method that can change the dish
        public static void ChangeDish<T>(int Id, string Month, int what, T change){
            var jsonValue = "\"" + change + "\"";
            List<string> keys = new List<string>() { "Name", "Price", "Description", "hasFish", "hasShellFish", "hasMeat", "isVegan", "isGlutenFree" };
            // Load the dishes from the JSON file
            string json = File.ReadAllText(@"C:\Users\ikben\Desktop\Project_B_local_file\DataSources\Dishes.json");
            JObject menuData = JObject.Parse(json);
            JArray dishesArray = (JArray)menuData[Month];
            
            // Find the dish by ID
            JObject dishToUpdate = dishesArray.FirstOrDefault(d => (int)d["ID"] == Id) as JObject;
            
            if (dishToUpdate != null)
            { 
                //string key checks what the user wants to change
                string key = keys[what - 1];
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
                File.WriteAllText(@"C:\Users\ikben\Desktop\Project_B_local_file\DataSources\Dishes.json", JsonConvert.SerializeObject(menuData, Formatting.Indented));

                // Print the updated value
                Console.WriteLine($"Updated {key}: {dishToUpdate[key]}, this was {before}");
            }
            else
            {
                Console.WriteLine($"Dish with ID {Id} not found in {Month}");
            }
        }
    }
}

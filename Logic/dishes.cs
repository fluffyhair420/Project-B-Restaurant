using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Restaurant
{

    // class dish (with Name, Price, Description, hasFish, hasMeat, isVegan)
    // range by price needs to by possible

    public class Dish
    {
        ConsoleKeyInfo input;
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
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
            //make a string of the json file to use later
            string jsonFile = "DataSources/Dishes.json";
            using (StreamReader r = new StreamReader(jsonFile))
            {
                string json = r.ReadToEnd();
                //List<Dish> dishes = JsonConvert.DeserializeObject<List<Dish>>(json);

                Dictionary<string, List<Dish>> menu = JsonConvert.DeserializeObject<Dictionary<string, List<Dish>>>(json);

                // Get the dishes for the "January" month
                List<Dish> thisMonthDishes = menu[Month];
                return thisMonthDishes;
            }
        }


    }
}

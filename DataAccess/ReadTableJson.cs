using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Restaurant
{
    public class ReadTableJson
    {
        string jsonFile = "DataSources/Table.json"; // Path to the json file

        /*
        * Reserve.LoadJson() method loads reads the JSON file
        * Sets the JSON data in a list of Table objects
        * Returns a list of Table objects for use later
        * ===
        * Made the method public so it will be accessible later on
        */
        public List<Table> LoadJson()
        {
            List<Table> tables = new List<Table>(); // Create a new list of Table objects
            try
            {
                using (StreamReader reader = new StreamReader(jsonFile))
                {
                    string json = reader.ReadToEnd();
                    tables = JsonConvert.DeserializeObject<List<Table>>(json);
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"ERROR: File not found. Please check ReadTableJson.LoadJson() method. {e.Message}.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: Something went wrong. Please check ReadTableJson.LoadJson() method. {e.Message}.");
            }
            return tables;
        }
    }
}
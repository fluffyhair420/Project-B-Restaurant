using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Restaurant
{
    public static class ReadTableJson
    {
        static string tablePath = "DataSources/Table.json"; // Path to the json file

        /*
        * Reserve.LoadJson() method loads reads the JSON file
        * Sets the JSON data in a list of Table objects
        * Returns a list of Table objects for use later
        * ===
        * Made the method public so it will be accessible later on
        */
        public static dynamic LoadTableJson()
        {
            try
            {
                if (!File.Exists(tablePath))
                {
                    throw new FileNotFoundException("The file could not be found.", tablePath);
                }

                string tableJson = File.ReadAllText(tablePath);
                dynamic data = JsonConvert.DeserializeObject(tableJson);
                return data;
            }

            catch (FileNotFoundException e)
            {
                Console.WriteLine($"ERROR: File not found. Please check ReadTableJson.LoadJson() method. {e.Message}.");
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: Something went wrong. Please check ReadTableJson.LoadJson() method. {e.Message}.");
                return null;
            }
            
        }
    }
}
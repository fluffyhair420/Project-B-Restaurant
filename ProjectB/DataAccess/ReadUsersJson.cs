using Newtonsoft.Json;

namespace Restaurant
{
    public static class ReadUser
    {
        static string userPath = "DataSources/User.json"; // Path to the json file

        public static List<UserInfo> LoadUsersList()
        {
            List<UserInfo> users = new List<UserInfo>(); // Create a new list of Table objects
            try
            {
                if (!File.Exists(userPath))
                {
                    throw new FileNotFoundException("The file could not be found.", userPath);
                }
                string userJson = File.ReadAllText(userPath);
                users = JsonConvert.DeserializeObject<List<UserInfo>>(userJson);
                return users;
            }

            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: Something went wrong. {e.Message}.");
                return null;
            }
        }

        public static dynamic LoadUsersDynamic()
        {
            try
            {
                if (!File.Exists(userPath))
                {
                    throw new FileNotFoundException("The file could not be found.", userPath);
                }

                string userJson = File.ReadAllText(userPath);
                dynamic data = JsonConvert.DeserializeObject(userJson);
                return data;
            }

            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: Something went wrong. {e.Message}.");
                return null;
            }
        }
    }
}
using Newtonsoft.Json;

namespace Restaurant
{
    public static class ReadCurrentUser
    {
        static string currentUserPath = "DataSources/CurrentUser.json"; // Path to the json file

        public static dynamic LoadCurrentUser()
        {
            try
            {
                if (!File.Exists(currentUserPath))
                {
                    throw new FileNotFoundException("The file could not be found.", currentUserPath);
                }

                string currentUserJson = File.ReadAllText(currentUserPath);
                dynamic data = JsonConvert.DeserializeObject(currentUserJson);
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
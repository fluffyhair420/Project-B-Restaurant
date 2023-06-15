using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Restaurant
{
    public class ReadReviewFromJson
    {
        // Get filepath of json file
        static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Reviews.json"));

        public static JArray ReadJsonFromFile()
        {

            // Read contents of Reviews.json into a JArray(JArr) and returns it
            using (StreamReader file = File.OpenText(path))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JArray JArr;
                // Try to read Reviews.json, if it cant be read, create a new empty array
                try
                {
                    JArr = (JArray)JToken.ReadFrom(reader);
                }
                catch(JsonReaderException)
                {
                    JArr = new();
                }
                return JArr;
            }
        }
    }
}
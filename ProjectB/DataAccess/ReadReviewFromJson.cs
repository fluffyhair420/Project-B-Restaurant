using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ReadFromJson
{
    // Get filepath of json file
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Reviews.json"));

    public static JArray ReadJsonFromFile()
    {

        // Read contents of Reviews.json into a JArray(JArr) and returns it
        using (StreamReader file = File.OpenText(path))
        using (JsonTextReader reader = new JsonTextReader(file))
        {
            JArray JArr = (JArray)JToken.ReadFrom(reader);

            return JArr;
        }
        
    }
}
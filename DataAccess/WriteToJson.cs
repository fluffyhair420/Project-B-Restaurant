using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class WriteToJson
{
    static readonly string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Reviews.json"));
    public static void WriteJsonToFile(string userName, string reviewText, string reservationID)
    {

        // Get contents from Reviews.json (as array) using ReadFromJson class
        JArray allReviews = ReadFromJson.ReadJsonFromFile();

        // Make newReview object
        JObject newReview = new(
            new JProperty("ReservationID", reservationID),
            new JProperty("UserName", userName),
            new JProperty("Review", reviewText)
        );

        // Add newReview object to allReviews array
        allReviews.Add(newReview);

        // Open Reviews.json and write allReviews to it
        using (StreamWriter file = File.CreateText(path))
        using (JsonTextWriter writer = new JsonTextWriter(file))
        {
            allReviews.WriteTo(writer);
        }

        // FORMAT JSON

    //  "ReviewID" :
    //  [
    //     {"Username" : "Username2"},
    //     {"Review2" : "Text Review 2}"
    //  ]


        // Console.WriteLine("testA");
        // Console.WriteLine(alljson);
        // Console.WriteLine("test2");

        // JArray jsonArray = new JArray();

        // jsonArray.Add("value1");
        // jsonArray.Add("value2");
        // jsonArray.Add("value3");

        // // Convert the JArray to a JSON string
        // string jsonString = JsonConvert.SerializeObject(jsonArray);

        // Console.WriteLine(jsonString);

    }
}
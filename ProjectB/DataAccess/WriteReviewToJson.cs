using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Restaurant
{
    public class WriteToJson
    {
        static readonly string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Reviews.json"));

        /*
        Get content of Reviews.json using ReadReviewFromJson, as JArray
        Take the paramaters username, reviewtext & reservationID
        Make new JObject with these parameters
        Add new JObject to JArray
        Write JArray back to Reviews.json
        */
        public static void WriteReviewToFile(string reservationID, string userName, string reviewText)
        {

            // Get contents from Reviews.json (as array) using ReadReviewFromJson class
            JArray allReviews = ReadReviewFromJson.ReadJsonFromFile();

            // Make newReview object
            JObject newReview = new(
                new JProperty("ReservationID", reservationID),
                new JProperty("UserName", userName),
                new JProperty("ReviewText", reviewText)
            );

            // Add newReview object to allReviews array
            allReviews.Add(newReview);

            // Open Reviews.json and write allReviews to it
            using (StreamWriter file = File.CreateText(path))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                writer.Formatting = Formatting.Indented;
                allReviews.WriteTo(writer);
            }
        }

        // Writes the given JArray to Reviews.json
        public static void WriteArrayToFile(JArray allReviews)
        {
        // Open Reviews.json and write allReviews to it
            using (StreamWriter file = File.CreateText(path))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                writer.Formatting = Formatting.Indented;
                allReviews.WriteTo(writer);
            }

        }
    }
}
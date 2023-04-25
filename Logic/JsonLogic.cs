using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public static class JsonLogic
{

    static string reviewsStr;
    static string tempUserName;
    static string tempReview;
    static string tempID;
    static int tempInt = 0;

    public static string LoadReviews()
    {
        // Get contents of Reviews.json as JArray
        JArray JArr = ReadFromJson.ReadJsonFromFile();


        foreach (JObject review in JArr)
        {
            tempInt++;
            // Assign values of current review to temporary variables
            tempUserName = review.GetValue("UserName").ToString();
            tempReview = review.GetValue("Review").ToString();

            // Man I love whitespace
            reviewsStr += 
@$"
[{tempInt}]
UserName: {tempUserName}
Review:
{tempReview}

";
        }
        tempInt = 0;

        return reviewsStr;
    }

    public static int CheckID(string reservationID)
    {
        /*
        Code 0: valid ID
        Code 1: A review for this ID already exists
        Code 2: No review exists with this ID
        */

        // Get contents of Reviews.json as JArray
        JArray JArr = ReadFromJson.ReadJsonFromFile();

        // Check if given reservationID already exists in Reviews.json
        foreach (JObject review in JArr)
        {
            tempID = review.GetValue("ReservationID").ToString();

            if (tempID == reservationID) return 1;
        }

        // TODO add check if a reservation with this ID exists(if not, return code 2)

        // If ID is not found return code 0
        return 0;

    }

    public static void RemoveReview(int reviewIndex)
    {
        // Get json contents
        JArray JArr = ReadFromJson.ReadJsonFromFile();

        // Remove review at given index (Reviews start at 1 in presentation so 1 is added to index)
        JArr.RemoveAt(reviewIndex+1);

        WriteToJson.WriteArrayToFile(JArr);


    }


    
//     // public void ConvertToJson(string userName, string reviewText, string reservationID)
//     // {

//     //     ReviewModel reviewObj = new ReviewModel // Create review object
//     //     {
//     //         UserName = userName,
//     //         ReviewText = reviewText
//     //     };

//     //     WriteToJson.WriteJsonToFile(reviewObj, reservationID); // Calls WriteToJson.WriteJsonToFile with review object + ID as params
//     // }

// //     public void ConvertFromJson()
// //     {
// //         ReviewModel reviewModel = ReadFromJson.ReadJsonFromFile();
// //         Console.WriteLine("TEST");
// //         Console.WriteLine(@$"Username: {reviewModel.UserName}
// // Review:
// // {reviewModel.ReviewText}");
// //     }
}
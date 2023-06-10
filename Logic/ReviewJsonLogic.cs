using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Restaurant
{
    public static class ReviewJsonLogic
    {

        static string reviewsStr;
        static string tempUserName;
        static string tempReview;
        static string tempID;
        static int tempInt = 0;

        public static string LoadReviews()
        {
            // Get contents of Reviews.json as JArray
            JArray JArr = ReadReviewFromJson.ReadJsonFromFile();

            string reviewsStr = "";
            foreach (JObject review in JArr)
            {
                tempInt++;
                // Assign values of current review to temporary variables
                tempUserName = review.GetValue("UserName").ToString();
                tempReview = review.GetValue("ReviewText").ToString();

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
            JArray JArr = ReadReviewFromJson.ReadJsonFromFile();

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

        // TODO add error codes and return these instead of returning to removereviewadmin
        // Takes an index(int), and removes the review with this index in Reviews.json
        public static void RemoveReviewIndex(int reviewIndex)
        {
            // Get json contents
            JArray JArr = ReadReviewFromJson.ReadJsonFromFile();

            // Remove review at given index
            try
            {
                JArr.RemoveAt(reviewIndex);
            }
            catch (System.ArgumentOutOfRangeException)
            {
                Console.WriteLine("Error: There is no review for this number!");
                Review.RemoveReviewAdmin();
            }
            catch(Exception exep)
            {
                Console.WriteLine("An error occurred:");
                Console.WriteLine(exep);
                Review.RemoveReviewAdmin();
            }

            WriteToJson.WriteArrayToFile(JArr);

        }

        // Takes a ReservationID as input, gets the index of the matching review,
        // and with this calls RemoveReviewIndex to remove this review
        public static void RemoveReviewID(string reviewID)
        {
            Console.WriteLine(reviewID);
            // Get json contents
            JArray JArr = ReadReviewFromJson.ReadJsonFromFile();
            
            int ReviewIndex = -1;
            for (int i = 0; i < JArr.Count; i++)
            {
                if (JArr[i]["ReservationID"].ToString() == reviewID)
                {
                    ReviewIndex = i;
                    break;
                }
            }

            RemoveReviewIndex(ReviewIndex);
            Console.WriteLine("Review removed!");
            Review.Start();
        }


        public static string GetCurrentUsername()
        {
            string currentUserPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/CurrentUser.json"));
            string currentUserJson = File.ReadAllText(currentUserPath);
            dynamic data = JsonConvert.DeserializeObject(currentUserJson);

            return data.UserName;
        }


        /* Takes 2 parameters, a key and a value. For example, 
        if Key = UserName & Value = Lorenzo ,
        this function will return a ... 
         TODO ?

        */
        // public static string LoadReviewsFilter()
        // {

        // }
    }
}
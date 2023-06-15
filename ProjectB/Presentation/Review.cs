using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Restaurant{
    static class Review
    {
        public static void Start()
        {
            while(true){


            Console.WriteLine(@"=== Reviews ===
1:  Write a review
2:  Read reviews
3:  Remove reviews
4:  Return to main menu");

            string userInput = Console.ReadLine();

            if (userInput is "1")
            {
                WriteReview();
            }
            else if (userInput is "2")
            {
                ReadReview();
            }
            else if (userInput is "3")
            {
                RemoveReviewUser();
            }
            else if (userInput is "4")
            {

                Program.Main();
            }


            }
        }

        public static void ReadReview()
        {
            string reviews = ReviewJsonLogic.LoadReviews();
            if (reviews.Length <= 0)
            {
                Console.WriteLine("\nVariflavors has no reviews yet!");
            }
            Console.WriteLine(reviews);

        }


        public static void WriteReview()
        {
            // correctID contains the reservation ID which matches with the user's account
            // User has to correctly input this ID as extra check
            string correctID = "";
            string userName;
            dynamic userData = ReadCurrentUser.LoadCurrentUser();


            // Check if user is logged in, if yes, check if their email is also in Table.json
            // if matching email is found, continue, else print message and return to review menu
            // If user is not logged in, ask for their name and reservationID and check these

            if (!UserLogin.userLoggedIn) // User is not logged in
            {
                // Get contents of Table.json
                string tablePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Table.json"));
                string tableJson = File.ReadAllText(tablePath);
                dynamic tableData = JsonConvert.DeserializeObject(tableJson);

                // Check if Table.json is empty
                if (tableData == null)
                {
                    Console.WriteLine("ERROR: Table.json is empty");
                    Start();
                }

                // Username check
                while(true)
                {
                    Console.WriteLine("Enter your name, or 'Q' to quit");
                    userName = Console.ReadLine();

                    if (userName.ToUpper() == "Q") Start();

                    else if (userName == null || userName.Length < 2)
                    {
                        Console.WriteLine("Please input a name atleast 2 characters long");
                    } 

                    else break;
                }
                // Ask for reservationID, check if is exists in Table.json
                // and if a review has been made with this ID
                string reservationID = "";
                while(true)
                {
                    Console.WriteLine("Enter your reservation code, or 'Q' to quit");
                    reservationID = Console.ReadLine();

                    if (reservationID.ToUpper() == "Q") Start();

                    // Check if input reservation is in Table.json
                    bool reservationExists = false;
                    foreach (JObject booking in tableData)
                    {
                        if (reservationID == booking.GetValue("ReservationID").ToString())
                        {
                            reservationExists = true;
                            break;
                        }
                    }

                    if (reservationExists == false)
                    {
                        Console.WriteLine("This reservation code is invalid!");
                        Start();
                    }

                    // Check if there is already a review with the given ID
                    if (ReviewJsonLogic.CheckID(reservationID) == 1)
                    {
                        Console.WriteLine("There already exists a review for this reservation");
                        Start();
                    }


                    string reviewText;
                    // Check review text validity
                    while(true)
                    {
                        Console.WriteLine("Write your review or 'Q' to quit\n");
                        reviewText = Console.ReadLine();

                        if (reviewText.ToUpper() == "Q") Start();

                        if (reviewText == null || reviewText.Length < 5)
                        {
                            Console.WriteLine("Please write a review at least 5 characters long");
                        }

                        else break;
                    }

                    while(true){
                        Console.WriteLine(@$"Do you want to post this review?

{reviewText}

1: Post review
2: Return to menu");

                        string userInput = Console.ReadLine();

                        if (userInput == "1")
                        {
                            Console.WriteLine("Review posted!");
                            WriteToJson.WriteReviewToFile(reservationID, userName, reviewText);
                            Start();
                        }

                        else if (userInput == "2")
                        {
                            Start();
                        }
                    }
                }
            }


            else // User is logged in
            {
                userName = userData.UserName;
                // Get current user's email
                string currentMail = userData.Email;

                // Get contents of Table.json
                string tablePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Table.json"));
                string tableJson = File.ReadAllText(tablePath);
                dynamic tableData = JsonConvert.DeserializeObject(tableJson);

                // Check email of current user against emails in Table.json
                bool matchingMail = false;

                // Check if Table.json is empty
                if (tableData == null)
                {
                    Console.WriteLine("Please make a reservation before leaving a review!");
                    Start();
                }


                foreach (JObject booking in tableData)
                {
                    if (currentMail == booking.GetValue("ReservationEmail").ToString())
                    {
                        matchingMail = true;
                        correctID = booking.GetValue("ReservationID").ToString();
                    }
                }

                // If matching email is found, let user continue, else return them to review menu
                if (matchingMail == false)
                {
                    Console.WriteLine(@"
Sorry, it appears no reservations have been made by this account.
Please visit our restaurant before leaving a review!");
                    Start();
                }

                // Ask user to input their reservation ID. Match input to ID in Table.json,
                // if wrong, repeat until correct input or user quits.
                string reservationID = "";
                while(true)
                {
                    Console.WriteLine("Enter your reservation code, or 'Q' to quit");
                    reservationID = Console.ReadLine();

                    if (reservationID.ToUpper() == "Q") Start();

                    else if (reservationID == correctID) break;
                }


                // Check if there is already a review with the given ID
                if (ReviewJsonLogic.CheckID(reservationID) == 1)
                {
                    Console.WriteLine("There already exists a review for this reservation");
                    Start();
                }

                string reviewText;
                // Check review text validity
                while(true)
                {
                    Console.WriteLine("Write your review or 'Q' to quit\n");
                    reviewText = Console.ReadLine();

                    if (reviewText.ToUpper() == "Q") Start();

                    if (reviewText == null || reviewText.Length < 5)
                    {
                        Console.WriteLine("Please write a review at least 5 characters long");
                    }

                    else break;
                }

                while(true){
                    Console.WriteLine(@$"Do you want to post this review?

{reviewText}

1: Post review
2: Return to menu");

                    string userInput = Console.ReadLine();

                    if (userInput == "1")
                    {
                        Console.WriteLine("Review posted!");
                        WriteToJson.WriteReviewToFile(reservationID, userName, reviewText);
                        Start();
                    }

                    else if (userInput == "2")
                    {
                        Start();
                    }
                }
            }
        }


        public static void RemoveReviewUser()
        {
            // Check if reviews.json is empty
            string reviews = ReviewJsonLogic.LoadReviews();
            if (reviews == null || reviews.Length <= 0)
            {
                Console.WriteLine("No reviews have been found!");
                Start();
            }

            // Check if user is logged in, if yes, show reviews made by this account,
            // else return user to reviewmenu
            if (!UserLogin.userLoggedIn) 
            {
                Console.WriteLine("Please log in to remove reviews!");
                Start();
            }

            // Get currentuser data            
            dynamic userData = ReadCurrentUser.LoadCurrentUser();

            // Get reviews data
            string currentReviewPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Reviews.json"));
            string currentReviewJson = File.ReadAllText(currentReviewPath);
            
            dynamic ReviewData = JsonConvert.DeserializeObject(currentReviewJson);


            // Get current user's username
            // Also check if this user has written any reviews
            string userName = "";
            try
            {
            userName = userData.UserName;
            }
            catch(Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
            {
                Console.WriteLine("It appears you have not yet posted any reviews");
                Start();
            }

            // All of this user's reviews
            string userReviews = "";

            // ReservationID's of this user's reviews (needed to remove review)
            List<int> usersIDs = new List<int>();

            // reviewIndex is used for numbering the user's reviews when they are displayed
            // This index is not used for anything else
            // tempInt is set to the review's reservationID, which is then added to the intlist userIDs
            // (review.ReservationID is a Jobject so it can't be used for this)

            int reviewIndex = 0;
            int tempInt = 1;
            foreach (var review in ReviewData)
            {
                if (review.UserName == userName)
                {
                    tempInt = review.ReservationID;
                    usersIDs.Add(tempInt);
                    userReviews +=  @$"
[{reviewIndex}]
{review.ReviewText}
";
                    reviewIndex++;
                }
            }


            while(true)
            {
                Console.WriteLine(userReviews);
                Console.WriteLine("Enter the number of the review to delete or Q to cancel");
                
                string userInput = Console.ReadLine();
                if (userInput == "Q" || userInput == "q") Start();

                try
                {
                    int userInputInt = Convert.ToInt32(userInput);
                    // chosenReviewID = ReservationID of the review the user chose to remove
                    string chosenReviewID = usersIDs[userInputInt].ToString();
                    // Call function to remove review by id
                    ReviewJsonLogic.RemoveReviewID(chosenReviewID);


                }
                catch(Exception ex)
                when (ex is ArgumentOutOfRangeException || ex is FormatException)
                {
                    Console.WriteLine("\nPlease choose a valid number!");
                    
                }
            }
        }


        public static void RemoveReviewAdmin()
        {
            // Check if reviews.json is empty
            string reviews = ReviewJsonLogic.LoadReviews();
            if (reviews == null || reviews.Length <= 0)
            {
                Console.WriteLine("No reviews have been found!");
                AdminMainMenu.Menu();
            }
            // Check if admin is logged in (if this check is tripped there is an issue)
            if (AdminMainMenu.adminLoggedIn == false)
            {
                Console.WriteLine("Only the admin can access this menu!");
                Program.Main();
            }

            Console.WriteLine(reviews);

            //while userInput != valid review number / Q
            while(true)
            {
                // Add check if review json is empty
                Console.WriteLine("Enter the number of the review to delete, or Q to return to menu");
                string reviewIndexString = Console.ReadLine();

                if (reviewIndexString.ToUpper() == "Q")
                {
                    AdminMainMenu.Menu();
                }

                // Check if input is int, if not print error + return to reviewmenu
                try
                {
                int reviewIndex = Convert.ToInt32(reviewIndexString);

                // Call RemoveReview to remove review at given index
                // If input is correct review will be removed, otherwise error message is printed and user returned to reviewmenu
                // Since the reviews are numbered starting at 1,  1 is subtracted to match the actual index of the reviews(which starts at 0)
                ReviewJsonLogic.RemoveReviewIndex(reviewIndex-1);
                Console.WriteLine("Review removed");
                AdminMainMenu.Menu();
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Please input a number!");
                    RemoveReviewAdmin();

                }
            }
        }
    }
}

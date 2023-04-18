static class Review
{
    //static private ReviewToJson reviewToJson = new();


    public static void Start()
    {
        while(true){

        Console.WriteLine(@"
1:  Write a review
2:  Read reviews
3:  Remove reviews (admin only)
4:  Return to main menu");

        string userInput = Console.ReadLine();

        // change to switch case?
        if (userInput is "1")
        {
            WriteReview();
        }
        // else if (userInput is "2")
        // {
        //     ReadReview();
        // }
        // else if (userInput is "3")
        // {
        //     RemoveReview();
        // }
        // else if (userInput is "4")
        // {
        //     Environment.Exit(0);
        //     // Redirect to main menu
        // }

        }
    }

    public static void WriteReview(){

        // Normally a valid reservation code has to be entered before the user can write a review
        Console.WriteLine("Enter your reservation code");
        string reservationCode = Console.ReadLine();

        Console.WriteLine("Write your review\n");
        string userReview = Console.ReadLine();

        while(true){
            Console.WriteLine(@$"Do you want to post this review?

{userReview}

1: Post review
2: Return to menu");

            string userInput = Console.ReadLine();

            if (userInput == "1")
            {
                Console.WriteLine("Review posted!");
                string userName = "testname";

                //reviewToJson.Start();
            }
            else if (userInput == "2")
            {
                Start();
            }
        }
    }

    // public static void ReadReview(){
    //     Review reviewObj = new();

    //     Console.WriteLine("\n");
    //     for (int i = 0; i < reviewsList.Count; i++)
    //     {
    //         Console.WriteLine($"{i}.  (username)");// username would normally be name of who wrote the review
    //         Console.WriteLine("-------------");
    //         Console.WriteLine(reviewsList[i]);
    //         Console.WriteLine("-------------\n");
    //     }
    // }

    // public static void RemoveReview(){
    //     Console.WriteLine("\n");
    //     for (int i = 0; i < reviewsList.Count; i++)
    //     {
    //         Console.WriteLine($"{i}.  (username)");// username would normally be name of who wrote the review
    //         Console.WriteLine("-------------");
    //         Console.WriteLine(reviewsList[i]);
    //         Console.WriteLine("-------------\n");
    //     }

    //     while(true)
    //     {
    //         Console.WriteLine("Enter number of review to remove or Q to go back");
    //         string userInput = Console.ReadLine();

    //         if (userInput.ToUpper() == "Q"){
    //             Main();
    //         }

    //         try { // try/ catch is to check if user inputs a valid review number
    //             int reviewNumber = int.Parse(userInput);

    //             Console.WriteLine("Are you sure you want to remove this review?\n");
    //             Console.WriteLine(reviewsList[reviewNumber]);

    //             Console.WriteLine("\n1: Remove review");
    //             Console.WriteLine("2: Go back");

    //             string userChoice = Console.ReadLine();

    //             if (userChoice == "1")
    //             {
    //                 reviewsList.RemoveAt(reviewNumber);
    //                 Console.WriteLine("Review removed!");
    //                 RemoveReview();
    //             }
    //             if (userChoice == "2")
    //             {
    //                 RemoveReview();
    //             }
    //         }

    //         catch(System.FormatException){
    //             Console.WriteLine("Invalid input");
    //             RemoveReview();
    //         }
    //         catch(System.ArgumentOutOfRangeException){
    //             Console.WriteLine("No review with that number exists");
    //             RemoveReview();
    //         }
    //     }



    // }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Restaurant;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void WriteAndReadReview()
    {

        // Filepath json:  C:\Users\Lorenzo\Documents\Visualstudio code C#\Project-B-Copy-Test-2\Project-B-Restaurant-main\ReviewTest\bin\Debug\net6.0\DataSources
        Review.ReadReview();

        // ReservationID has to exist in Table.json
        WriteToJson.WriteReviewToFile("7356854", "TestUsername", "TestReviewText");


        string reviewsContent = ReviewJsonLogic.LoadReviews();

        string expectedString = "UserName: TestUsername\nReview:\nTestReviewText";
        bool jsonContainsReview = reviewsContent.Contains(expectedString);

        Assert.IsTrue(jsonContainsReview);

//         UserName: lor
// Review:
// reviewtexttt
        // Assert.IsTrue


//         var stringWriter = new StringWriter();
// 	    Console.SetOut(stringWriter);
//         string output = stringWriter.ToString();

//         string expectedOutput = @$"[1]
// UserName: testname1
// Review:
// reviewtesttext1

    
// [2]
// UserName: testname2
// Review:
// reviewtesttext2";

//         Assert.AreEqual(expectedOutput, output,
//                         string.Format("Expected111: {0}; Actual111: {1}",
//                                 expectedOutput, output));

        // var input = new StringReader("2");
        // Console.SetIn(input);



        // var output = stringWriter.ToString();

        // Assert.AreEqual("a", output);


        // string reviews = ReviewJsonLogic.LoadReviews();
        // JArray testArray = new JArray();

        //     JObject testReview = new(
        //         new JProperty("ReservationID", 8241543),
        //         new JProperty("UserName", "Reviewtestname"),
        //         new JProperty("ReviewText", "Reviewtesttext")
        //     );
        // testArray.Add(testReview);
        
// [
//   {
//     "ReservationID": "8081875",
//     "UserName": "lor",
//     "ReviewText": "review1"
//   }
// ]
        // Console.writeline output will be send to this stringwriter
        // var stringWriter = new StringWriter();
	    // Console.SetOut(stringWriter);
    }

    public void RemoveReviewUserTest()
    {

    }

    public void RemoveReviewAdminTest()
    {

    }

}
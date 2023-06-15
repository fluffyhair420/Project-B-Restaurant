namespace Restaurant;

[TestClass]
public class TestUser
{

    [DataTestMethod]
    [DataRow("Kim", "Kim")] // returns input because all characters are alphabetic
    [DataRow("Gert-Jan", "Gert-Jan")] // returns input because all characters are alphabetic, and '-' is also accepted
    [DataRow("Kim1", null)] // returns null because not all characters are alphabetic
    [DataRow("Kim!", null)] // returns null because not all characters are alphabetic
    [DataRow("", "")] // returns nothing because the user put in an empty string
    public void UserCheck_FirstNameIsValid_ReturnAreEqual(string userInput, string expected)
    {
        // Arrange
        string prompt = "\nFirst name: ";
        string errorMessage = "Invalid input. Please enter a first name containing only letters.";
        Func<string, bool> validationFunc = UserCheck.isNameAlphabetic;
        userInput += Environment.NewLine;
        var consoleInput = new System.IO.StringReader(userInput);
        Console.SetIn(consoleInput);

        // Act
        string actual = UserCheck.GetValidInput(prompt, validationFunc, errorMessage, true);

        // Assert
        Assert.AreEqual(expected, actual);
    }


    [DataTestMethod]
    [DataRow("abcD123", false)] // returns false because Length < 10
    [DataRow("lowernumber1", false)] // false because there's no uppercase
    [DataRow("UPPERNUMBER1", false)] // false because there's no lowercase
    [DataRow("NotValid", false)] // returns false because there's no number included
    [DataRow("onlylowerletters", false)] // false because there's no uppercase and numbers
    [DataRow("ONLYUPPER", false)] // false because there's no lowercase and numbers
    [DataRow("12345678", false)] // false because there's no lower/uppercase
    [DataRow("ValidPassword9", true)] // returns true because all requirements are met
    [DataRow("", false)] // false because input is empty
    
    public void UserCheck_UserPassWordValid_ReturnAreEqual(string userInput, bool expectedReturnValue)
    {
        bool actualReturnValue = UserCheck.PasswordCheck(userInput);
        Assert.AreEqual(expectedReturnValue, actualReturnValue,
        string.Format("Expected return value: {0}, Actual return value: {1}", expectedReturnValue, actualReturnValue));
    }

    [DataTestMethod]
    [DataRow("emailemail.com", false)] // no @                                         
    [DataRow("@email.com", false)] // no text before @                             
    [DataRow("email@.com", false)] // no text after @                             
    [DataRow("email@email", false)] // no .com (or similar)
    [DataRow("email@email.", false)] // no .com (or similar)
    [DataRow("email@emailcom", false)] // no .com (or similar)                      
    [DataRow("email@.com", false)] // no text in between @ and .com (or similar)   
    [DataRow("", false)] // no text                                      
    [DataRow("email.com", false)] // no @ and text after @      
    [DataRow("ema-il@email.com", false)] // "-" in first text
    [DataRow("email@email.123", false)] // numbers instead of text as end
    [DataRow("email@ema-il.com", false)] // "-" in domain name is not allowed
    [DataRow("email@email1.com", true)] // domain name can contain numbers
    [DataRow("email1@email.com", true)] // personal name can contain numbers
    [DataRow("email@email.com", true)] // most used email format
    
    public void UserCheck_EmailValid_ReturnAreEqual(string userInput, bool expectedReturnValue)
    {
        bool actualReturnValue = UserCheck.EmailCheck(userInput);
        Assert.AreEqual(expectedReturnValue, actualReturnValue,
        string.Format("Expected return value: {0}, Actual return value: {1}", expectedReturnValue, actualReturnValue));
    }

    [DataTestMethod]
    [DataRow("Bergschenhoek", true)]
    [DataRow("Berkel en Rodenrijs", true)]
    [DataRow("'s Hertogenbosch", true)]
    [DataRow("Alphen aan den Rijn", true)]
    [DataRow("Alphen a/d Rijn", true)]
    [DataRow("Zuid-Holland", true)]
    [DataRow("'s-Gravenhave", true)]
    [DataRow("A'Dam", true)]
    [DataRow("-Amsterdam", false)] // no - as first character
    [DataRow("'`", false)] // must contain a letter
    [DataRow("``````", false)] // must contain a letter
    [DataRow("`s Hertogenbosch-", false)] // no - as last character
    [DataRow("Ab", false)] // must be 3 characters long
    [DataRow("Urk", true)]
    [DataRow("Alphen-aan-den-rijn", true)]

    public void UserCheck_isCityAlphabetic_ReturnAreEqual(string userInput, bool expectedReturnValue)
    {
        bool actualReturnValue = UserCheck.isCityAlphabetic(userInput);
        Assert.AreEqual(expectedReturnValue, actualReturnValue,
        string.Format("Expected return value: {0}, Actual return value: {1}", expectedReturnValue, userInput));
    }

}

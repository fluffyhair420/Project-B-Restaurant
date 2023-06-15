namespace Restaurant;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void AdminCanMakeReservation()
    {
        // Arrange
        AdminReserve adminReserve = new AdminReserve();
        adminReserve.partySize = "4";
        adminReserve.reservationDate = "30/06/2023 18:30";
        adminReserve.reservationName = "John Doe";
        adminReserve.reservationEmail = "johndoe@example.com";
        adminReserve.reservationPhoneNumber = "1234567890";

        // Act
        adminReserve.ReservationForm();

        // Assert
        Assert.AreEqual("4", adminReserve.partySize); // Verify the party size is set correctly
        Assert.AreEqual("30/06/2023 18:30", adminReserve.reservationDate); // Verify the reservation date is set correctly
        Assert.AreEqual("John Doe", adminReserve.reservationName); // Verify the reservation name is set correctly
        Assert.AreEqual("johndoe@example.com", adminReserve.reservationEmail); // Verify the reservation email is set correctly
        Assert.AreEqual("1234567890", adminReserve.reservationPhoneNumber); // Verify the reservation phone number is set correctly
    }
    [TestMethod]
    public void IsDateValid_ValidDate_ReturnsTrue()
    {
        // Arrange
        string validDate = "15/06/2023 18:30";

        // Act
        bool isValid = ReserveLogic.IsDateValid(validDate);
        bool expected = true;

        // Assert
        Assert.AreEqual(isValid, expected);
    }

    [TestMethod]
    public void IsDateValid_InvalidDate_ReturnsFalse()
    {
        // Arrange
        string invalidDate = "30/02/2023 10:00"; // Assuming February 30th is an invalid date

        // Act
        bool isValid = ReserveLogic.IsDateValid(invalidDate);
        bool expected = false;

        // Assert
        Assert.AreEqual(isValid, expected);
    }
}
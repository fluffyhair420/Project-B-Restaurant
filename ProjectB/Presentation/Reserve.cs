using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;


namespace Restaurant
{
    public class Reserve
    {
        string filePath = "DataSources/CurrentUser.json";
        string partySize;
        string reservationDate;
        string reservationName;
        string reservationEmail;
        string reservationPhoneNumber;
        string reservationID = ReserveLogic.GenReservationID();

        /*
        * ReservationForm
        * This is where the information of the reservation is input
        TODO: add check using ReservationDateValid() to check the date input
        TODO: add check to see if party size is over 6
        TODO: make it so that the user that's logged in can see their reservations
        TODO: go to sleep (i'm tired)
        * === === ===
        * if broken, ask shae,
        * if still broken, shae will cry
        */
        private void ReservationForm()
        {
            Console.Clear();
            Console.WriteLine(@$"Restaurant Open Hours:
Monday:       16:00 - 22:00
Tuesday:      16:00 - 22:00
Wednesday:    16:00 - 22:00
Thursday:     16:00 - 22:00
Friday:       16:00 - 22:00
Saturday:     16:00 - 22:00
Sunday:       CLOSED
");
            while (true)
            {
                //Show party size
                Console.WriteLine($"Party Size *: {partySize}");

                //Show reservation date
                Console.WriteLine($"Reservation Date & Time (DD/MM/YYYY HH:MM) *: {reservationDate}");
                if (!UserLogin.userLoggedIn) //*Shows this if there is no user logged in
                {
                    //Show reservation name
                    Console.WriteLine($"Reservation Name *: {reservationName}");
                    //Show reservation email
                    Console.WriteLine($"Reservation Email *: {reservationEmail}");
                    //Show reservation phone number
                    Console.WriteLine($"Reservation Phone Number *: {reservationPhoneNumber}");
                }
                else //*Shows this if there is a user logged in
                {
                    Console.WriteLine($"Reservation Name *: {reservationName}");
                    Console.WriteLine($"Reservation Email *: {reservationEmail}");
                    Console.WriteLine($"Reservation Phone Number *: {reservationPhoneNumber}");
                }
                Console.WriteLine($"Reservation ID: {reservationID}");
                Console.WriteLine("* You can't leave this field empty.");

                /*
                * Users that are logged in don't have to fill in:
                * - Reservation Name
                * - Reservation Email
                * - Reservation Phone Number
                */
                if (string.IsNullOrEmpty(partySize))
                {
                    Console.SetCursorPosition("Party Size *: ".Length, 9);
                    partySize = GetPartySize();
                }
                if (string.IsNullOrEmpty(reservationDate))
                {
                    Console.SetCursorPosition("Reservation Date & Time (DD/MM/YYYY HH:MM) *: ".Length, 10);
                    reservationDate = GetDate();
                }
                if (!UserLogin.userLoggedIn) //*Goes here is there is no user logged in
                {
                    if (string.IsNullOrEmpty(reservationName))
                    {
                        Console.SetCursorPosition("Reservation Name *: ".Length, 11);
                        reservationName = GetName();
                    }
                    if (string.IsNullOrEmpty(reservationEmail))
                    {
                        Console.SetCursorPosition("Reservation Email *: ".Length, 12);
                        reservationEmail = GetEmail();
                    }
                    if (string.IsNullOrEmpty(reservationPhoneNumber))
                    {
                        Console.SetCursorPosition("Reservation Phone Number *: ".Length, 13);
                        reservationPhoneNumber = GetPhoneNumber();
                    }
                }

                break;
            }

            WriteTableJson.WriteJson(Convert.ToInt16(partySize), reservationDate, reservationName, reservationEmail, reservationPhoneNumber, reservationID);
            Email.ConfirmationEmail();
            Console.Clear();
            Console.WriteLine(@$"
A confirmation e-mail has been sent to {reservationEmail}.
Please check your inbox and spam.");
            Program.Main();
        }

        /*
        * All checks for party size:
        // -Check if is empty
        // -Check if is not a number
        // -Check if is > 6
        // -Check if is < 0
        */
        private string GetPartySize()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input)) //*If empty go here
                {
                    Console.Clear();
                    Console.WriteLine("You can't leave the \"Party Size *:\" field empty.");
                    PromptRetryOrQuit();
                }
                else if (!int.TryParse(input, out int n)) //*If not a number go here
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a number.");
                    PromptRetryOrQuit();
                }
                else if (Convert.ToInt16(input) > 6) //*If > 6 go here
                {
                    Console.Clear();
                    Console.WriteLine("Please keep in mind party sizes over 6 need to call to make a reservation.");
                    PromptRetryOrQuit();
                }
                else if (Convert.ToInt32(input) <= 0) //*If <= 0 go here
                {
                    Console.Clear();
                    Console.WriteLine("Party size must be greater than 0");
                    PromptRetryOrQuit();
                }
                else
                {
                    return input;
                }
            }
        }

        /*
        * All checks for Reservation Date:
        // -Check if is empty
        // -Check if format good
        // -Check if not on Sunday
        // -Check if in future
        */
        private string GetDate()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input)) //*If empty go here
                {
                    Console.Clear();
                    Console.WriteLine("You can't leave the \"Reservation Date *:\" field empty.");
                    PromptRetryOrQuit();
                }
                else if (!ReserveLogic.IsDateValid(input)) //*If format not good/on Sunday/not in future go here
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a valid date and time. (DD/MM/YYYY HH:MM)");
                    PromptRetryOrQuit();
                }
                else if (DateTime.ParseExact(input, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture).Date == DateTime.Now.Date)
                {
                    Console.Clear();
                    Console.WriteLine("If you would like to reserve on the same day, you would have to call.");
                    PromptRetryOrQuit();
                }
                else
                {
                    try
                    {
                        DateTime reservationDateTime = DateTime.ParseExact(input, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                        if (reservationDateTime <= DateTime.Now) //* If the entered date is in the past, go here
                        {
                            Console.Clear();
                            Console.WriteLine("Please enter a future date and time.");
                            PromptRetryOrQuit();
                        }
                        return input;
                    }
                    catch (FormatException) //* If the format is correct but the date is invalid, go here
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter a valid date and time.");
                        PromptRetryOrQuit();
                    }
                }
            }
        }

        /*
        * All checks for Reservation Name:
        // -Checks if is empty
        // -Checks if is alphabetical
        */
        private string GetName()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input)) //*If empty go here
                {
                    Console.Clear();
                    Console.WriteLine("You can't leave the \"Reservation Name *:\" field empty.");
                    PromptRetryOrQuit();
                }
                else if (!UserCheck.isNameAlphabetic(input)) //*If not alphabetic go here
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a valid name.");
                    PromptRetryOrQuit();
                }
                else
                {
                    return input;
                }
            }
        }

        /*
        * All Reservation Email checks:
        // -Check if is empty
        // -Check is format is correct
        */
        private string GetEmail()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input)) //*If empty go here
                {
                    Console.Clear();
                    Console.WriteLine("You can't leave the \"Reservation Email *:\" field empty.");
                    PromptRetryOrQuit();
                }
                else if (!UserCheck.EmailCheck(input)) //*If format bad go here
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a valid email.");
                    PromptRetryOrQuit();
                }
                else
                {
                    return input;
                }
            }
        }

        /*
        * All Reservation Phone Number checks:
        // -Check if is empty
        // -Check if format is correct
        */
        private string GetPhoneNumber()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input)) //*If empty go here
                {
                    Console.Clear();
                    Console.WriteLine("You can't leave the \"Reservation Phone Number *:\" field empty.");
                    PromptRetryOrQuit();
                }
                else if (!UserCheck.IsNumeric(input) && input.Length < 10) //*If format bad go here
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a valid phone number.");
                    PromptRetryOrQuit();
                }
                else
                {
                    return input;
                }
            }
        }

        private void PromptRetryOrQuit()
        {
            Console.WriteLine(@"Press Y: Continue
Press Q: Quit");
            while (true)
            {
                string choice = Console.ReadLine();
                if (choice.ToUpper() == "Y")
                {
                    ReservationForm();
                }
                else if (choice.ToUpper() == "Q")
                {
                    Program.Main();
                }
                else
                {
                    Console.WriteLine("Please enter Y or Q");
                }
            }
        }

        /*
        * Reserve.MainReserve() method
        * This is where all the necessary stuff happens for the Reserve class
        ! UNDER CONSTRUCTION
        ! DO NOT TOUCH WITHOUT CONSULTING SHAE
        */
        public void MainReserve()
        {
            /*
            TODO:
            // - Ask user how many people will be in their party
            // - Ask user which table to reserve
            * - Ask user to input reservation date
            * - Ask user to input reservation name
            * - Ask user if they want to change reservation
            * - Ask user if they want to change reservation date
            * - Ask user if they want to change reservation name
            * - Ask user if they want to delete reservation
            */

            dynamic currentUser = ReadCurrentUser.LoadCurrentUser();
            if (currentUser != null)
            {
                reservationName = $"{currentUser.FirstName} {currentUser.LastName}";
                reservationEmail = $"{currentUser.Email}";
                reservationPhoneNumber = $"{currentUser.PhoneNumber}";
            }

            // reservationName = !string.IsNullOrEmpty(currentUser.FirstName) && !string.IsNullOrEmpty(currentUser.LastName) ? $"{currentUser.FirstName} {currentUser.LastName}" : null;
            // reservationEmail = !string.IsNullOrEmpty(currentUser.Email) ? $"{currentUser.Email}" : null;
            // reservationPhoneNumber = !string.IsNullOrEmpty(currentUser.PhoneNumber) ? $"{currentUser.PhoneNumber}" : null;
            Console.Clear();
            while (true)
            {
                Console.WriteLine(@"Keep in mind:
Party sizes over 6 need to call to make a reservation.

Continue to reservation form? Y/N");
                string tempInput = Console.ReadLine();

                if ((tempInput == "Y") || (tempInput == "y"))
                {
                    ReservationForm();
                }
                else if ((tempInput == "N") || (tempInput == "n"))
                {
                    Console.Clear();
                    Program.Main();
                }
                else
                {
                    Console.WriteLine("Please enter Y or N");
                }
            }
        }
    }
}

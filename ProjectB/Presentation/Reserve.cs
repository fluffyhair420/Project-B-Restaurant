// using System.Net;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using System.Globalization;
// using Newtonsoft.Json;


// namespace Restaurant
// {
//     public class Reserve
//     {
//         string filePath = "DataSources/CurrentUser.json";

//         int number;
//         string partySize;
//         string reservationDate;
//         string reservationName;
//         string reservationEmail;
//         string reservationPhoneNumber;
//         string reservationID = GenReservationID();

//         private CurrentUserInfo GetCurrentUserInfo()
//         {
//             CurrentUserInfo currentUser = new CurrentUserInfo();

//             try
//             {
//                 using (StreamReader streamReader = new StreamReader(filePath))
//                 {
//                     string json = streamReader.ReadToEnd();

//                     if (!string.IsNullOrEmpty(json))
//                     {
//                         currentUser = JsonConvert.DeserializeObject<CurrentUserInfo>(json);
//                     }
//                 }
//             }
//             catch (FileNotFoundException e)
//             {
//                 Console.WriteLine($"ERROR: File not found. Please check Reserve.GetCurrentUserInfo() method. {e.Message}.");
//             }
//             catch (Exception e)
//             {
//                 Console.WriteLine($"ERROR: Something went wrong. Please check Reserve.GetCurrentUserInfo() method. {e.Message}");
//             }
//             return currentUser;
//         }

//         /*
//         * ReservationDateValid method takes in:
//         * The reservationDate: is the date the user wants to reserve the table for
//         * === === === ===
//         * Made method private, should not be called outside of the Reserve class
//         * === === === ===
//         * This method checks if the reservationDate is valid according to the format specified
//         * the specified format is dd-MM-yyyy HH:mm
//         * return true if the date works with the format &
//         * return true if the date is in the future &
//         * return true if the date doesn't fall on a Sunday
//         * else return false
//         */
//         private bool IsDateValid(string reservationDate)
//         {
//             string dateFormat = "dd/MM/yyyy HH:mm";
//             DateTime date;

//             if (!DateTime.TryParseExact(reservationDate, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
//             {
//                 return false;
//             }

//             DateTime currentDate = DateTime.Now.Date;

//             if (date <= currentDate || date.DayOfWeek == DayOfWeek.Sunday)
//             {
//                 return false;
//             }

//             return true;
//         }



//         /*
//         * Generates ReservationID
//         */
//         private static string GenReservationID()
//         {
//             Random random = new Random();
//             return random.Next(1000000, 10000000).ToString();
//         }

//         /*
//         * ReservationForm
//         * This is where the information of the reservation is input
//         TODO: add check using ReservationDateValid() to check the date input
//         TODO: add check to see if party size is over 6
//         TODO: make it so that the user that's logged in can see their reservations
//         TODO: go to sleep (i'm tired)
//         * === === ===
//         * if broken, ask shae,
//         * if still broken, shae will cry
//         */
//         private void ReservationForm()
//         {
//             Console.WriteLine(@$"Restaurant Open Hours:
// Monday:       16:00 - 22:00
// Tuesday:      16:00 - 22:00
// Wednesday:    16:00 - 22:00
// Thursday:     16:00 - 22:00
// Friday:       16:00 - 22:00
// Saturday:     16:00 - 22:00
// Sunday:       CLOSEDy
// ");
//             Console.WriteLine($"Party Size: {partySize}");
//             Console.WriteLine($"Reservation Date: {reservationDate}");
//             Console.WriteLine($"Reservation Name: {reservationName}");
//             Console.WriteLine($"Reservation Email: {reservationEmail}");
//             Console.WriteLine($"Reservation Phone Number: {reservationPhoneNumber}");
//             Console.WriteLine($"Reservation ID: {reservationID}");

//             Console.SetCursorPosition("Party Size: ".Length, Console.CursorTop - 6);
//             partySize = Console.ReadLine();
//             Console.SetCursorPosition("Reservation Date: ".Length, Console.CursorTop);
//             reservationDate = Console.ReadLine();
//             if ((reservationName == null))
//             {
//                 Console.SetCursorPosition("Reservation Name: ".Length, Console.CursorTop);
//                 reservationName = Console.ReadLine();
//                 Console.SetCursorPosition("Reservation Email: ".Length, Console.CursorTop);
//                 reservationEmail = Console.ReadLine();
//                 Console.SetCursorPosition("Reservation Phone Number: ".Length, Console.CursorTop);
//                 reservationPhoneNumber = Console.ReadLine();
//             }

//             var writeToJson = new WriteTableJson();
//             writeToJson.WriteJson(Convert.ToInt16(partySize), reservationDate, reservationName, reservationEmail, reservationPhoneNumber, reservationID);
//             Email.ConfirmationEmail();
//             Console.Clear();
//             Console.WriteLine(@$"
// A confirmation e-mail has been sent to {reservationEmail}.
// Please check your inbox and spam.");
//             Program.Main();
//         }

//         /*
//         * Reserve.MainReserve() method
//         * This is where all the necessary stuff happens for the Reserve class
//         ! UNDER CONSTRUCTION
//         ! DO NOT TOUCH WITHOUT CONSULTING SHAE
//         */
//         public void MainReserve()
//         {
//             /*
//             TODO:
//             // - Ask user how many people will be in their party
//             // - Ask user which table to reserve
//             * - Ask user to input reservation date
//             * - Ask user to input reservation name
//             * - Ask user if they want to change reservation
//             * - Ask user if they want to change reservation date
//             * - Ask user if they want to change reservation name
//             * - Ask user if they want to delete reservation
//             */
            
//             CurrentUserInfo currentUser = GetCurrentUserInfo();
//             reservationName = !string.IsNullOrEmpty(currentUser.FirstName) && !string.IsNullOrEmpty(currentUser.LastName) ? $"{currentUser.FirstName} {currentUser.LastName}" : null;
//             reservationEmail = !string.IsNullOrEmpty(currentUser.Email) ? $"{currentUser.Email}" : null;
//             reservationPhoneNumber = !string.IsNullOrEmpty(currentUser.PhoneNumber) ? $"{currentUser.PhoneNumber}" : null;
//             Console.Clear();
//             while (true)
//             {
//                 Console.WriteLine(@"Keep in mind:
// Party sizes over 6 need to call to make a reservation.

// Continue to reservation form? Y/N");
//                 string tempInput = Console.ReadLine();

//                 if ((tempInput == "Y") || (tempInput == "y"))
//                 {
//                     ReservationForm();
//                 }
//                 else if ((tempInput == "N") || (tempInput == "n"))
//                 {
//                     Console.Clear();
//                     Program.Main();
//                 }
//                 else
//                 {
//                     Console.WriteLine("Please enter Y or N");
//                 }
//             }
//         }
//     }
// }

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
                Console.WriteLine($"Party Size *: ");
                Console.WriteLine($"Reservation Date *: ");
                if (!UserLogin.userLoggedIn)
                {
                    Console.WriteLine($"Reservation Name *: ");
                    Console.WriteLine($"Reservation Email *: ");
                    Console.WriteLine($"Reservation Phone Number *: ");
                }
                else
                {
                    Console.WriteLine($"Reservation Name *: {reservationName}");
                    Console.WriteLine($"Reservation Email *: {reservationEmail}");
                    Console.WriteLine($"Reservation Phone Number *: {reservationPhoneNumber}");
                }
                Console.WriteLine($"Reservation ID: {reservationID}");
                Console.WriteLine("* You can't leave this field empty.");

                Console.SetCursorPosition("Party Size *: ".Length, Console.CursorTop - 7);
                partySize = GetPartySize();
                Console.SetCursorPosition("Reservation Date *: ".Length, Console.CursorTop);
                reservationDate = GetDate();

                if (!UserLogin.userLoggedIn)
                {
                    Console.SetCursorPosition("Reservation Name *: ".Length, Console.CursorTop);
                    reservationName = GetName();
                    Console.SetCursorPosition("Reservation Email *: ".Length, Console.CursorTop);
                    reservationEmail = GetEmail();
                    Console.SetCursorPosition("Reservation Phone Number *: ".Length, Console.CursorTop);
                    reservationPhoneNumber = GetPhoneNumber();
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

        private string GetPartySize()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.Clear();
                    Console.WriteLine("You can't leave the \"Party Size *:\" field empty.");
                    PromptRetryOrQuit();
                }
                else if (!int.TryParse(input, out int n))
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a number.");
                    PromptRetryOrQuit();
                }
                else if (Convert.ToInt16(input) > 6)
                {
                    Console.Clear();
                    Console.WriteLine("Please keep in mind party sizes over 6 need to call to make a reservation.");
                    PromptRetryOrQuit();
                }
                else
                {
                    return input;
                }
            }
        }

        private string GetDate()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.Clear();
                    Console.WriteLine("You can't leave the \"Reservation Date *:\" field empty.");
                    PromptRetryOrQuit();
                }
                else if (!ReserveLogic.IsDateValid(input))
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a valid date and time. (dd/MM/yyyy HH:mm)");
                    PromptRetryOrQuit();
                }
                else
                {
                    return input;
                }
            }
        }

        private string GetName()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.Clear();
                    Console.WriteLine("You can't leave the \"Reservation Name *:\" field empty.");
                    PromptRetryOrQuit();
                }
                else if (!UserCheck.isNameAlphabetic(input))
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

        private string GetEmail()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.Clear();
                    Console.WriteLine("You can't leave the \"Reservation Email *:\" field empty.");
                    PromptRetryOrQuit();
                }
                else if (!UserCheck.EmailCheck(input))
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

        private string GetPhoneNumber()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.Clear();
                    Console.WriteLine("You can't leave the \"Reservation Phone Number *:\" field empty.");
                    PromptRetryOrQuit();
                }
                else if (!UserCheck.IsNumeric(input))
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
            Console.WriteLine("Press Y to continue or press Q to quit.");
            while (true)
            {
                string choice = Console.ReadLine();
                if (choice == "Y" || choice == "y")
                {
                    ReservationForm();
                }
                else if (choice == "Q" || choice == "q")
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
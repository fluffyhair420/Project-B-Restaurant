using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;


namespace Restaurant
{
    public class AdminReserve
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
                Console.WriteLine($"Reservation Name *: ");
                Console.WriteLine($"Reservation Email *: ");
                Console.WriteLine($"Reservation Phone Number *: ");
                Console.WriteLine($"Reservation ID: {reservationID}");
                Console.WriteLine("* You can't leave this field empty.");

                Console.SetCursorPosition("Party Size *: ".Length, Console.CursorTop - 7);
                partySize = GetPartySize();
                Console.SetCursorPosition("Reservation Date *: ".Length, Console.CursorTop);
                reservationDate = GetDate();
                Console.SetCursorPosition("Reservation Name *: ".Length, Console.CursorTop);
                reservationName = GetName();
                Console.SetCursorPosition("Reservation Email *: ".Length, Console.CursorTop);
                reservationEmail = GetEmail();
                Console.SetCursorPosition("Reservation Phone Number *: ".Length, Console.CursorTop);
                reservationPhoneNumber = GetPhoneNumber();

                break;
            }

            WriteTableJson.WriteJson(Convert.ToInt16(partySize), reservationDate, reservationName, reservationEmail, reservationPhoneNumber, reservationID);
            Email.ConfirmationEmail();
            Console.Clear();
            Console.WriteLine(@$"
A confirmation e-mail has been sent to {reservationEmail}.
Please check your inbox and spam.");
            MainMenu.Main();
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
                else if (Convert.ToInt16(input) > 48)
                {
                    Console.Clear();
                    Console.WriteLine("Party Size can't be over 48");
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
                    MainMenu.Main();
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
        public void AdminMainReserve()
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
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Continue to reservation form? Y/N");
                string tempInput = Console.ReadLine();

                if ((tempInput == "Y") || (tempInput == "y"))
                {
                    ReservationForm();
                }
                else if ((tempInput == "N") || (tempInput == "n"))
                {
                    Console.Clear();
                    MainMenu.Main();
                }
                else
                {
                    Console.WriteLine("Please enter Y or N");
                }
            }
        }
    }
}
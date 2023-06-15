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
        public string partySize;
        public string reservationDate;
        public string reservationName;
        public string reservationEmail;
        public string reservationPhoneNumber;
        public string reservationID = ReserveLogic.GenReservationID();

        /*
        * ReservationForm
        * This is where the information of the reservation is input
        TODO: add check using ReservationDateValid() to check the date input
        TODO: add check to see if party size is over 6
        TODO: make it so that the user that's logged in can see their reservations
        * === === ===
        * if broken, ask shae
        */
        public void ReservationForm()
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
                Console.WriteLine($"Party Size *: {partySize}");
                Console.WriteLine($"Reservation Date & Time (DD/MM/YYYY HH:MM)*: {reservationDate}");
                Console.WriteLine($"Reservation Name *: {reservationName}");
                Console.WriteLine($"Reservation Email *: {reservationEmail}");
                Console.WriteLine($"Reservation Phone Number *: {reservationPhoneNumber}");
                Console.WriteLine($"Reservation ID: {reservationID}");
                Console.WriteLine("* You can't leave this field empty.");

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

                break;
            }

            WriteTableJson.WriteJson(Convert.ToInt16(partySize), reservationDate, reservationName, reservationEmail, reservationPhoneNumber, reservationID);
            Email.ConfirmationEmail();
            Console.Clear();
            Console.WriteLine(@$"
A confirmation e-mail has been sent to {reservationEmail}.
Please check your inbox and spam.");
            AdminMainMenu.Menu();
        }

        public string GetPartySize() //*ALL THE CHECKS FOR THE PARTY SIZE
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input)) //*Checks if field is empty
                {
                    Console.Clear();
                    Console.WriteLine("You can't leave the \"Party Size *:\" field empty.");
                    PromptRetryOrQuit();
                }
                else if (!int.TryParse(input, out int n)) //*Checks if input is not a number
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a number.");
                    PromptRetryOrQuit();
                }
                else if (Convert.ToInt16(input) > 48) //*Checks if input is > 48
                {
                    Console.Clear();
                    Console.WriteLine("Party Size can't be over 48");
                    PromptRetryOrQuit();
                }
                else if (Convert.ToInt32(input) <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("Party Size can't be less that 1");
                    PromptRetryOrQuit();
                }
                else
                {
                    return input;
                }
            }
        }

        public string GetDate()
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
                    Console.WriteLine("Please enter a valid date and time. (DD/MM/YYYY HH:MM)");
                    PromptRetryOrQuit();
                }
                else
                {
                    return input;
                }
            }
        }

        public string GetName()
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

        public string GetEmail()
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

        public string GetPhoneNumber()
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
                else if (!UserCheck.IsNumeric(input) && input.Length < 10)
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

        public void PromptRetryOrQuit()
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
                    AdminMainMenu.Menu();
                }
                else
                {
                    Console.WriteLine("Please enter Y or Q");
                }
            }
        }

        public void SeeReservation()
        {
            Console.Clear();
            while (true)
            {
                dynamic currentReservations = ReadTableJson.LoadTableJson();
                Console.WriteLine(@"1. See all reservations
2. See user reservations
3. Exit
");
                string tempInput = Console.ReadLine();

                switch (tempInput)
                {
                    case "1":
                        {
                            Console.Clear();
                            if (currentReservations != null)
                            {
                                Console.WriteLine("All current reservations:");
                                foreach (var reservations in currentReservations)
                                {
                                    Console.WriteLine(@$"Party Size: {reservations.PartySize}
Reservation Date: {reservations.ReservationDate}
Reservation Name: {reservations.ReservationName}
Reservation Email: {reservations.ReservationEmail}
Reservation Phone Number: {reservations.ReservationPhoneNumber}
Reservation ID: {reservations.ReservationID}
");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No current reservations.");
                            }
                            break;
                        }
                    case "2":
                        {
                            Console.Clear();
                            Console.WriteLine("Enter Reservation Email:");
                            string emailInput = Console.ReadLine();

                            if (currentReservations != null)
                            {
                                var filteredReservations = ((IEnumerable<dynamic>)currentReservations).Where(r => r.ReservationEmail == emailInput);
                                if (filteredReservations.Any())
                                {
                                    Console.WriteLine($"Reservations for {emailInput}");
                                    foreach (var reservations in filteredReservations)
                                    {
                                        Console.WriteLine(@$"Party Size: {reservations.PartySize}
Reservation Date: {reservations.ReservationDate}
Reservation Name: {reservations.ReservationName}
Reservation Email: {reservations.ReservationEmail}
Reservation Phone Number: {reservations.ReservationPhoneNumber}
Reservation ID: {reservations.ReservationID}
");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"No reservations for {emailInput}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No current reservations");
                            }
                            break;
                        }
                    case "3":
                        {
                            AdminMainReserve();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Please enter 1-3.");
                            continue;
                        }
                }
            }
        }

        public void ChangeReservation()
        {
            dynamic currentReservations = ReadTableJson.LoadTableJson();
            Console.Clear();
            Console.WriteLine("Enter Reservation ID: ");
            string reservationId = Console.ReadLine();

            if (currentReservations != null)
            {
                var reservationToChange = ((IEnumerable<dynamic>)currentReservations).FirstOrDefault(r => r.ReservationID == reservationId);
                if (reservationToChange != null)
                {
                    Console.WriteLine(@$"Current reservation details:
1. Party Size: {reservationToChange.PartySize}
2. Reservation Date: {reservationToChange.ReservationDate}
3. Reservation Name: {reservationToChange.ReservationName}
4. Reservation Email: {reservationToChange.ReservationEmail}
5. Reservation Phone Number: {reservationToChange.ReservationPhoneNumber}
");
                    Console.WriteLine("What do you want to change?");
                    string tempInput = Console.ReadLine();

                    switch (tempInput)
                    {
                        case "1":
                            {
                                while (true)
                                {
                                    Console.WriteLine("New Party Size (or press 'q' to quit): ");
                                    partySize = Console.ReadLine();

                                    if (partySize == "q")
                                    {
                                        Console.WriteLine("Quitting...");
                                        break;
                                    }
                                    else if (string.IsNullOrEmpty(partySize))
                                    {
                                        Console.WriteLine("Can't leave it empty.");
                                    }
                                    else if (!int.TryParse(partySize, out int n))
                                    {
                                        Console.WriteLine("Must be a number.");
                                    }
                                    else if (Convert.ToInt32(partySize) > 48)
                                    {
                                        Console.WriteLine("Can't be greater than 48.");
                                    }
                                    else if (Convert.ToInt32(partySize) <= 0)
                                    {
                                        Console.WriteLine("Can't be less than 1.");
                                    }
                                    else
                                    {
                                        reservationToChange = partySize;
                                        break;
                                    }
                                }

                                break;
                            }
                        case "2":
                            {
                                while (true)
                                {
                                    Console.WriteLine("New Reservation Date (or press 'q' to quit): ");
                                    reservationDate = Console.ReadLine();

                                    if (reservationDate == "q")
                                    {
                                        Console.WriteLine("Quitting...");
                                        break;
                                    }
                                    else if (string.IsNullOrEmpty(reservationDate))
                                    {
                                        Console.WriteLine("You can't leave it empty.");
                                    }
                                    else if (!ReserveLogic.IsDateValid(reservationDate))
                                    {
                                        Console.WriteLine("Please enter a valid date and time. (DD/MM/YYYY HH:MM)");
                                    }
                                    else
                                    {
                                        reservationToChange.ReservationDate = reservationDate;
                                        break;
                                    }
                                }
                                break;
                            }
                        case "3":
                            {
                                while (true)
                                {
                                    Console.WriteLine("New Reservation Name (or press 'q' to quit): ");
                                    reservationName = Console.ReadLine();

                                    if (reservationName == "q")
                                    {
                                        Console.WriteLine("Quitting...");
                                        break;
                                    }
                                    else if (string.IsNullOrEmpty(reservationName))
                                    {
                                        Console.WriteLine("You can't leave it empty.");
                                    }
                                    else if (!UserCheck.isNameAlphabetic(reservationName))
                                    {
                                        Console.WriteLine("Please enter a valid name.");
                                    }
                                    else
                                    {
                                        reservationToChange.ReservationName = reservationName;
                                        break;
                                    }
                                }

                                break;
                            }
                        case "4":
                            {
                                while (true)
                                {
                                    Console.WriteLine("New Reservation Email (or press 'q' to quit): ");
                                    reservationEmail = Console.ReadLine();

                                    if (reservationEmail == "q")
                                    {
                                        Console.WriteLine("Quitting...");
                                        break;
                                    }
                                    else if (string.IsNullOrEmpty(reservationEmail))
                                    {
                                        Console.WriteLine("You can't leave it empty.");
                                    }
                                    else if (!UserCheck.EmailCheck(reservationEmail))
                                    {
                                        Console.WriteLine("Please enter a valid email.");
                                    }
                                    else
                                    {
                                        reservationToChange.ReservationEmail = reservationEmail;
                                        break;
                                    }
                                }

                                break;
                            }
                        case "5":
                            {
                                while (true)
                                {
                                    Console.WriteLine("New Reservation Phone Number (or press 'q' to quit): ");
                                    reservationPhoneNumber = Console.ReadLine();

                                    if (reservationPhoneNumber == "q")
                                    {
                                        Console.WriteLine("Quitting...");
                                        break;
                                    }
                                    else if (string.IsNullOrEmpty(reservationPhoneNumber))
                                    {
                                        Console.WriteLine("You can't leave empty.");
                                    }
                                    else if (!UserCheck.IsNumeric(reservationPhoneNumber) || reservationPhoneNumber.Length < 10)
                                    {
                                        Console.WriteLine("Please enter a valid phone number.");
                                    }
                                    else
                                    {
                                        reservationToChange.ReservationPhoneNumber = reservationPhoneNumber;
                                        break;
                                    }
                                }
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Invalid input. No changes will be made.");
                                break;
                            }
                    }
                    string updatedReservation = JsonConvert.SerializeObject(currentReservations, Formatting.Indented);
                    File.WriteAllText("DataSources/Table.json", updatedReservation);
                    Console.WriteLine("Reservation updated successfully.");
                }
                else
                {
                    Console.WriteLine("No reservation found.");
                }
            }
            else
            {
                Console.WriteLine("No current reservations");
            }
        }

        public void DeleteReservation()
        {
            dynamic currentReservations = ReadTableJson.LoadTableJson();
            Console.Clear();
            Console.WriteLine("Enter Reservation ID: ");
            string reservationId = Console.ReadLine();

            if (currentReservations != null)
            {
                var reservationToDelete = ((IEnumerable<dynamic>)currentReservations).FirstOrDefault(r => r.ReservationID == reservationId);

                if (reservationToDelete != null)
                {
                    currentReservations.Remove(reservationToDelete);
                    string updatedReservation = JsonConvert.SerializeObject(currentReservations, Formatting.Indented);
                    File.WriteAllText("DataSources/Table.json", updatedReservation);
                    Console.WriteLine("Reservation deleted successfully.");
                }
                else
                {
                    Console.WriteLine("No reservation found.");
                }
            }
            else
            {
                Console.WriteLine("No current reservations.");
            }
        }

        /*
        * Admin Reserve
        * This is where all the necessary stuff happens for the Admin Reserve class
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
                Console.WriteLine(@"1. Make reservation
2. See reservation
3. Change reservation
4. Delete reservation
5. Exit
");
                string tempInput = Console.ReadLine();

                switch (tempInput)
                {
                    case "1":
                        {
                            ReservationForm();
                            break;
                        }
                    case "2":
                        {
                            SeeReservation();
                            break;
                        }
                    case "3":
                        {
                            ChangeReservation();
                            break;
                        }
                    case "4":
                        {
                            DeleteReservation();
                            break;
                        }
                    case "5":
                        {
                            AdminMainMenu.Menu();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Please enter 1-5.");
                            continue;
                        }
                }
            }
        }
    }
}
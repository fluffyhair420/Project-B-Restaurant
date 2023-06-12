using Newtonsoft.Json;

namespace Restaurant
{
    public static class MyBookings
    {
        private static string bookingPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Table.json"));
        private static string readBookingPath = File.ReadAllText(bookingPath);
        private static List<Table> bookingData = JsonConvert.DeserializeObject<List<Table>>(readBookingPath);
        private static string currentUserPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/CurrentUser.json"));
        private static string currentUserJson = File.ReadAllText(currentUserPath);
        static dynamic currentUserData = JsonConvert.DeserializeObject(currentUserJson);
        public static bool foundBooking {get; set;}

        public static bool MyBooking()
        {
            readBookingPath = File.ReadAllText(bookingPath);
            bookingData = JsonConvert.DeserializeObject<List<Table>>(readBookingPath);
            currentUserJson = File.ReadAllText(currentUserPath);
            currentUserData = JsonConvert.DeserializeObject(currentUserJson);

            if (bookingData == null || bookingData.Count == 0)
            {
                foundBooking = false;
                Console.WriteLine("There's no booking yet!");
                Program.Main();
                return foundBooking;
            }

            var filteredBookings = bookingData.Where(booking => (string)booking.ReservationEmail == (string)currentUserData.Email).ToList();
            Console.WriteLine("\n=== My Bookings ===");
            if (filteredBookings.Count > 0)
            {
                foundBooking = true;
                // Display the filtered bookings and allow the user to perform actions
                foreach (var booking in filteredBookings)
                {
                    Console.WriteLine($@"
Full name: {booking.ReservationName}
Email: {booking.ReservationEmail}
Reservation date: {booking.ReservationDate}
Party size: {booking.PartySize}
Reservation ID: {booking.ReservationID}
");
                }

                // Rest of the code for actions like edit, cancel, etc.
                Console.WriteLine(@"
1. Edit booking
2. Cancel booking
3. Back");
                bool newInput = false;
                while (!newInput)
                {
                    string userInput = Console.ReadLine();
                    switch (userInput)
                    {
                        case "1":
                            newInput = true;
                            EditBooking(filteredBookings, currentUserData);
                            break;
                        case "2":
                            newInput = true;
                            CancelBooking(filteredBookings, currentUserData);
                            break;
                        case "3":
                            newInput = true;
                            Program.Main();
                            break;
                        default:
                            Console.WriteLine("\nInvalid input. Please type 1-3.");
                            break;
                    }
                }
            }
        
            if (filteredBookings.Count <= 0)
            {
                foundBooking = false;
                Console.WriteLine("You haven't made any bookings with us yet!");
                Program.Main();
                return foundBooking;
            }

            return foundBooking;
        }               

        public static void EditBooking(List<Table> filteredBookings, dynamic currentuserdata)
        {
            Console.WriteLine("\n=== Edit Booking ===");
            Console.WriteLine(@"
Please enter the Reservation ID of the booking you want to edit.
Press Escape to go back.
");

            bool validInput = false;
            while (!validInput)
            {
                Console.Write("Reservation ID: ");
                string reservationId = string.Empty;
                ConsoleKeyInfo keyInfo;

                do
                {
                    keyInfo = Console.ReadKey();

                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine();
                        MyBooking();
                        return;
                    }

                    // Process the key press
                    if (keyInfo.Key != ConsoleKey.Enter)
                        reservationId += keyInfo.KeyChar;

                } while (keyInfo.Key != ConsoleKey.Enter);

                var changeBooking = filteredBookings.Where(booking => (string)booking.ReservationID == (string)reservationId && (string)booking.ReservationEmail == (string)currentUserData.Email);

                if (changeBooking.Any())
                {
                    validInput = true; // Set flag to indicate valid input
                    // Perform operations on the matched bookings
                    foreach (var booking in changeBooking)
                    {
                        // Do something with the booking
                        Console.WriteLine($"Found booking with ID {booking.ReservationID}:");
                        Console.WriteLine($@"
Full name: {booking.ReservationName}
Email: {booking.ReservationEmail}
Reservation date: {booking.ReservationDate}
Party size: {booking.PartySize}
Reservation ID: {booking.ReservationID}
");
                        Console.WriteLine("What do you want to change?");
                        Console.WriteLine(@"
1. Reservation date
2. Party size
3. Back
");
                        bool newInput = false;
                        while (newInput == false)
                        {
                            string userInput = Console.ReadLine();
                            switch (userInput)
                            {
                                case "1":
                                    newInput = true;
                                    ChangeDate(booking, currentuserdata); // change this ????? niet meer??????
                                    break;
                                case "2":
                                    newInput = true;
                                    ChangePartySize(booking, currentuserdata);
                                    break;
                                case "3":
                                    newInput = true;
                                    MyBooking();
                                    break;
                                default:
                                    Console.WriteLine("\nInvalid input.");
                                    break;
                            }
                        }
                    }
                }

                else
                {
                    // No bookings matched the reservation ID
                    Console.WriteLine(@"
No booking found with the provided Reservation ID.
Please make sure you filled in the correct Reservation ID.
");
                }
            }
            MyBooking();
        }

        public static void CancelBooking(List<Table> filteredBookings, dynamic currentuserdata)
        {
            bool validInput = false;
            Console.WriteLine("\n=== Cancel Booking ===");
            Console.WriteLine("Please enter the Reservation ID of the booking you want to cancel. Press Escape to go back.");
            while (!validInput)
            {
                Console.Write("Reservation ID: ");
                string reservationId = string.Empty;
                ConsoleKeyInfo keyInfo;

                do
                {
                    keyInfo = Console.ReadKey();

                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine();
                        MyBooking();
                        return;
                    }

                    // Process the key press
                    if (keyInfo.Key != ConsoleKey.Enter)
                        reservationId += keyInfo.KeyChar;

                } while (keyInfo.Key != ConsoleKey.Enter);

                var removeBooking = filteredBookings.FirstOrDefault(booking => (string)booking.ReservationID == reservationId && (string)booking.ReservationEmail == (string)currentUserData.Email);

                if (removeBooking != null)
                {
                    validInput = true;

                    // Remove the booking from the json file
                    bookingData.Remove(removeBooking);

                    // Serialize the updated file to JSON
                    string updatedBookingJsonContents = JsonConvert.SerializeObject(bookingData, Formatting.Indented);

                    // Write the updated JSON contents back to the file
                    File.WriteAllText(bookingPath, updatedBookingJsonContents);

                    Console.WriteLine($"Booking with ID {removeBooking.ReservationID} has been removed.");
                    Email.BookingCancelation(removeBooking);
                }

                else
                {
                    Console.WriteLine(@"
No booking found with the provided Reservation ID.
Please make sure you made a reservation with us and filled in the correct Reservation ID.
");
                }
            }
        }

        public static void ChangeDate(Table booking, dynamic currentuserdata)
        {
            string newBookDate = "";
            bool validDate = false;
            while (!validDate)
            {
                Console.Write("Enter new booking date (DD/MM/YYYY 00:00): ");
                newBookDate = Console.ReadLine();

                string format = "dd/MM/yyyy HH:mm";
                DateTime parsedDateTime;

                bool isValidDateTime = DateTime.TryParseExact(newBookDate, format,
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out parsedDateTime);
                    

                DateTime currentDate = DateTime.Now.Date;

                if (parsedDateTime.Day <= currentDate.Day || parsedDateTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    Console.WriteLine("Invalid DateTime format.");
                }

                // Set the opening and closing hours for the restaurant
                TimeSpan openingTime = new TimeSpan(16, 0, 0); // 16:00 (4:00 PM)
                TimeSpan closingTime = new TimeSpan(21, 0, 0); // 21:00 (9:00 PM)

                // For future dates, check if the reservation time is within the opening and closing hours
                if (parsedDateTime.TimeOfDay < openingTime || parsedDateTime.TimeOfDay > closingTime)
                {
                    Console.WriteLine("Invalid DateTime format.");
                }

                else
                {
                    validDate = true;
                }
            }

            if (validDate)
            {
                booking.ReservationDate = newBookDate;
            }
            string updatedBookingJsonContents = JsonConvert.SerializeObject(bookingData, Formatting.Indented);
            File.WriteAllText(bookingPath, updatedBookingJsonContents);
            Console.WriteLine("Reservation date updated.\n");
            Email.DateChangedEmail(booking);
            
        }

        public static void ChangePartySize(Table booking, dynamic currentuserdata)
        {
            Console.WriteLine(@"
Keep in mind, if you want to invite more people than originally planned,
you'll have to contact Vari Flavors directly.
");
            string newPartySize = "";
            bool validSize = false;
            while (!validSize)
            {  

                Console.Write("Enter new party size: ");
                newPartySize = Console.ReadLine();

                if (Convert.ToInt32(newPartySize) <= booking.PartySize && Convert.ToInt32(newPartySize) >= 1)
                {
                    validSize = true;

                }
                else
                {
                    Console.WriteLine(@"
You can only change your party size to a smaller group with a minimum of 1.
Please contact us directly if you want to invite more people.
");
                }
            }

            if (validSize)
            {
                booking.PartySize = Convert.ToInt32(newPartySize);
            }
            string updatedBookingJsonContents = JsonConvert.SerializeObject(bookingData, Formatting.Indented);
            File.WriteAllText(bookingPath, updatedBookingJsonContents);
            Console.WriteLine("Party size updated.\n");
            Email.PartySizeChangedEmail(booking);
        }
    }
}

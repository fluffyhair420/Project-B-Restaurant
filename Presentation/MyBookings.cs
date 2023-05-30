using Newtonsoft.Json;

namespace Restaurant
{
    public static class MyBookings
    {
        private static string bookingPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Table.json"));
        private static string readBookingPath = File.ReadAllText(bookingPath);
        static dynamic bookingData = JsonConvert.DeserializeObject(readBookingPath);
        private static string currentUserPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/CurrentUser.json"));
        private static string currentUserJson = File.ReadAllText(currentUserPath);
        static dynamic currentUserData = JsonConvert.DeserializeObject(currentUserJson);

        public static void MyBooking()
        {
            readBookingPath = File.ReadAllText(bookingPath);
            bookingData = JsonConvert.DeserializeObject(readBookingPath);
            currentUserJson = File.ReadAllText(currentUserPath);
            currentUserData = JsonConvert.DeserializeObject(currentUserJson);
            bool foundBooking = false;
            if (bookingData != null)
            {
                foreach (var booking in bookingData)
                {
                    Console.WriteLine(currentUserData.Email);
                    if (currentUserData.Email == booking.ReservationEmail)
                    {
                        Console.WriteLine("\n=== My Booking ===");
                        Console.WriteLine($@"
Full name: {booking.ReservationName}
Email: {booking.ReservationEmail}
Reservation date: {booking.ReservationDate}
Party size: {booking.PartySize}
Reservation ID: {booking.ReservationID}"
);
                    foundBooking = true;
                    break;
                    }
                }                
                
            }
            if (!foundBooking)
            {
                Console.WriteLine("You haven't made any bookings with us yet!");
                MainMenu.Main();
            }

            if (bookingData == null)
            {
                Console.WriteLine("There's no booking yet!");
                MainMenu.Main();
            }
        }
    }
}
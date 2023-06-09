using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using Newtonsoft.Json;

namespace Restaurant
{
    public static class Email
    {
        private static string bookingPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Table.json"));
        private static string readBookingPath = File.ReadAllText(bookingPath);
        static dynamic data = JsonConvert.DeserializeObject(readBookingPath);
        private static string currentUserPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/CurrentUser.json"));
        private static string currentUserJson = File.ReadAllText(currentUserPath);
        static dynamic currentUserData = JsonConvert.DeserializeObject(currentUserJson);

        public static void ConfirmationEmail()
        {
            readBookingPath = File.ReadAllText(bookingPath);
            data = JsonConvert.DeserializeObject(readBookingPath);
            currentUserJson = File.ReadAllText(currentUserPath);
            currentUserData = JsonConvert.DeserializeObject(currentUserJson);

            //Console.WriteLine(data.ReservationEmail);
            dynamic lastBooking = null;
            foreach (var booking in data)
            {
                if (!UserLogin.userLoggedIn)
                {
                    lastBooking = booking;
                }

                else if (booking.ReservationEmail == currentUserData.Email)
                {
                    lastBooking = booking;
                }
            }

            if (lastBooking != null)
            {
                string recipientEmail = lastBooking.ReservationEmail;
                string subject = "Booking Confirmation";
                string body = @$"Dear {lastBooking.ReservationName},

We are delighted to inform you that your booking with reference number {lastBooking.ReservationID} for {lastBooking.ReservationDate} for a {lastBooking.PartySize} person table has been booked successfully.
We look forward to welcome you in our restaurant!";
                SendEmail(recipientEmail, lastBooking, subject, body);
            }
        }

        public static void DateChangedEmail(Table booking)
        {
            readBookingPath = File.ReadAllText(bookingPath);
            data = JsonConvert.DeserializeObject(readBookingPath);
            currentUserJson = File.ReadAllText(currentUserPath);
            currentUserData = JsonConvert.DeserializeObject(currentUserJson);

            //Console.WriteLine(booking.ReservationDate);
            if (booking != null)
            {
                string recipientEmail = booking.ReservationEmail;
                string subject = "Booking date changed";
                string body = @$"Dear {booking.ReservationName},

We would like to inform you that the date of your booking with reference number {booking.ReservationID} has successfully been changed to {booking.ReservationDate}.
We look forward to welcome you in our restaurant!";
                SendEmail(recipientEmail, booking, subject, body);
            }
        }

        public static void PartySizeChangedEmail(Table booking)
        {
            readBookingPath = File.ReadAllText(bookingPath);
            data = JsonConvert.DeserializeObject(readBookingPath);
            currentUserJson = File.ReadAllText(currentUserPath);
            currentUserData = JsonConvert.DeserializeObject(currentUserJson);

            if (booking != null)
            {
                string recipientEmail = booking.ReservationEmail;
                string subject = "Party size changed";
                string body = @$"Dear {booking.ReservationName},

We would like to inform you that the party size of your booking with reference number {booking.ReservationID} for {booking.ReservationDate} has successfully been changed to {booking.PartySize}.
We look forward to welcome you in our restaurant!";
                SendEmail(recipientEmail, booking, subject, body);
            }
        }

        public static void BookingCancelation(Table booking)
        {
            readBookingPath = File.ReadAllText(bookingPath);
            data = JsonConvert.DeserializeObject(readBookingPath);
            currentUserJson = File.ReadAllText(currentUserPath);
            currentUserData = JsonConvert.DeserializeObject(currentUserJson);

            if (booking != null)
            {
                string recipientEmail = booking.ReservationEmail;
                string subject = "Reservation canceled";
                string body = @$"Dear {booking.ReservationName},

We would like to inform you that your booking with reference number {booking.ReservationID} for {booking.ReservationDate} for {booking.PartySize} people has successfully been canceled.
We look forward to welcome you in our restaurant some other time!";
                SendEmail(recipientEmail, booking, subject, body);
            }
        }

        private static void SendEmail(string recipientEmail, dynamic reservation, string subject, string body) // string subject, string body
        {
            // setup sender's information
            EmailInfo senderInfo = new EmailInfo();
            string senderEmail = senderInfo.Email;
            string senderPassword = senderInfo.Password;
            string senderDisplayName = senderInfo.DisplayName;

            // setup message to user/guest
            MailMessage message = new MailMessage();
            message.From = new MailAddress(senderEmail, senderDisplayName);
            message.To.Add(recipientEmail);
            message.Subject = subject;
            message.Body = body;
            
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
            smtpClient.EnableSsl = true;
            smtpClient.Send(message);
        }
    }
}
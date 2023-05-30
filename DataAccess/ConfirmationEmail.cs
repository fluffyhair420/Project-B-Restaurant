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

        public static void Info()
        {
            readBookingPath = File.ReadAllText(bookingPath);
            data = JsonConvert.DeserializeObject(readBookingPath);
            currentUserJson = File.ReadAllText(currentUserPath);
            currentUserData = JsonConvert.DeserializeObject(currentUserJson);
            string recipientEmail = "";

            //Console.WriteLine(data.ReservationEmail);
            foreach (var booking in data)
            if (booking.ReservationEmail == currentUserData.Email)
            {
                recipientEmail = booking.ReservationEmail;
                SendEmail(recipientEmail, booking);
            }


            Console.WriteLine(@$"
A confirmation e-mail has been sent to {recipientEmail}.
Please check your inbox and spam.");
        }


        private static void SendEmail(string recipientEmail, dynamic reservation)
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
            message.Subject = "Booking Confirmation";
            message.Body = @$"Dear {reservation.ReservationName},

We are enlightened to inform you that your booking with reference number {reservation.ReservationID} for {reservation.ReservationDate} for a {reservation.PartySize} person table has been booked succesfully.
We look forward to welcome you in our restaurant!";
            
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
            smtpClient.EnableSsl = true;
            smtpClient.Send(message);
        }
    }
}
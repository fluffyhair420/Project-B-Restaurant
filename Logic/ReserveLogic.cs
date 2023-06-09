using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace Restaurant
{
    public static class ReserveLogic
    {
        /*
        * ReservationDateValid method takes in:
        * The reservationDate: is the date the user wants to reserve the table for
        * === === === ===
        * This method checks if the reservationDate is valid according to the format specified
        * the specified format is dd-MM-yyyy HH:mm
        * return true if the date works with the format &
        * return true if the date is in the future &
        * return true if the date doesn't fall on a Sunday
        * else return false
        */
        public static bool IsDateValid(string reservationDate)
        {
            string dateFormat = "dd/MM/yyyy HH:mm";
            DateTime date;

            if (!DateTime.TryParseExact(reservationDate, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                return false;
            }

            DateTime currentDate = DateTime.Now.Date;

            if (date <= currentDate || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }

            return true;
        }



        /*
        * Generates ReservationID
        */
        public static string GenReservationID()
        {
            Random random = new Random();
            return random.Next(1000000, 10000000).ToString();
        }
    }
}
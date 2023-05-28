using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Restaurant
{
    public class WriteTableJson
    {
        string jsonFile = "DataSources/Table.json";

        /*
        * WriteTableJson.WriteJson() method takes in:
        * The reservationName: is the name that the user wants the table to be reserved for
        * The reservationDate: is the date that the user wants to reserve the table for
        * === === === ===
        * Made the method private, should not be used outside the Reserve class
        */
        public void WriteJson(int partySize, string reservationDate, string reservationName, string reservationEmail, string reservationPhoneNumber, string reservationID)
        {
            Table reservation = new Table()
            {
                PartySize = partySize,
                ReservationDate = reservationDate,
                ReservationName = reservationName,
                ReservationEmail = reservationEmail,
                ReservationPhoneNumber = reservationPhoneNumber,
                ReservationID = reservationID
            };

            string json = JsonConvert.SerializeObject(reservation);
            File.WriteAllText(jsonFile, json);
        }
    }
}
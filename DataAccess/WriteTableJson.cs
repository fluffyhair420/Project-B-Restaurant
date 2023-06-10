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

            List<Table> reservations;

            // Read the existing JSON from the file
            string existingJson = File.ReadAllText(jsonFile);

            // Check if the file is empty or contains no valid JSON
            if (string.IsNullOrWhiteSpace(existingJson))
            {
                // If the file is empty, create a new list
                reservations = new List<Table>();
            }
            else
            {
                // If the file has existing reservations, deserialize the JSON
                reservations = JsonConvert.DeserializeObject<List<Table>>(existingJson);

                // Check if deserialization failed or the deserialized object is null
                if (reservations == null)
                {
                    // If deserialization failed or the object is null, create a new list
                    reservations = new List<Table>();
                }
            }

            // Add the new reservation to the list
            reservations.Add(reservation);

            // Serialize the entire reservations list
            string updatedJson = JsonConvert.SerializeObject(reservations, Formatting.Indented);
            File.WriteAllText(jsonFile, updatedJson);
        }

    }
}
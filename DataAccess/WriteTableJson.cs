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
        * The tableNumber: is the number of the table that the user wants to reserve
        * The reservationName: is the name that the user wants the table to be reserved for
        * The reservationDate: is the date that the user wants to reserve the table for
        * === === === ===
        * Made the method private, should not be used outside the Reserve class
        */
        public void WriteJson(int tableNumber, string reservationName, string reservationDate, int reservationID)
        {
            try
            {
                var readVar = new ReadTableJson();
                List<Table> tables = readVar.LoadJson();

                Table table = tables.FirstOrDefault(t => t.TableNumber == tableNumber);

                Reservation newReservation = new Reservation
                {
                    Name = reservationName,
                    Date = reservationDate,
                    ID = reservationID
                };

                table.Reservation.Add(newReservation);
                var sortedReservations = table.Reservation.OrderBy(r => DateTime.ParseExact(r.Date, "dd/MM/yyyy HH:mm", null)).ToList();

                table.Reservation = sortedReservations;

                string jsonObject = JsonConvert.SerializeObject(tables, Formatting.Indented);
                using (StreamWriter writer = new StreamWriter(jsonFile))
                {
                    writer.Write(jsonObject);
                }

                Console.WriteLine($"Table {tableNumber} has been reserved for {reservationName} on {reservationDate}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: Something went wrong. Please check Reserve.ReserveTable() method. {e.Message}.");
            }
        }
    }
}
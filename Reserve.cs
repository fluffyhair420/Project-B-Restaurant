using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;

namespace Project_B_Restaurant_Code {
    public class Reserve {
        string jsonFile = "./Table.json"; // Path to the json file

        /*
        * Reserve.LoadJson() method loads reads the JSON file
        * Sets the JSON data in a list of Table objects
        * Returns a list of Table objects for use later
        * Made the method public so it will be accessible later on
        */
        public List<Table> LoadJson() {
            List<Table> tables = new List<Table>(); // Create a new list of Table objects
            try {
                using (StreamReader reader = new StreamReader(jsonFile)) {
                    string json = reader.ReadToEnd();
                    tables = JsonConvert.DeserializeObject<List<Table>>(json);
                }
            } catch (FileNotFoundException e) {
                Console.WriteLine($"ERROR: File not found. Please check Reserve.LoadJson() method. {e.Message}.");
            } catch (Exception e) {
                Console.WriteLine($"ERROR: Something went wrong. Please check Reserve.LoadJson() method. {e.Message}.");
            }
            return tables;
        }

        /*
        * Reserve.ReserveTable() method takes in:
        * The tableNumber: is the number of the table that the user wants to reserve
        * The reservationName: is the name that the user wants the table to be reserved for
        * The reservationDate: is the date that the user wants to reserve the table for
        * ===
        * Made the method private, should not be used outside the Reserve class
        */
        private void ReserveTable(int tableNumber, string reservationName, string reservationDate) {
            try {
                List<Table> tables = LoadJson();

                Table table = tables.FirstOrDefault(t => t.TableNumber == tableNumber);

                if (table == null) {
                    Console.WriteLine($"ERROR: Table {tableNumber} does not exist");
                    return;
                }

                if (table.Reserved) {
                    Console.WriteLine($"ERROR: Table {tableNumber} is already reserved.");
                    return;
                }

                table.Reserved = true;
                table.ReservedName = reservationName;
                table.ReservedDate.Add(reservationDate);

                string json = JsonConvert.SerializeObject(tables, Formatting.Indented);
                using (StreamWriter writer = new StreamWriter(jsonFile)) {
                    writer.Write(json);
                }

                Console.WriteLine($"Table {tableNumber} has been reserved for {reservationName} on {reservationDate}.");
            } catch (FileNotFoundException e) {
                Console.WriteLine($"ERROR: File not found. Please check Reserve.ReserveTable() method. {e.Message}.");
            } catch (Exception e) {
                Console.WriteLine($"ERROR: Something went wrong. Please check Reserve.ReserveTable() method. {e.Message}.");
            }
        }

        /*
        * Reserve.ReservationDateValid method takes in:
        * The reservationDate: is the date the user wants to reserve the table for
        * ===
        * Made method private, should not be called outside of the Reserve class
        * ===
        * This method checks if the reservationDate is valid according to the format specified
        * the specified format is dd-MM-yyyy
        * return true if the date works with the format, else return false
        */
        private bool ReservationDateValid(string reservationDate) {
            string datetimeFormat = "dd-MM-yyyy";
            DateTime date;
            return true ? (DateTime.TryParseExact(reservationDate, datetimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date)) : false;
        }

        /*
        * Reserve.MainReserve() method
        * This is where all the necessary stuff happens for the Reserve class
        ! UNDER CONSTRUCTION
        ! DO NOT TOUCH WITHOUT CONSULTING SHAE
        */
        public void MainReserve() {

            // Console.WriteLine("How many people are in your party? (Please enter amount)");
            // Console.WriteLine("Keep in mind, we can currently only seat up to 6 people at a time.");
            // int partyAmount = Convert.ToInt16(Console.ReadLine());
            // if (partyAmount > 6) {
            //     Console.WriteLine("We can currently only seat up to 6 people at a time");
            // } else if (partyAmount < 1) {
            //     Console.WriteLine("Haha funny");
            // } else if (partyAmount <= 2) {
            //     Console.WriteLine("");
            // }
        }
    }
}
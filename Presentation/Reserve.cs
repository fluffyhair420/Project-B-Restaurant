using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;

namespace Restaurant {
    public class Reserve {
        string jsonFile = "DataSources/Table.json"; // Path to the json file

        int partySize = 0;
        int tableNumber = 0;
        string reservationDate = "";
        string reservationName = "";
        int reservationID = GenReservationID();

        /*
        * Reserve.LoadJson() method loads reads the JSON file
        * Sets the JSON data in a list of Table objects
        * Returns a list of Table objects for use later
        * ===
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
        * ReserveTable() method takes in:
        * The tableNumber: is the number of the table that the user wants to reserve
        * The reservationName: is the name that the user wants the table to be reserved for
        * The reservationDate: is the date that the user wants to reserve the table for
        * === === === ===
        * Made the method private, should not be used outside the Reserve class
        */
        private void ReserveTable(int tableNumber, string reservationName, string reservationDate, int reservationID) {
            try {
                List<Table> tables = LoadJson();

                Table table = tables.FirstOrDefault(t => t.TableNumber == tableNumber);

                Reservation newReservation = new Reservation {
                    Name = reservationName,
                    Date = reservationDate,
                    ID = reservationID
                };

                table.Reservation.Add(newReservation); // add the new reservation to the Table list
                table.Reservation = table.Reservation.OrderBy(r => r.Date).ToList(); // sort the Table list by earliest date to latest date

                string json = JsonConvert.SerializeObject(tables, Formatting.Indented);
                using (StreamWriter writer = new StreamWriter(jsonFile)) {
                    writer.Write(json);
                }

                Console.WriteLine($"Table {tableNumber} has been reserved for {reservationName} on {reservationDate}.");
            } catch (Exception e) {
                Console.WriteLine($"ERROR: Something went wrong. Please check Reserve.ReserveTable() method. {e.Message}.");
            }
        }


        /*
        * 
        TODO: Compare user input date with already existing date in the JSON
        */
        private bool CompareReservationDate(string reservationDate, int tableNumber) {
            List<Table> tables = LoadJson();
            DateTime inputDate;

            if (!DateTime.TryParse(reservationDate, out inputDate)) {
                // The input date string could not be parsed, return false
                return false;
            }

            // Filter the list of tables to get the table with the specified number
            Table table = tables.FirstOrDefault(t => t.TableNumber == tableNumber);

            foreach (Reservation reservation in table.Reservation) {
                DateTime reservationDateTime;

                if (DateTime.TryParse(reservation.Date, out reservationDateTime)) {
                    // Calculate the absolute difference in hours between the two dates
                    double hoursDiff = Math.Abs((reservationDateTime - inputDate).TotalHours);

                    // If the difference is less than 2 hours, return true
                    if (hoursDiff < 2) {
                        return true;
                    }
                }
            }

            // No matching reservations were found, return false
            return false;
        }


        /*
        * ReservationDateValid method takes in:
        * The reservationDate: is the date the user wants to reserve the table for
        * === === === ===
        * Made method private, should not be called outside of the Reserve class
        * === === === ===
        * This method checks if the reservationDate is valid according to the format specified
        * the specified format is dd-MM-yyyy HH:mm
        * return true if the date works with the format, else return false
        */
        private bool ReservationDateValid(string reservationDate) {
            string datetimeFormat = "dd/MM/yyyy HH:mm";
            DateTime date;
            bool isDateTime = DateTime.TryParseExact(reservationDate, datetimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
            return isDateTime;
        }


        /*
        * Generates ReservationID
        */
        private static int GenReservationID() {
            Random random = new Random();
            return random.Next(1000000, 10000000);
        }

        private void ShowReservationInfo(int partySize, int tableNumber, string reservationDate, string reservationName, int reservationID) {
            Console.WriteLine(@$"===================================
Party Size: {partySize}
Table Number: {tableNumber}
Reservation Date: {reservationDate}
Reservation Name: {reservationName}
Reservation ID: {reservationID}
===================================");
        }

        /*
        * Reserve.MainReserve() method
        * This is where all the necessary stuff happens for the Reserve class
        ! UNDER CONSTRUCTION
        ! DO NOT TOUCH WITHOUT CONSULTING SHAE
        */
        public void MainReserve() {
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

            List<Table> tables = LoadJson();

            Console.WriteLine("Would you like to make a reservation? Y/N");
            string userInput = Console.ReadLine();

            if (userInput == "Y" || userInput == "y") {

                while (true) {
                    ShowReservationInfo(partySize, tableNumber, reservationDate, reservationName, reservationID);
                    Console.WriteLine("What is the size of your party?");
                    Console.WriteLine("Keep in mind that we can only seat up to 6 per party");
                    Console.Write("Party size: ");
                    partySize = Convert.ToInt16(Console.ReadLine());
                    if (partySize < 1 || partySize > 6) {
                        Console.WriteLine("Please keep in mind that we can only seat up to 6 per party");
                    } else {
                        Console.Clear();
                        break;
                    }
                }

                while (true) {
                    ShowReservationInfo(partySize, tableNumber, reservationDate, reservationName, reservationID);
                    Console.Write($"Available tables party of {partySize}: ");
                    switch (partySize) {
                        case 1:
                        case 2: {
                                Console.WriteLine("1 2 3 4 5 6 7 8");
                                break;
                            }
                        case 3:
                        case 4: {
                                Console.WriteLine("9 10 11 12 13");
                                break;
                            }
                        case 5:
                        case 6: {
                                Console.WriteLine("14 15");
                                break;
                            }
                    }
                    Console.WriteLine("Which table would you like to reserve?");
                    tableNumber = Convert.ToInt16(Console.ReadLine());
                    if (partySize <= 2 && (tableNumber >= 1 && tableNumber <= 8)) {
                        Console.Clear();
                        break;
                    } else if (partySize <= 4 && (tableNumber >= 9 && tableNumber <= 13)) {
                        Console.Clear();
                        break;
                    } else if (partySize <= 6 && (tableNumber >= 14 && tableNumber <= 15)) {
                        Console.Clear();
                        break;
                    } else {
                        Console.WriteLine("\nThis table does not exist");
                    }
                }

                while (true) {
                    ShowReservationInfo(partySize, tableNumber, reservationDate, reservationName, reservationID);
                    Console.WriteLine(@"Open hours:
Monday:     17:00-22:00
Tuesday:    17:00-22:00
Wednesday:  17:00-22:00
Thursday:   17:00-22:00
Friday:     17:00-22:00
Saturday:   17:00-22:00
Sunday:     17:00-21:00");
                    Console.WriteLine("When would you like to schedule the reservation?");
                    Console.WriteLine("Keep in mind the format is dd/MM/yyyy HH:mm");
                    Console.WriteLine("For example 01/04/2023 17:00");
                    reservationDate = Console.ReadLine();
                    if (ReservationDateValid(reservationDate) == true) {
                        if (CompareReservationDate(reservationDate, tableNumber) == true) {
                            Console.WriteLine("The reservation time is within 2 hours of another reservation, please try another");
                        } else {
                            Console.Clear();
                            break;
                        }
                    } else {
                        Console.WriteLine("The date you input is invalid, please try again.");
                    }
                }

                while (true) {
                    ShowReservationInfo(partySize, tableNumber, reservationDate, reservationName, reservationID);
                    Console.WriteLine("Please enter the name for the reservation.");
                    reservationName = Console.ReadLine();
                    if (reservationName != null) {
                        Console.Clear();
                        break;
                    } else {
                        Console.WriteLine("You can't leave this field empty");
                        Console.WriteLine("Please try again.");
                    }
                }

                while (true) {
                    Console.WriteLine("Is the information correct? Y/N");
                    ShowReservationInfo(partySize, tableNumber, reservationDate, reservationName, reservationID);
                    string userConfirm = Console.ReadLine();
                    if (userConfirm == "Y" || userConfirm == "y") {
                        ReserveTable(tableNumber, reservationName, reservationDate, reservationID);
                        break;
                    } else {
                        Console.WriteLine("Please enter the correct information.");
                        MainReserve();
                    }
                }
            } else {
                return;
            }
        }
    }
}
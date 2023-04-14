using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_B_Restaurant_Code {
    public class Table {
        public int TableNumber { get; set; }
        public int Seats { get; set; }
        public List<Reservation> Reservation { get; set; }
    }
}
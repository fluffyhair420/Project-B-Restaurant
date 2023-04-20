using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant {
    public class Table {
        public int TableNumber { get; set; }
        public int Seats { get; set; }
        public List<Reservation> Reservation { get; set; }
    }
}
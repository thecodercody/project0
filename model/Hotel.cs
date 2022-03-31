using System;

namespace model {
    public class Hotel {
        //properties
        public int resNum { get; set; }
        public int tripNum { get; set; }
        public DateTime checkIn { get; set; }
        public DateTime checkOut { get; set; }
        public string name { get; set; }
    
        //constructors
        public Hotel() {}
        public Hotel(int resNum, int tripNum, string name, DateTime checkIn, DateTime checkOut) {
            this.resNum = resNum;
            this.tripNum = tripNum;
            this.name = name;
            this.checkIn = checkIn;
            this.checkOut = checkOut;
        }
    }
}
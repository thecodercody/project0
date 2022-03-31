using System;

namespace model {
    public class Flight {
        //properties
        public int flightNum { get; set; }
        public DateTime depTime { get; set; }
        public DateTime arrTime { get; set; }
        public string airline { get; set; }
        public string depCity { get; set; }
        public string arrCity { get; set; }
        public int tripNum { get; set; }

        //constructors
        public Flight() {
            
        }
        public Flight(int flightNum, DateTime depTime, DateTime arrTime, string airline, string depCity, string arrCity, int tripNum) {
            this.flightNum = flightNum;
            this.depTime = depTime;
            this.arrTime = arrTime;
            this.airline = airline;
            this.depCity = depCity;
            this.arrCity = arrCity;
            this.tripNum = tripNum;
        }
    }
}
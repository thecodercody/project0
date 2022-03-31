using System;
using System.Collections;
using System.Collections.Generic;
using Itenso.TimePeriod;

namespace model {
    public class Trip {
        //properties        
        public int tripNum { get; set; } 
        public int calNum { get; set; }
        public string tripName { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public TimePeriodCollection timeline { get; set; } = new TimePeriodCollection();
        public List<Flight> flights { get; set; } = new List<Flight>();
        public List<Hotel> hotels { get; set; } = new List<Hotel>();
        
        
        //constructors
        public Trip() { }
        public Trip(int calNum, string tripName, DateTime start, DateTime end) {
            this.calNum = calNum;
            this.start = start;
            this.end = end;
            this.tripName = tripName;
        }

         public Trip(int calNum, string tripName, DateTime start, DateTime end, int tripNum) {
            this.calNum = calNum;
            this.start = start;
            this.end = end;
            this.tripName = tripName;
            this.tripNum = tripNum;
        }

        //methods
        public void add(Flight flight) {
            //add the flight timerange to the trip timeline
            TimeRange timeRange = new TimeRange(flight.depTime, flight.arrTime);
            timeline.Add(timeRange);
        }

        public void add(Hotel hotel) {
            //add the hotel timerange to the trip timeline
            TimeRange timeRange = new TimeRange(hotel.checkIn, hotel.checkOut);
            timeline.Add(timeRange);
        }

        public void display() {
                
                System.Console.WriteLine();
        }

        //method to set the trip start to the start of the timeline, which can contain multiple events
        public void tripStart() {
            start = timeline.Start;
        }

        //method to set the trip end to the start of the timeline, which can contain multiple events
        public void tripEnd() {
            end = timeline.End;
        }


    }
}
using System;
using System.Collections.Generic;

namespace model {
    public class UserCalendar {
        //properties
        public int calNum { get; set; }
        public string userEmail { get; set; }
        public List<Trip> calendar = new List<Trip>();

        //constructors
        public UserCalendar() {}
        public UserCalendar(string userEmail) {
            this.userEmail = userEmail;
        }

        public UserCalendar(string userEmail, int calNum) {
            this.userEmail = userEmail;
            this.calNum = calNum;
        }

        //methods
        // public void add(Trip trip) {
        //     calendar.Add(trip);
        // }
    }
}
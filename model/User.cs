using System;

namespace model {
    public class User {
        //properties
        public string email { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int calNum { get; set; }

        //constructors
        public User() {}
        public User(string email, string password, string firstName, string lastName, int calNum) {
            this.email = email;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.calNum = calNum;
        }
    }
}
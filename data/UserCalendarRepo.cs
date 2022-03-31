using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using model;

namespace data {

    public class UserCalendarRepo {
        
        private string _connectionString;

        public UserCalendarRepo(string connectionString) {
            _connectionString = connectionString;
        }

        //get all calendars
        public List<UserCalendar> getUserCalendars() {
            List<UserCalendar> calendars = new List<UserCalendar>();

            using(SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                string query = "SELECT * FROM calendars";
                using(SqlCommand cmd = new SqlCommand(query, connection)) {
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                        if(reader.HasRows) {
                            while(reader.Read()) {
                                string userEmail = reader.GetString(0);
                                int calNum = reader.GetInt32(1);
                                
                                UserCalendar readUserCalendar = new UserCalendar(userEmail, calNum);
                                calendars.Add(readUserCalendar);
                            }
                        }
                    }
                }
                connection.Close();
            }
            return calendars;
        }
        

        //get user calendar
        public UserCalendar getUserCalendar(User user) {
            UserCalendar calendar = new UserCalendar();

            using(SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                string query = "SELECT * FROM calendars WHERE userEmail = '" + user.email + "'";
                using(SqlCommand cmd = new SqlCommand(query, connection)) {
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                        if(reader.HasRows) {
                            while(reader.Read()) {
                                string userEmail = reader.GetString(0);
                                int calNum = reader.GetInt32(1);
                                
                                calendar.userEmail = userEmail;
                                calendar.calNum = calNum; 
                                
                            }
                        }
                    }
                }
                connection.Close();
            }
            return calendar;
        }

        public void createUserCalendar(UserCalendar calendar) {
            using(SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();

                using(SqlCommand sqlCmd = new SqlCommand("insert into calendars VALUES (@userEmail)", connection)) {
                    
                    // SqlParameter calNumParam = new SqlParameter("@calNum", calendar.calNum);
                    // sqlCmd.Parameters.Add(calNumParam);

                    SqlParameter userEmailParam = new SqlParameter("@userEmail", calendar.userEmail);
                    sqlCmd.Parameters.Add(userEmailParam);

                    sqlCmd.ExecuteNonQuery();
                }
                connection.Close();
            }                     
        }
    }
}
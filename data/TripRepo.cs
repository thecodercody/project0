using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using model;

namespace data {

    public class TripRepo {

        private string _connectionString;

        public TripRepo(string connectionString) {
            _connectionString = connectionString;
        }

        public List<Trip> getTrips() {
            List<Trip> trips = new List<Trip>();

            using(SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                string query = "SELECT * FROM trips";
                using(SqlCommand cmd = new SqlCommand(query, connection)) {
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                        if(reader.HasRows) {
                            while(reader.Read()) {
                                int calNum = reader.GetInt32(0);
                                DateTime tripStart = reader.GetDateTime(1);
                                DateTime tripEnd = reader.GetDateTime(2);
                                string tripName = reader.GetString(3);
                                int tripNum = reader.GetInt32(4);

                                Trip readTrip = new Trip(calNum, tripName, tripStart, tripEnd, tripNum);
                                trips.Add(readTrip);
                            }
                        }
                    }
                }
                connection.Close();
            }
            return trips;
        }

        public void createTrip(Trip trip) {
            using(SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();

                using(SqlCommand sqlCmd = new SqlCommand("insert into trips (calNum, tripStart, tripEnd, tripName) values (@calNum, @start, @end, @tripName)", connection)) {

                    SqlParameter calNumParam = new SqlParameter("@calNum", trip.calNum);
                    sqlCmd.Parameters.Add(calNumParam);

                    SqlParameter startParam = new SqlParameter("@start", trip.start);
                    sqlCmd.Parameters.Add(startParam);

                    SqlParameter endParam = new SqlParameter("@end", trip.end);
                    sqlCmd.Parameters.Add(endParam);

                    SqlParameter tripNameParam = new SqlParameter("@tripName", trip.tripName);
                    sqlCmd.Parameters.Add(tripNameParam);

                    sqlCmd.ExecuteNonQuery();
                }
                connection.Close();
            }                     
        }

        //delete trip
        public void deleteTrip(int tripNum)
            {
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string selectQry = "SELECT * FROM trips";
                    using(SqlCommand cmd = new SqlCommand(selectQry, connection))
                    {
                        DataSet trips = new DataSet();

                        SqlDataAdapter tripAdapter = new SqlDataAdapter(cmd);

                        tripAdapter.Fill(trips, "trips");

                        DataTable tripTable = trips.Tables["trips"];

                        foreach(DataRow row in tripTable.Rows) {
                            if((int)row["tripNum"] == tripNum) {
                                row.Delete();
                            }
                        }

                        SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(tripAdapter);
                        tripAdapter.InsertCommand = cmdBuilder.GetInsertCommand();

                        tripAdapter.Update(tripTable);
                    }
                }
            }
        
        //update trip
        public void updateTrip(Trip trip)
            {
                using(SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();

                string query = "update trips set tripStart = @tripStart, tripEnd = @tripEnd where tripNum = @tripNum";
                using(SqlCommand cmd = new SqlCommand(query, connection)) {

                    cmd.Parameters.AddWithValue("@tripStart", trip.start);

                    cmd.Parameters.AddWithValue("@tripEnd", trip.end);

                    cmd.Parameters.AddWithValue("@tripNum", trip.tripNum);

                    cmd.ExecuteNonQuery();
                }
                }                     
            }
    }
}
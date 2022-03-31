using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using model;

namespace data {

    public class FlightRepo {
        
        private string _connectionString;

        public FlightRepo(string connectionString) {
            _connectionString = connectionString;
        }

        public List<Flight> getFlights(int trip) {
            List<Flight> flights = new List<Flight>();

            using(SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                string query = "SELECT * FROM flights WHERE tripNum = " + trip;
                using(SqlCommand cmd = new SqlCommand(query, connection)) {
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                        if(reader.HasRows) {
                            while(reader.Read()) {
                                int flightNum = reader.GetInt32(0);
                                DateTime depTime = reader.GetDateTime(1);
                                DateTime arrTime = reader.GetDateTime(2);
                                string airline = reader.GetString(3);
                                string depCity = reader.GetString(4);
                                string arrCity = reader.GetString(5);
                                int tripNum = reader.GetInt32(6);

                                Flight readFlight = new Flight(flightNum, depTime, arrTime, airline, depCity, arrCity, tripNum);
                                flights.Add(readFlight);
                            }
                        }
                    }
                }
                connection.Close();
            }
            return flights;
        }

        public void createFlight(Flight flight) {
            using(SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();

                using(SqlCommand sqlCmd = new SqlCommand("insert into flights (flightNum, depTime, arrTime, airline, depCity, arrCity, tripNum) VALUES (@flightNum, @depTime, @arrTime, @airline, @depCity, @arrCity, @tripNum)", connection)) {
                    
                    SqlParameter flightNumParam = new SqlParameter("@flightNum", flight.flightNum);
                    sqlCmd.Parameters.Add(flightNumParam);

                    SqlParameter depTimeParam = new SqlParameter("@depTime", flight.depTime);
                    sqlCmd.Parameters.Add(depTimeParam);

                    SqlParameter arrTimeParam = new SqlParameter("@arrTime", flight.arrTime);
                    sqlCmd.Parameters.Add(arrTimeParam);

                    SqlParameter airlineParam = new SqlParameter("@airline", flight.airline);
                    sqlCmd.Parameters.Add(airlineParam);

                    SqlParameter arrCityParam = new SqlParameter("@arrCity", flight.arrCity);
                    sqlCmd.Parameters.Add(arrCityParam);

                    SqlParameter depCityParam = new SqlParameter("@depCity", flight.depCity);
                    sqlCmd.Parameters.Add(depCityParam);

                    SqlParameter calNumParam = new SqlParameter("@tripNum", flight.tripNum);
                    sqlCmd.Parameters.Add(calNumParam);

                    sqlCmd.ExecuteNonQuery();
                }
                connection.Close();
            }                     
        }

        //updates a user's calendar
        public void updateTripNum(Flight flight) {
            using(SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();

                using(SqlCommand sqlCmd = new SqlCommand("update flights set tripNum = @tripNum where tripStart = @tripStart", connection)) {
                    
                    SqlParameter calNumParam = new SqlParameter("@tripNum", flight.tripNum);
                    sqlCmd.Parameters.Add(calNumParam);
                    
                    SqlParameter emailParam = new SqlParameter("@tripStart", flight.depTime);
                    sqlCmd.Parameters.Add(emailParam);

                    sqlCmd.ExecuteNonQuery();
                }
                connection.Close();
            }                     
        }

        //update flight
        public void updateFlight(Flight flight)
            {
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string selectQry = "SELECT * FROM flights";
                    using(SqlCommand cmd = new SqlCommand(selectQry, connection))
                    {
                        DataSet flights = new DataSet();

                        SqlDataAdapter flightAdapter = new SqlDataAdapter(cmd);

                        flightAdapter.Fill(flights, "flights");

                        DataTable flightTable = flights.Tables["flights"];

                        DataRow newRow = flightTable.NewRow();
                        newRow["flightNum"] = flight.flightNum;
                        newRow["depTime"] = flight.depTime;
                        newRow["arrTime"] = flight.arrTime;
                        newRow["airline"] = flight.airline;
                        newRow["depCity"] = flight.depCity;
                        newRow["arrCity"] = flight.arrCity;
                        newRow["tripNum"] = flight.tripNum;

                        flightTable.Rows.Add(newRow);

                        SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(flightAdapter);
                        flightAdapter.InsertCommand = cmdBuilder.GetInsertCommand();

                        flightAdapter.Update(flightTable);
                    }
                }
            }

        //delete flight
        public void deleteFlight(int flightNum)
            {
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string selectQry = "SELECT * FROM flights";
                    using(SqlCommand cmd = new SqlCommand(selectQry, connection))
                    {
                        DataSet flights = new DataSet();

                        SqlDataAdapter flightAdapter = new SqlDataAdapter(cmd);

                        flightAdapter.Fill(flights, "flights");

                        DataTable flightTable = flights.Tables["flights"];

                        foreach(DataRow row in flightTable.Rows) {
                            if((int)row["flightNum"] == flightNum) {
                                row.Delete();
                            }
                        }

                        SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(flightAdapter);
                        flightAdapter.InsertCommand = cmdBuilder.GetInsertCommand();

                        flightAdapter.Update(flightTable);
                    }
                }
            }
    }
}
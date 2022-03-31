using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using model;

namespace data {

    public class HotelRepo {

        private string _connectionString;

        public HotelRepo(string connectionString) {
            _connectionString = connectionString;
        }

        public List<Hotel> getHotels(int trip) {
            List<Hotel> hotels = new List<Hotel>();

            using(SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                string query = "SELECT * FROM hotels WHERE tripNum = " + trip;
                using(SqlCommand cmd = new SqlCommand(query, connection)) {
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                        if(reader.HasRows) {
                            while(reader.Read()) {
                                int hotelNum = reader.GetInt32(0);
                                int tripNum = reader.GetInt32(1);
                                DateTime checkIn = reader.GetDateTime(2);
                                DateTime checkOut = reader.GetDateTime(3);
                                string name = reader.GetString(4);

                                Hotel readHotel = new Hotel(hotelNum, tripNum, name, checkIn, checkOut);
                                hotels.Add(readHotel);
                            }
                        }
                    }
                }
                connection.Close();
            }
            return hotels;
        }

        public void createHotel(Hotel hotel) {
            using(SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();

                using(SqlCommand sqlCmd = new SqlCommand("insert into hotels (resNum, tripNum, checkIn, checkOut, name) VALUES (@resNum, @tripNum, @checkIn, @checkOut, @name)", connection)) {
                    
                    SqlParameter resNumParam = new SqlParameter("@HotelNum", hotel.resNum);
                    sqlCmd.Parameters.Add(resNumParam);

                    SqlParameter tripNumParam = new SqlParameter("@tripNum", hotel.tripNum);
                    sqlCmd.Parameters.Add(tripNumParam);
                    
                    SqlParameter checkInParam = new SqlParameter("@checkIn", hotel.checkIn);
                    sqlCmd.Parameters.Add(checkInParam);

                    SqlParameter checkOutParam = new SqlParameter("@checkOut", hotel.checkOut);
                    sqlCmd.Parameters.Add(checkOutParam);

                    SqlParameter nameParam = new SqlParameter("@name", hotel.checkOut);
                    sqlCmd.Parameters.Add(nameParam);

                    sqlCmd.ExecuteNonQuery();
                }
                connection.Close();
            }                     
        }
    }
}
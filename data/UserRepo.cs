using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using model;

namespace data {

    public class UserRepo {
        
        private string _connectionString;

        public UserRepo(string connectionString) {
            _connectionString = connectionString;
        }

        //gathers list of all users from the database
        public List<User> getUsers() {
            List<User> users = new List<User>();

            using(SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                string query = "SELECT * FROM users";
                using(SqlCommand cmd = new SqlCommand(query, connection)) {
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                        if(reader.HasRows) {
                            while(reader.Read()) {
                                string email = reader.GetString(0);
                                string password = reader.GetString(1);
                                string firstName = reader.GetString(2);
                                string lastName = reader.GetString(3);
                                int calNum = reader.GetInt32(4);

                                User readUser = new User(email, password, firstName, lastName, calNum);
                                users.Add(readUser);
                            }
                        }
                    }
                }
                connection.Close();
            }
            return users;
        }

        // //get an individual User
        // public List<User> getUser(string userEmail) {
        //     List<User> user = new List<User>();
        //     using(SqlConnection connection = new SqlConnection(_connectionString)) {
        //         connection.Open();
        //         string query = "SELECT * FROM users;
        //         using(SqlCommand cmd = new SqlCommand(query, connection)) {
        //             using(SqlDataReader reader = cmd.ExecuteReader()) {
        //                 if(reader.HasRows) {
        //                     while(reader.Read()) {
        //                         string email = reader.GetString(0);
        //                         string password = reader.GetString(1);
        //                         string firstName = reader.GetString(2);
        //                         string lastName = reader.GetString(3);

        //                         User newUser = new User(email, password, firstName, lastName);
        //                         user.Add(newUser);
        //                     }
        //                 }
        //             }
        //         }
        //         connection.Close();
        //     }
        //     return user;
        // }

        //creates a new user in the database
        public void createUser(User user) {
            using(SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();

                using(SqlCommand sqlCmd = new SqlCommand("insert into users (email, password, firstName, lastName) VALUES (@email, @password, @firstName, @lastName)", connection)) {
                    
                    SqlParameter emailParam = new SqlParameter("@email", user.email);
                    sqlCmd.Parameters.Add(emailParam);

                    SqlParameter passwordParam = new SqlParameter("@password", user.password);
                    sqlCmd.Parameters.Add(passwordParam);
                    
                    SqlParameter firstNameParam = new SqlParameter("@firstName", user.firstName);
                    sqlCmd.Parameters.Add(firstNameParam);

                    SqlParameter lastNameParam = new SqlParameter("@lastName", user.lastName);
                    sqlCmd.Parameters.Add(lastNameParam);

                    sqlCmd.ExecuteNonQuery();
                }
                connection.Close();
            }                     
        }


        //updates a user's calendar
        public void updateCalendar(User user) {
            using(SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();

                using(SqlCommand sqlCmd = new SqlCommand("update users set calNum = @calNum where email = @email", connection)) {
                    
                    SqlParameter calNumParam = new SqlParameter("@calNum", user.calNum);
                    sqlCmd.Parameters.Add(calNumParam);
                    
                    SqlParameter emailParam = new SqlParameter("@email", user.email);
                    sqlCmd.Parameters.Add(emailParam);

                    sqlCmd.ExecuteNonQuery();
                }
                connection.Close();
            }                     
        }

        //checks if user already exists in the database
        public bool check(string email) {
            List<User> userList = getUsers();
            foreach(User user in userList) {
                if(user.email == email)
                    return false;
            }
            return true;
        }

        //checks is password for user is correct
        public bool passCheck(string user, string tryPass) {
            string password = "";
            using(SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                string query = "SELECT password FROM users WHERE email = '" + user + "'";
                using(SqlCommand cmd = new SqlCommand(query, connection)) {
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                        if(reader.HasRows) {
                            while(reader.Read()) {
                                password = reader.GetString(0);
                            }
                        }
                    }
                }
                connection.Close();
            }
            if(password == tryPass)
                return true;
            return false;
        }
        
    
    }
}
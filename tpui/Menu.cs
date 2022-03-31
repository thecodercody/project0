using System;

namespace tpui {
    class Menu {
        
        //create single menu object for program
        private static Menu menu = new Menu();

        //ensure the constructor is never invoked
        private Menu() {}

        //provide access to the menu object
        public static Menu getMenu() {
            return menu;
        }

        //show register or login menu
        public void registerOrLogin() {
            Console.ResetColor();
            System.Console.WriteLine();
            System.Console.WriteLine("1. Register a new user");
            System.Console.WriteLine("2. Log in to an existing user account");
            System.Console.WriteLine();
            System.Console.Write("Please choose an option: ");     
        }

        #region user registration
        //prompt user for email for registering
        public void registerEmail() {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine();
            System.Console.WriteLine("REGISTER NEW USER");
            Console.ResetColor();
            System.Console.Write("Enter a valid email address: "); 
        }

        //prompt user for password for registering
        public void registerPasswordIntl() {
            Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine();
                System.Console.WriteLine("REGISTER NEW USER");
                Console.ResetColor();
                System.Console.Write("Enter a password: "); 
        }

        //prompt user for password confirmation for registering
        public void registerPasswordConf() {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine();
            System.Console.WriteLine("REGISTER NEW USER");
            Console.ResetColor();
            System.Console.Write("Enter the password again: "); 
        }

        //prompt user for first name for registering
        public void registerFirstName() {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine();
            System.Console.WriteLine("REGISTER NEW USER");
            Console.ResetColor();
            System.Console.Write("Enter your first name: "); 
        }

        //prompt user for last name for registering
        public void registerLastName() {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine();
            System.Console.WriteLine("REGISTER NEW USER");
            Console.ResetColor();
            System.Console.Write("Enter your last name: "); 
        }
        #endregion

        #region user login
        //prompt user for email for login
        public void emailLogin() {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine();
            System.Console.WriteLine("LOG IN EXISTING USER");
            Console.ResetColor();
            System.Console.Write("Enter your registered user email address: "); 
        }

        //prompt user for password for login
        public void passwordLogin() {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine();
            System.Console.WriteLine("LOG IN EXISTING USER");
            Console.ResetColor();
            System.Console.Write("Enter your password: "); 
        }
        #endregion
        //show menu
        public void showMain() {
                Console.ResetColor();
                System.Console.WriteLine();
                System.Console.WriteLine("1. View calendar of all planned trips");
                System.Console.WriteLine("2. Modify or delete a planned trip");
                System.Console.WriteLine("3. Plan a new trip");
                System.Console.WriteLine();
                System.Console.Write("Please choose an option: ");        
        }    

        //show option 1: display calendar
            //this is implemented in Program.cs

        //show option 2: modify or delete trip
        public void showModOrDel() {
            Console.ResetColor();
            System.Console.WriteLine();
            System.Console.WriteLine("1. Modify");
            System.Console.WriteLine("2. Delete");
            System.Console.WriteLine();
            Console.Write("Please choose an option: ");
        }


        #region show option 3: create new trip

        //ask if user will be flying or not
        public void flightOrNot() {
            System.Console.WriteLine();
            System.Console.Write("Add a flight? (Y or N): ");
        }

            #region prompt for flight information
            //flight departure date
            public void showAddFlightDepDate() {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine();
                System.Console.WriteLine("ADD FLIGHT INFORMATION");
                Console.ResetColor();
                System.Console.Write("Enter departure date in mm-dd-yyyy format: ");   
            }

            //flight departure time
            public void showAddFlightDepTime() {
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine();
                System.Console.WriteLine("ADD FLIGHT INFORMATION");
                Console.ResetColor();
                System.Console.Write("Enter departure time in hh:mm (24-hour clock) format: ");
            }

            //flight arrival date
            public void showAddFlightArrDate() {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine();
                System.Console.WriteLine("ADD FLIGHT INFORMATION");
                Console.ResetColor();
                System.Console.Write("Enter arrival date in mm-dd-yyyy format: ");   
            }

            //flight arrival time
            public void showAddFlightArrTime() {
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine();
                System.Console.WriteLine("ADD FLIGHT INFORMATION");
                Console.ResetColor();
                System.Console.Write("Enter arrival time in hh:mm (24-hour clock) format: ");
            }

            //flight number
            public void showAddFlightNumber() {
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine();
                System.Console.WriteLine("ADD FLIGHT INFORMATION");
                Console.ResetColor();
                System.Console.Write("Enter flight number: ");
            }

            //airline name
            public void showAddAirline() {
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine();
                System.Console.WriteLine("ADD FLIGHT INFORMATION");
                Console.ResetColor();
                System.Console.Write("Enter name of airline (optional): ");
            }

            //departure city
            public void showAddDepCity() {
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine();
                System.Console.WriteLine("ADD FLIGHT INFORMATION");
                Console.ResetColor();
                System.Console.Write("Enter name of departure city (optional): ");
            }

            //arrival city
            public void showAddArrCity() {    
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine();
                System.Console.WriteLine("ADD FLIGHT INFORMATION");
                Console.ResetColor();
                System.Console.Write("Enter name of arrival city (optional): ");
            }

        #endregion

        //ask if user will be staying in a hotel or not
        public void hotelOrNot() {
            System.Console.WriteLine();
            System.Console.Write("Add a hotel? (Y or N): ");
        }

            #region prompt for hotel information
            //add reservation number
            public void showAddResNum() {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine();
                System.Console.WriteLine("ADD HOTEL INFORMATION");
                Console.ResetColor();
                System.Console.Write("Enter hotel reservation number: ");   
            }

            //add hotel name
            public void showAddHotelName() {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine();
                System.Console.WriteLine("ADD HOTEL INFORMATION");
                Console.ResetColor();
                System.Console.Write("Enter hotel name: ");   
            }

            //add check-in date
            public void showAddCheckInDate() {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine();
                System.Console.WriteLine("ADD HOTEL INFORMATION");
                Console.ResetColor();
                System.Console.Write("Enter check-in date in mm-dd-yyyy format: ");   
            }

            //add check-in time
            public void showAddCheckInTime() {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine();
                System.Console.WriteLine("ADD HOTEL INFORMATION");
                Console.ResetColor();
                System.Console.Write("Enter check-in time in hh:mm (24-hour clock) format: ");   
            }

            //add check-out date
            public void showAddCheckOutDate() {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine();
                System.Console.WriteLine("ADD HOTEL INFORMATION");
                Console.ResetColor();
                System.Console.Write("Enter check-out date in mm-dd-yyyy format: ");   
            }

            //add check-out time
            public void showAddCheckOutTime() {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine();
                System.Console.WriteLine("ADD HOTEL INFORMATION");
                Console.ResetColor();
                System.Console.Write("Enter check-out time in hh:mm (24-hour clock) format: ");   
            }
            #endregion
        #endregion
    }
}
using System;
using System.Globalization;
using System.Collections.Generic;
using Itenso.TimePeriod;
using model;
using data;
using tpui;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        #region initialize the program
        //retrieve the check object
        Check check = Check.getCheck();
        
        //retrieve the header object
        Header header = Header.getHeader();

        //retrieve the menu object
        Menu menu = Menu.getMenu();

        //get connection string
        string connectionString = File.ReadAllText("./connectionString.txt");

        //retrieve the repo objects
        UserRepo userRepo = new UserRepo(connectionString);
        UserCalendarRepo calendarRepo = new UserCalendarRepo(connectionString);
        TripRepo tripRepo = new TripRepo(connectionString);
        FlightRepo flightRepo = new FlightRepo(connectionString);
        HotelRepo hotelRepo = new HotelRepo(connectionString);

        //create a new global user object
        User user = new User();
        //retrieve the users list object
        List<User> users;
        //List<User> users = userRepo.getUsers();
        
        //calendar object to store user's calendar
        UserCalendar calendar = new UserCalendar();
        
        #endregion

        #region register or login menu and prompt for choice
        //show header
        header.show();
        //maintain proper spacing (this space can be replaced with invalid input message)
        System.Console.WriteLine();
        //show the menu
        menu.registerOrLogin();

        //user choice variable
        int registerOrLoginOption;
        //checking for validity and repeating prompts if user input is invalid
        while(!Int32.TryParse(Console.ReadLine(), out registerOrLoginOption) || !check.registerOrLogin(registerOrLoginOption)) {              
                header.show();
                check.show();
                menu.registerOrLogin();
        }
        #endregion
        
        switch(registerOrLoginOption) {
            case 1:
                //register new user
                #region enter user email address
                //show header
                header.show();
                //maintain proper spacing (this space can be replaced with invalid input message)
                System.Console.WriteLine();
                //prompt user for email address
                menu.registerEmail();

                //string for user email input
                string email = Console.ReadLine();
                //checking for email format validity and repeating prompts if user input is invalid
                while(!check.email(email)) {
                    header.show();
                    check.show();
                    menu.registerEmail();
                    email = Console.ReadLine();
                }

                //checking if email already exists in database and prompting again if so
                while(!userRepo.check(email)) {
                    header.show();
                    check.userExists();
                    menu.registerEmail();
                    email = Console.ReadLine();
                }
                #endregion
                
                #region enter password
                //password string to store
                string password = "";

                //loop for password initial input and verification matching
                bool passMatch = false;
                while(passMatch == false) {
                    #region enter user password initial
                    //show header
                    header.show();
                    //maintain proper spacing (this space can be replaced with invalid input message)
                    System.Console.WriteLine();
                    //prompt user to enter password
                    menu.registerPasswordIntl();
                    
                    //variable for password input
                    var pass = string.Empty;
                    //password masking
                    ConsoleKey key;
                    do
                    {
                        var keyInfo = Console.ReadKey(intercept: true);
                        key = keyInfo.Key;

                        if (key == ConsoleKey.Backspace && pass.Length > 0)
                        {
                            Console.Write("\b \b");
                            pass = pass[0..^1];
                        }
                        else if (!char.IsControl(keyInfo.KeyChar))
                        {
                            Console.Write("*");
                            pass += keyInfo.KeyChar;
                        }
                    } while (key != ConsoleKey.Enter);
                    #endregion
                    #region enter user password confirmation
                    //show header
                    header.show();
                    //maintain proper spacing (this space can be replaced with invalid input message)
                    System.Console.WriteLine();
                    //prompt user to enter password
                    menu.registerPasswordConf();
                    
                    //variable for password input
                    var passConf = string.Empty;
                    //password masking
                    ConsoleKey keyConf;
                    do
                    {
                        var keyInfo = Console.ReadKey(intercept: true);
                        keyConf = keyInfo.Key;

                        if (keyConf == ConsoleKey.Backspace && passConf.Length > 0)
                        {
                            Console.Write("\b \b");
                            passConf = passConf[0..^1];
                        }
                        else if (!char.IsControl(keyInfo.KeyChar))
                        {
                            Console.Write("*");
                            passConf += keyInfo.KeyChar;
                        }
                    } while (keyConf != ConsoleKey.Enter);
                    #endregion
                    
                    //check to see passwords match, and, if so, end the loop
                    if(pass == passConf) {
                        password = passConf;
                        passMatch = true;
                    }
                }
                #endregion

                #region enter user first name
                //show header
                header.show();
                //maintain proper spacing (this space can be replaced with invalid input message)
                System.Console.WriteLine();
                //prompt user to enter first name
                menu.registerFirstName();

                //string to store user first name
                string firstName = Console.ReadLine();
                #endregion
                
                #region enter user last name
                //show header
                header.show();
                //maintain proper spacing (this space can be replaced with invalid input message)
                System.Console.WriteLine();
                //prompt user to enter first name
                menu.registerLastName();

                //string to store user first name
                string lastName = Console.ReadLine();
                #endregion
                
                #region add user and new calendar for user to database
                //populate this session's user object
                user.email = email;
                user.password = password;
                user.firstName = firstName;
                user.lastName = lastName;

                //add the new user to the database
                userRepo.createUser(user);

                //add a calendar for the user to the database
                calendar = new UserCalendar(user.email);
                calendarRepo.createUserCalendar(calendar);
                List<UserCalendar> calendars = calendarRepo.getUserCalendars();
                
                calendar.calNum = (calendars.Find(i => i.userEmail == user.email)).calNum;
                user.calNum = calendar.calNum;
                userRepo.updateCalendar(user);
                #endregion
                break;
            case 2:
                //log in the user
                #region enter user email for login
                //show header
                header.show();
                //maintain proper spacing (this space can be replaced with invalid input message)
                System.Console.WriteLine();
                //prompt user for login email
                menu.emailLogin();

                //store user email input
                string tempEmail = Console.ReadLine();
                
                //checking for validity and repeating prompts if user input is invalid
                while(userRepo.check(tempEmail)) {
                    header.show();
                    check.noUser();
                    menu.emailLogin();
                    tempEmail = Console.ReadLine();
                }
                #endregion

                #region enter user password for login
                //show header
                header.show();
                //maintain proper spacing (this space can be replaced with invalid input message)
                System.Console.WriteLine();
                //prompt user for login password
                menu.passwordLogin();

                //variable for password input
                    var passCheck = string.Empty;
                    //password masking
                    ConsoleKey keyCheck;
                    do
                    {
                        var keyInfo = Console.ReadKey(intercept: true);
                        keyCheck = keyInfo.Key;

                        if (keyCheck == ConsoleKey.Backspace && passCheck.Length > 0)
                        {
                            Console.Write("\b \b");
                            passCheck = passCheck[0..^1];
                        }
                        else if (!char.IsControl(keyInfo.KeyChar))
                        {
                            Console.Write("*");
                            passCheck += keyInfo.KeyChar;
                        }
                    } while (keyCheck != ConsoleKey.Enter);
                
                //checking for validity and repeating prompts if user input is invalid
                while(!userRepo.passCheck(tempEmail, passCheck)) {
                    header.show();
                    check.passIsWrong();
                    menu.passwordLogin();
                    passCheck = Console.ReadLine();
                }

                #endregion
                
                #region retrieve the correct user object
                //retrieve the correct user object 
                users = userRepo.getUsers();
                foreach(User usr in users) {
                    if(usr.email == tempEmail) {
                        user.email = usr.email;
                        user.password = usr.password;
                        user.firstName = usr.firstName;
                        user.lastName = usr.lastName;
                        user.calNum = usr.calNum;
                    }
                }
                #endregion
                break;
        }


        #region show menu & have user selection option
        //show header
        header.show();
        //maintain proper spacing (this space can be replaced with invalid input message)
        System.Console.WriteLine();
        //show the menu
        menu.showMain();

        //user choice variable
        int option;
        //checking for validity and repeating menu if user input is invalid
        while(!Int32.TryParse(Console.ReadLine(), out option) || !check.mainMenu(option)) {              
                header.show();
                check.show();
                menu.showMain();
        }   
        #endregion
        
        //switch statement for selected option
        switch(option) {
            case 1:
                #region display user calendar
                //show header
                header.show();
                //maintain proper spacing (this space can be replaced with invalid input message)
                System.Console.WriteLine();
                //display user calendar
                List<Trip> userTrips = tripRepo.getTrips();
                foreach(Trip userTrip in userTrips) {
                    if(userTrip.calNum == user.calNum) {
                        Console.WriteLine("Trip ID " + userTrip.tripNum + ": " + userTrip.tripName + " - begins " + userTrip.start +
                        " and ends " + userTrip.end);
                    }
                }
                #endregion
                break;

            case 2:
                #region modify or delete trip
                //display calendar and ask user for choice
                //show header
                header.show();
                //maintain proper spacing (this space can be replaced with invalid input message)
                System.Console.WriteLine();
                
                
                //gather list of only current user's trips for selection
                List<Int32> choices = new List<Int32>();
                //display user calendar
                List<Trip> userTripsMod = tripRepo.getTrips();
                foreach(Trip userTrip in userTripsMod) {
                    if(userTrip.calNum == user.calNum) {
                        Console.WriteLine("Trip ID " + userTrip.tripNum + ": " + userTrip.tripName + " - begins " + userTrip.start +
                        " and ends " + userTrip.end);
                        choices.Add(userTrip.tripNum);
                    }
                }

                //prompt user for trip ID to modify or delete
                System.Console.WriteLine();
                System.Console.Write("Enter a Trip ID to modify or delete it: ");

                //user choice variable
                int choice;
                //checking for validity and repeating menu if user input is invalid
                while(!Int32.TryParse(Console.ReadLine(), out choice) || !choices.Contains(choice)) {              
                        header.show();
                        check.show();
                        foreach(Trip userTrip in userTripsMod) {
                            if(userTrip.calNum == user.calNum) {
                                Console.WriteLine("Trip ID " + userTrip.tripNum + ": " + userTrip.tripName + " - begins " + userTrip.start +
                                " and ends " + userTrip.end);
                                choices.Add(userTrip.tripNum);
                            }
                        }

                        //prompt user for trip ID to modify or delete
                        System.Console.WriteLine();
                        System.Console.Write("Enter a Trip ID to modify or delete it: ");
                }

                //asking whether user wants to modify or delete
                
                //show header
                header.show();
                //maintain proper spacing (this space can be replaced with invalid input message)
                System.Console.WriteLine();
                //prompt for 1-modify or 2-delete
                menu.showModOrDel();

                //checking user input for type and choice validity
                int modOrDel;
                while(!Int32.TryParse(Console.ReadLine(), out modOrDel) || !(modOrDel == 1 || modOrDel == 2)) {              
                    header.show();
                    check.show();
                    menu.showModOrDel();
                }

                //switch between modify and delete based on user's choice
                switch(modOrDel) {
                    case 1:
                        break;
                    case 2:
                        #region delete trip
                        //deleting all flights in trip as a precondition for deleting trip from database

                            //list to store all flights in database
                        List<Flight> allFlights = new List<Flight>();
                        foreach(Flight flight in allFlights) {
                            if(flight.tripNum == choice) {
                                flightRepo.deleteFlight(flight.flightNum);
                            }
                        }
                        
                        //deleting trip from database
                        tripRepo.deleteTrip(choice);
                        #endregion
                        break;
                }

                #endregion
                break;

            case 3:
                #region create new trip

                #region initialize a new trip and name it
                //make a new trip
                //List<UserCalendar> calendars = calendarRepo.getUserCalendars();

                Trip trip = new Trip();

                trip.calNum = user.calNum;

                //show header
                header.show();
                //maintain proper spacing (this space can be replaced with invalid input message)
                System.Console.WriteLine();
                //prompt user for trip name
                System.Console.WriteLine();
                System.Console.Write("Enter a name for this trip: ");

                //add trip name to trip object
                trip.tripName = Console.ReadLine();
                #endregion
                
                #region determine whether user will be flying or not
                //show header        
                header.show();
                //maintain proper spacing (this space can be replaced with invalid input message)
                System.Console.WriteLine();
                //ask user if he or she will be flying
                menu.flightOrNot();
                
                //ensuring user answer is valid
                char flightOrNot = Convert.ToChar(Console.ReadLine());
                while(!(flightOrNot == 'Y' || flightOrNot == 'N' || flightOrNot == 'y' || flightOrNot == 'n')) {
                    header.show();
                    check.show();
                    menu.flightOrNot();
                    flightOrNot = Convert.ToChar(Console.ReadLine());
                }                
                #endregion

                //adding flight if user answered yes
                //loop to add as many flights as the user needs or skipping if user won't be flying
                bool addMoreFlights;
                if(flightOrNot == 'N' || flightOrNot == 'n')
                    addMoreFlights = false;
                else
                    addMoreFlights = true;
                
                while(addMoreFlights == true) {
                #region add flight

                    #region add flight departure date
                    //show header        
                    header.show();
                    //maintain proper spacing (this space can be replaced with invalid input message)
                    System.Console.WriteLine();
                    //prompt user for departure date input
                    menu.showAddFlightDepDate();

                    //object for user date input
                    DateTime depDate;
                    //input validity checks
                    while(!DateTime.TryParseExact(Console.ReadLine(), "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out depDate)) {              
                        header.show();
                        check.show();
                        menu.showAddFlightDepDate();
                    }
                    #endregion

                    #region add flight departure time
                    //show header        
                    header.show();
                    //maintain proper spacing (this space can be replaced with invalid input message)
                    System.Console.WriteLine();
                    //prompt user for departure time input
                    menu.showAddFlightDepTime();

                    //object for user time input
                    TimeSpan depTime;
                    while(!TimeSpan.TryParse(Console.ReadLine(), out depTime)) {              
                        header.show();
                        check.show();
                        menu.showAddFlightDepTime();
                    }

                    //combine the departure flight and time
                    depDate = depDate.Date + depTime;
                    #endregion

                    #region add flight arrival date
                    //show header        
                    header.show();
                    //maintain proper spacing (this space can be replaced with invalid input message)
                    System.Console.WriteLine();
                    //prompt user for arrival date input
                    menu.showAddFlightArrDate();

                    //object for user date input
                    DateTime arrDate;
                    //input validity checks
                    while(!DateTime.TryParseExact(Console.ReadLine(), "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out arrDate)) {              
                        header.show();
                        check.show();
                        menu.showAddFlightArrDate();
                    }

                    #endregion

                    #region add flight arrival time
                    //show header        
                    header.show();
                    //maintain proper spacing (this space can be replaced with invalid input message)
                    System.Console.WriteLine();
                    //prompt user for arrival time input
                    menu.showAddFlightArrTime();

                    //object for user time input
                    TimeSpan arrTime;
                    while(!TimeSpan.TryParse(Console.ReadLine(), out arrTime)) {              
                        header.show();
                        check.show();
                        menu.showAddFlightArrTime();
                    }

                    //combine the departure flight and time
                    arrDate = arrDate.Date + arrTime;

                    #endregion
                    
                    #region add flight number
                    //show header        
                    header.show();
                    //maintain proper spacing (this space can be replaced with invalid input message)
                    System.Console.WriteLine();
                    //prompt user for flight number
                    menu.showAddFlightNumber();

                    //int for user flight number input
                    int flightNum;
                    while(!Int32.TryParse(Console.ReadLine(), out flightNum)) {
                        header.show();
                        check.show();
                        menu.showAddFlightNumber();
                    }

                    #endregion

                    #region add airline
                    //show header        
                    header.show();
                    //maintain proper spacing (this space can be replaced with invalid input message)
                    System.Console.WriteLine();
                    //prompt user for airline name
                    menu.showAddAirline();

                    //string for user airline input
                    string airline = Console.ReadLine();

                    #endregion

                    #region add departure city
                    //show header        
                    header.show();
                    //maintain proper spacing (this space can be replaced with invalid input message)
                    System.Console.WriteLine();
                    //prompt user for departure city
                    menu.showAddDepCity();

                    //string for departure city input
                    string depCity = Console.ReadLine();

                    #endregion

                    #region add arrival city
                    //show header        
                    header.show();
                    //maintain proper spacing (this space can be replaced with invalid input message)
                    System.Console.WriteLine();
                    //prompt user for arrival city
                    menu.showAddArrCity();

                    //string for arrival city input
                    string arrCity = Console.ReadLine();
                #endregion

                    
                    //generate object from input
                    Flight newFlight = new Flight();
                    newFlight.flightNum = flightNum;
                    newFlight.depTime = depDate;
                    newFlight.arrTime = arrDate;
                    newFlight.airline = airline;
                    newFlight.depCity = depCity;
                    newFlight.arrCity = arrCity;

                    //save flight in trip
                    trip.add(newFlight);
                    trip.tripStart();
                    trip.tripEnd();

                    //add trip to database if doesn't exist
                    bool tripExists = false;
                    List<Trip> ttrips = tripRepo.getTrips();
                    foreach(Trip ttrip in ttrips) {
                        if(trip.start == ttrip.start) {
                            tripExists = true;
                            newFlight.tripNum = ttrip.tripNum;
                        }
                    }
                    if(!tripExists) {
                        tripRepo.createTrip(trip);
                        //add trip number to the flight
                        List<Trip> trips = tripRepo.getTrips();
                        foreach(Trip tempTrip in trips) {
                            if(newFlight.depTime == trip.start) {
                                newFlight.tripNum = tempTrip.tripNum;
                            }
                        }
                    }

                    //save flight in database
                    flightRepo.createFlight(newFlight);

                    #region add another flight?
                    //show header        
                    header.show();
                    //maintain proper spacing (this space can be replaced with invalid input message)
                    System.Console.WriteLine();
                    //ask user if he or she will be flying
                    menu.flightOrNot();
                    
                    //ensuring user answer is valid
                    flightOrNot = Convert.ToChar(Console.ReadLine());
                    while(!(flightOrNot == 'Y' || flightOrNot == 'N' || flightOrNot == 'y' || flightOrNot == 'n')) {
                        header.show();
                        check.show();
                        menu.flightOrNot();
                        flightOrNot = Convert.ToChar(Console.ReadLine());
                    }        
                    if(flightOrNot == 'N' || flightOrNot == 'n')
                        addMoreFlights = false;
                    #endregion
                #endregion
                }
                
                #region determine whether user will be staying in a hotel or not
                //show header        
                header.show();
                //maintain proper spacing (this space can be replaced with invalid input message)
                System.Console.WriteLine();
                //ask user if he or she will be staying in a hotel
                menu.hotelOrNot();
                
                //ensuring user answer is valid
                char hotelOrNot = Convert.ToChar(Console.ReadLine());
                while(!(hotelOrNot == 'Y' || hotelOrNot == 'N' || hotelOrNot == 'y' || hotelOrNot == 'n')) {
                    header.show();
                    check.show();
                    menu.hotelOrNot();
                    hotelOrNot = Convert.ToChar(Console.ReadLine());
                }                
                #endregion

                //adding hotel if user answered yes
                //loop to add as many hotels as the user needs or skipping if user won't be staying in a hotel
                bool addMoreHotels;
                if(hotelOrNot == 'N' || hotelOrNot == 'n')
                    addMoreHotels = false;
                else
                    addMoreHotels = true;
                
                while(addMoreHotels == true) {
                #region add hotel
                    #region add reservation number
                    //show header        
                    header.show();
                    //maintain proper spacing (this space can be replaced with invalid input message)
                    System.Console.WriteLine();
                    //show add reservation number
                    menu.showAddResNum();

                    //int for user reservation number input
                    int resNum;
                    //input validity checks
                    while(!Int32.TryParse(Console.ReadLine(), out resNum)) {
                        header.show();
                        check.show();
                        menu.showAddResNum();
                    }
                    #endregion

                    #region add hotel name
                    //show header        
                    header.show();
                    //maintain proper spacing (this space can be replaced with invalid input message)
                    System.Console.WriteLine();
                    //show add hotel name
                    menu.showAddHotelName();

                    //string for hotel name input
                    string hotelName = Console.ReadLine();
                    #endregion

                    #region add check-in date
                    //show header        
                    header.show();
                    //maintain proper spacing (this space can be replaced with invalid input message)
                    System.Console.WriteLine();
                    //show add check-in date
                    menu.showAddCheckInDate();

                    //object for user date input
                    DateTime checkInDate;
                    //input validity checks
                    while(!DateTime.TryParseExact(Console.ReadLine(), "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out checkInDate)) {
                        header.show();
                        check.show();
                        menu.showAddCheckInDate();
                    }
                    #endregion

                    #region add check-in time
                    //show header        
                    header.show();
                    //maintain proper spacing (this space can be replaced with invalid input message)
                    System.Console.WriteLine();
                    //prompt user for check-in time
                    menu.showAddCheckInTime();

                    //object for user time input
                    TimeSpan checkInTime;
                    while(!TimeSpan.TryParse(Console.ReadLine(), out checkInTime)) {              
                        header.show();
                        check.show();
                        menu.showAddCheckInTime();
                    }

                    //combine the check-in date and time
                    checkInDate = checkInDate.Date + checkInTime;
                    #endregion
                    
                    #region add check-out date
                    //show header        
                    header.show();
                    //maintain proper spacing (this space can be replaced with invalid input message)
                    System.Console.WriteLine();
                    //show add check-tut date
                    menu.showAddCheckOutDate();

                    //object for user date input
                    DateTime checkOutDate;
                    //input validity checks
                    while(!DateTime.TryParseExact(Console.ReadLine(), "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out checkOutDate)) {
                        header.show();
                        check.show();
                        menu.showAddCheckOutDate();
                    }

                    #endregion

                    #region add check-out time
                    //show header        
                    header.show();
                    //maintain proper spacing (this space can be replaced with invalid input message)
                    System.Console.WriteLine();
                    //prompt user for check-out time
                    menu.showAddCheckOutTime();

                    //object for user time input
                    TimeSpan checkOutTime;
                    while(!TimeSpan.TryParse(Console.ReadLine(), out checkOutTime)) {              
                        header.show();
                        check.show();
                        menu.showAddCheckOutTime();
                    }

                    //combine the check-out date and time
                    checkOutDate = checkOutDate.Date + checkOutTime;

                    #endregion
                
                    //generate object from input
                    Hotel newHotel = new Hotel(resNum, trip.tripNum, hotelName, checkInDate, checkOutDate);
                    //save hotel in database
                    hotelRepo.createHotel(newHotel);
                    //save hotel in trip
                    trip.add(newHotel);
                #endregion

                #region add another hotel?
                //show header        
                header.show();
                //maintain proper spacing (this space can be replaced with invalid input message)
                System.Console.WriteLine();
                //ask user if he or she will be staying in hotel
                menu.hotelOrNot();

                //ensuring user answer is valid
                hotelOrNot = Convert.ToChar(Console.ReadLine());
                while(!(hotelOrNot == 'Y' || hotelOrNot == 'N' || hotelOrNot == 'y' || hotelOrNot == 'n')) {
                    header.show();
                    check.show();
                    menu.hotelOrNot();
                    hotelOrNot = Convert.ToChar(Console.ReadLine());
                }                
                if(hotelOrNot == 'N' || hotelOrNot == 'n')
                    addMoreHotels = false;
                #endregion
                }

                #endregion

                trip.tripStart();


                trip.tripEnd();
               
                
                tripRepo.updateTrip(trip);
                break;
        }  
    }
}


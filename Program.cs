using System;
using Itenso.TimePeriod;

namespace planner
{
    class Program
    {
        static void Main(string[] args)
        {
            #region initialize the program
            
            //create a new schedule
            //Schedule schedule = new Schedule();

            //create new validator object
            Check check = Check.getCheck();
            
            //retrieve the header object
            Header header = Header.getHeader();

            //retrieve the menu object
            Menu menu = Menu.getMenu();

            #endregion

            #region show menu & have user selection option
            
            //welcome the user
            header.show();

            //maintain proper spacing (this space can be replaced with invalid input message)
            System.Console.WriteLine();

            //show the menu
            menu.show();

            //user choice variable
            int option;

            //repeat menu while checking for validity
            while(!Int32.TryParse(Console.ReadLine(), out option) || !check.mainMenu(option)) {              
                    header.show();
                    check.show();
                    menu.show();
            }
                
            #endregion
            
            //outbound flight
            Flight flightout = new Flight();
            flightout.depTime = new DateTime(2021, 7, 11, 10, 30, 0);
            flightout.arrTime = new DateTime(2021, 7, 11, 14, 0, 0);

            //inbound flight
            Flight flightin = new Flight();
            flightin.depTime = new DateTime(2022, 1, 31, 21, 0, 0);
            flightin.arrTime = new DateTime(2022, 2, 1, 8, 0, 0);

            
            // trip.add(flightout);
            // trip.add(flightin);
            
            // foreach(ITimePeriod timePeriod in trip.timeline) {
            //     System.Console.WriteLine("     Item: " + timePeriod);
            // }

            
        }
    }
}

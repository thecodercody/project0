using System;
using System.Text.RegularExpressions;

namespace model {
    public class Check {
        
        //create single check object for program
        private static Check check = new Check();

        //ensure the constructor is never invoked
        private Check() {}

        //provide access to the check object
        public static Check getCheck() {
            return check;
        }

        //show check
        public void show() {
            Console.BackgroundColor = ConsoleColor.Red;
            System.Console.WriteLine("                              INVALID INPUT!                            ");
            Console.ResetColor();
        }

        //show user exists error
        public void userExists() {
            Console.BackgroundColor = ConsoleColor.Red;
            System.Console.WriteLine("               USER ALREADY EXISTS!  USE A DIFFERENT EMAIL             ");
            Console.ResetColor();
        }

        //show invalid password error
        public void passIsWrong() {
            Console.BackgroundColor = ConsoleColor.Red;
            System.Console.WriteLine("                           INCORRECT PASSWORD!                         ");
            Console.ResetColor();
        }

        //show passwords don't match error
        public void passwordsDontMatch() {
            Console.BackgroundColor = ConsoleColor.Red;
            System.Console.WriteLine("                         PASSWORDS DO NOT MATCH!                       ");
            Console.ResetColor();
        }

        //show user doesn't exist error
        public void noUser() {
            Console.BackgroundColor = ConsoleColor.Red;
            System.Console.WriteLine("                           USER DOES NOT EXIST                         ");
            Console.ResetColor();
        }

        //ensure valid option chosen on register or login screen
        public bool registerOrLogin(int option) {
            if(option < 1 || option > 2)
                return false;
            return true;
        }
        
        //ensure valid option chosen on main menu
        public bool mainMenu(int option) {
            if(option < 1 || option > 3)
                return false;
            return true;
        }

        //regex email address check (this ensures the syntax of the email address is correct, but it does not verify that the email address is a working email address)
        public bool email(string input) {
            Regex regex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
            Match match = regex.Match(input);
            return (match.Success) ? true : false;
        }
    }
}
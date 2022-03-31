using System;

namespace tpui {
    class Header {
        
        //create single header object for program
        private static Header header = new Header();

        //ensure the constructor is never invoked
        private Header() {}

        //provide access to the header object
        public static Header getHeader() {
            return header;
        }

        //show header
        public void show() {
            //welcome the user
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            System.Console.WriteLine();
            System.Console.WriteLine("-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~");
            System.Console.WriteLine("                               TRIP PLANNER                             ");
            System.Console.WriteLine("-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~");
            System.Console.WriteLine();
            Console.ResetColor();
        }
    }
}
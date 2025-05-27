using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalOutput
{
    /// <summary>
    /// Class to handle printing ASCII art text in the console.
    /// </summary>
    public class AsciiArt
    {       
        public static void PrintAuthorizationAsciiArt(int posX, int posY)
        {
            string[] asciiStrings = new string[]
            {

                @"    _         _   _                _          _   _             ",
                @"   / \  _   _| |_| |__   ___  _ __(_)______ _| |_(_) ___  _ __  ",
                @"  / _ \| | | | __| '_ \ / _ \| '__| |_  / _` | __| |/ _ \| '_ \ ",
                @" / ___ \ |_| | |_| | | | (_) | |  | |/ / (_| | |_| | (_) | | | |",
                @"/_/   \_\__,_|\__|_| |_|\___/|_|  |_/___\__,_|\__|_|\___/|_| |_|"
            };

            Print(posX, posY, asciiStrings);
           

        }
        
        public static void PrintPOSAsciiArt(int posX, int posY)
        {
            string[] asciiStrings = new string[]
            {
                @" ____   ___  ____  ",
                @"|  _ \ / _ \/ ___| ",
                @"| |_) | | | \___ \ ",
                @"|  __/| |_| |___) |",
                @"|_|    \___/|____/ "
            };

            Print(posX, posY, asciiStrings);
        }

        public static void PrintInventoryAsciiArt(int posX, int posY)
        {
            string[] asciiStrings = new string[]
            {
                @" ___                      _                   ",
                @"|_ _|_ ____   _____ _ __ | |_ ___  _ __ _   _ ",
                @" | || '_ \ \ / / _ \ '_ \| __/ _ \| '__| | | |",
                @" | || | | \ V /  __/ | | | || (_) | |  | |_| |",
                @"|___|_| |_|\_/ \___|_| |_|\__\___/|_|   \__, |",
                @"                                         |___/ "
            };

            Print(posX, posY, asciiStrings);

        }
        
        public static void PrintMyCartAsciiArt(int posX, int posY)
        {
            string[] asciiStrings = new string[]
            {
                @" __  __          ____           _   ",
                @"|  \/  |_   _   / ___|__ _ _ __| |_ ",
                @"| |\/| | | | | | |   / _` | '__| __|",
                @"| |  | | |_| | | |__| (_| | |  | |_ ",
                @"|_|  |_|\__, |  \____\__,_|_|   \__|",
                @"         |___/                       "
            };
            Print(posX, posY, asciiStrings);
        }


        public static void Print(int posX, int posY, string[] asciiStrings)
        {



            for (int i = 0; i < asciiStrings.Length; i++)
            {

                Console.SetCursorPosition(posX, posY);
                Console.Write(asciiStrings[i]);
                posY++;
                Console.SetCursorPosition(posX, posY);

            }
            Console.SetCursorPosition(0, posY);
        }

        public static void PrintAdminAsciiArt(int posX, int posY)
        {
            string[] asciiStrings = new string[]
            {
                @"    _       _           _       ",
                @"   / \   __| |_ __ ___ (_)_ __  ",
                @"  / _ \ / _` | '_ ` _ \| | '_ \ ",
                @" / ___ \ (_| | | | | | | | | | |",
                @"/_/   \_\__,_|_| |_| |_|_|_| |_|"
            };

            Print(posX, posY, asciiStrings);
        }

        public static void PrintCashierAsciiArt(int posX, int posY)
        {

            string[] asciiStrings = new string[]
            {
                @"  ____          _     _           ",
                @" / ___|__ _ ___| |__ (_) ___ _ __ ",
                @"| |   / _` / __| '_ \| |/ _ \ '__|",
                @"| |__| (_| \__ \ | | | |  __/ |   ",
                @" \____\__,_|___/_| |_|_|\___|_|   "
            };

            Print(posX, posY, asciiStrings);
            
        }

        public static void PrintCostumerAsciiArt(int posX, int posY)
        {
            
            string[] asciiStrings = new string[]
            {
                @"  ____          _                             ",
                @" / ___|___  ___| |_ _   _ _ __ ___   ___ _ __ ",
                @"| |   / _ \/ __| __| | | | '_ ` _ \ / _ \ '__|",
                @"| |__| (_) \__ \ |_| |_| | | | | | |  __/ |   ",
                @" \____\___/|___/\__|\__,_|_| |_| |_|\___|_|   "
            };

            Print(posX, posY, asciiStrings);

        }

        public static void PrintExitAsciiArt(int posX, int posY)
        {
            
            string[] asciiStrings = new string[]
            {
                @" _____      _ _   ",
                @"| ____|_  _(_) |_ ",
                @"|  _| \ \/ / | __|",
                @"| |___ >  <| | |_ ",
                @"|_____/_/\_\_|\__|"
            };

            Print(posX, posY, asciiStrings);
        }

        internal static void PrintCheckOutAsciiArt(int posX, int posY)
        {
     

            string[] asciiStrings = new string[]
            {
                @"  ____ _               _               _   ",
                @"/  ___| |__   ___  ___| | _____  _   _| |_ ",
                @"| |   | '_ \ / _ \/ __| |/ / _ \| | | | __|",
                @"| |___| | | |  __/ (__|   < (_) | |_| | |_ ",
                @" \____|_| |_|\___|\___|_|\_\___/ \__,_|\__|"

            };
            Print(posX, posY, asciiStrings);

        }

        internal static void PrintMainMenuAsciiArt(int posX, int posY)
        {
            string[] asciiStrings = new string[]
              {
                     @" __  __    _    ___ _   _   __  __ _____ _   _ _   _",
                     @"|  \/  |  / \  |_ _| \ | | |  \/  | ____| \ | | | | |",
                     @"| |\/| | / _ \  | ||  \| | | |\/| |  _| |  \| | | | |",
                     @"| |  | |/ ___ \ | || |\  | | |  | | |___| |\  | |_| |",
                     @"|_|  |_/_/   \_\___|_| \_| |_|  |_|_____|_| \_|\___/",
              };

            Print(posX, posY, asciiStrings);
        }

        public static void PrintProductManagerAsciiArt(int posX, int posY)
        {
            
            string[] asciiStrings = new string[]
            {
         @" ____       __  __                                   ",
         @"|  _ \     |  \/  | __ _ _ __   __ _  __ _  ___ _ __ ",
         @"| |_) |____| |\/| |/ _` | '_ \ / _` |/ _` |/ _ \ '__|",
         @"|  __/_____| |  | | (_| | | | | (_| | (_| |  __/ |   ",
         @"|_|        |_|  |_|\__,_|_| |_|\__,_|\__, |\___|_|   ",
         @"                                      |___/           "};

            Print(posX, posY, asciiStrings);

        }
    }
}

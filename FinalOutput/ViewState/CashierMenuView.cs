
using FinalOutput.Services;
using FinalOutput.State;
using System;

namespace FinalOutput.ViewState
{
    /// <summary>
    /// Class that displays main menu view for cashier user type.
    /// </summary>
    public class CashierMenuView : MenuView
    {
        /// <summary>
        /// Displays main menu view in the console.
        /// </summary>
        public override void Display()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.CursorVisible = false;
            DisplayTitleAndOptions();

            var input = Console.ReadKey(true);
            var accountService = new CostumerAccountGetService();

            //Logic for handling user key input
            switch (input.Key)
            {
                case ConsoleKey.D1: // [1] POS
                    Menu.SetDisplayView(new POSView());
                    break;
                case ConsoleKey.D2: // [2] Inventory
                    Menu.SetDisplayView(new InventoryMenuViewCashier(accountService));
                    break;

                case ConsoleKey.D3: // [3] Product Management
                    Menu.SetDisplayView(new ProductManagerView(accountService));
                    break;
                case ConsoleKey.D4: // [4] Exit
                    Menu.SetDisplayView(new AuthorizationView());
                    break;
                default:

                    break;
            }
        }
        /// <summary>
        /// Displays title and menu options in the console.
        /// </summary>
        private void DisplayTitleAndOptions()
        {
            // Initialization
            Console.Clear();
            Console.WindowWidth = AppData.width;
            Console.WindowHeight = AppData.height;
            string[] displayOptions = new string[] { "[1] POS", "[2] Inventory", "[3] Product Management", "[4] Exit" };
            AsciiArt.PrintMainMenuAsciiArt(AppData.asciiHeaderX, AppData.asciiHeaderY);

            int posX = 36;
            int posY = 14;

            // Output
            for (int i = 0; i < displayOptions.Length; i++)
            {
                Console.SetCursorPosition(posX, posY);
                Console.Write(displayOptions[i]);
                posY++;
                Console.SetCursorPosition(posX, posY);

            }
        }
    }
}

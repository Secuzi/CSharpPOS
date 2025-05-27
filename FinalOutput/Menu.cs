using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media.Media3D;
using FinalOutput.ViewState;
using FinalOutput.Models;
using FinalOutput.PageHandling;
using FinalOutput.State;
using FinalOutput.Services;

namespace FinalOutput
{
    /// <summary>
    /// Class that holds the default configuration values of the application.
    /// </summary>
    internal static class AppData
    {
        public const int PriceXLoc = 37;
        public const int QuantityXLoc = 57;        
        public static IEnumerable<Account> accounts;
        public static List<Product> mainProducts;
        public const int width = 78;
        public const int height = 44;
        public const int TotalPerProductXLoc = 77; 
        public const int asciiHeaderX = 17;
        public const int asciiHeaderY = 2;
        public const int posX = 15;
        public const int posY = 40;
        public const string productFolderName = @"StockFile";
    }
    /// <summary>
    /// Manages view page navigation and display in the console.
    /// </summary>
    public class Menu
    {        
        private static MenuView CurrentView;
        private static Stack<MenuView> ViewHistory;
        private static bool IsDisplaying = true;
        public Menu()
        {
            CurrentView = new AuthorizationView();
            ViewHistory = new Stack<MenuView>(5);
            ViewHistory.Push(CurrentView);
        }
        /// <summary>
        /// Displays the current menu view in the console.
        /// </summary>
        public void Display()
        {
            SetDisplayOn();
            while (IsDisplaying)
            {
                CurrentView.Display();
            }
        }
        /// <summary>
        /// Sets the current menu view.
        /// </summary>
        /// <param name="menuView">The menu view to overwrite the current view.</param>
        public static void SetDisplayView(MenuView menuView)
        {
            ViewHistory.Push(CurrentView);
            CurrentView = menuView;
        }
        /// <summary>
        /// Sets the previous menu view as the current menu view.
        /// </summary>
        public static void SetPreviousView()
        {
            if(ViewHistory.Count > 0)
                CurrentView = ViewHistory.Pop();
        }
        /// <summary>
        /// Sets the menu view to be displayable in the console.
        /// </summary>
        public static void SetDisplayOn()
        {
            IsDisplaying = true;
        }
        /// <summary>
        /// Sets the menu view to be undisplayed in the console.
        /// </summary>
        public static void SetDisplayOff()
        {
            IsDisplaying = false;
        }
        /// <summary>
        /// Displays products in the console in tabular format.
        /// </summary>
        /// <param name="pageStack">the current page to be displayed.</param>
        /// <param name="productsParameter">the products to be displayed.</param>
        /// <param name="menuItems">the menu selection options to be displayed to the user.</param>
        public static void DisplayProducts(Stack<int> pageStack, IEnumerable<Product> productsParameter, string[] menuItems)
        {

            // Initialization
            #region
            /*
            int leftCoordTable = 10;
            int topCoordTable = 1;

            int leftProductItem = 11;
            int topProductItem = 3;

            int leftMenuOptions = 10;
            int topMenuOptions = 26;

            ---
            int leftProductItem = 16;
            int topProductItem = 9;
            ---
             * 
             */

            #endregion
            int leftCoordTable = 15;
            int topCoordTable = 7;

            int leftProductItem = 16;
            int topProductItem = 9;

            int leftMenuOptions = 15;
            int topMenuOptions = 34;           

            List<Product> products = productsParameter.ToList();            

            int count = products.Count();
            int currentPage = pageStack.Peek();
             
           // Output
            Menu.PrintTable(ref leftCoordTable, ref topCoordTable);
            Menu.PrintHeader(ref leftProductItem, ref topProductItem);
            Menu.PrintProducts(ref leftProductItem, ref topProductItem, currentPage, count, products);

            Console.SetCursorPosition(15, 31);
            Console.WriteLine($"Count: {products.Count}");


            PrintMenuOptions(leftMenuOptions, topMenuOptions, menuItems);

            Console.SetCursorPosition(29, ++topCoordTable);
            PageHandler prodHand = new PageHandler(new StandardPageBehavior());
            Console.Write("<<   ");
            Console.Write($"Page: {currentPage / 10 + 1} / {prodHand.PerformGetPage(productsParameter.ToList())}");
            Console.WriteLine("   >>");
        }
        /// <summary>
        /// Prints the menu options in the console.
        /// </summary>
        /// <param name="posX">sets the cursor position in the x axis in the console.</param>
        /// <param name="posY">sets the cursor position in the y axis in the console.</param>
        /// <param name="menuItems">the menu selection options to be displayed.</param>
        static void PrintMenuOptions(int posX, int posY, params string[] menuItems)
        {
            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.SetCursorPosition(posX, posY++);
                Console.WriteLine(menuItems[i]);
            }

        }
        /// <summary>
        /// Prints the header in the console.
        /// </summary>
        /// <param name="posX">sets the cursor position in the x axis in the console.</param>
        /// <param name="posY">sets the cursor position in the y axis in the console.</param>
        public static void PrintHeader(ref int posX, ref int posY)
        {
            Console.SetCursorPosition(posX, posY);
            Console.Write("Product");
            Console.SetCursorPosition(AppData.PriceXLoc, posY);

            Console.Write("Price");
            Console.SetCursorPosition(AppData.QuantityXLoc, posY);
            Console.Write("Quantity");
            posY += 2;

        }
        /// <summary>
        /// Prints a list of products to the console.
        /// </summary>
        /// <param name="posX">sets the cursor position in the x axis in the console.</param>
        /// <param name="posY">sets the cursor position in the y axis in the console.</param>
        /// <param name="currentPage">sets the current page in the list to be displayed.</param>
        /// <param name="count">sets the number of items to be displayed.</param>
        /// <param name="products"></param>
        public static void PrintProducts(ref int posX, ref int posY, int currentPage, int count, List<Product> products)
        {
            

            for (int i = currentPage; i < currentPage + 10 && i < count; i++)
            {
                Console.SetCursorPosition(posX, posY);
                Product product = products[i];
                Console.Write($"{product.ProductName}");

                Console.SetCursorPosition(AppData.PriceXLoc, posY);
                Console.Write($"{product.ProductPrice:C}");

                Console.SetCursorPosition(AppData.QuantityXLoc, posY);
                Console.Write($"{product.QtyInStock:N0}");

                posY += 2;
            }


        }
        /// <summary>
        /// Prints the table border in the console.
        /// </summary>
        /// <param name="posX">sets the cursor position in the x axis in the console.</param>
        /// <param name="posY">sets the cursor position in the y axis in the console.</param>
        public static void PrintTable(ref int posX, ref int posY)
        {
            // Initialization
            string line = "--------------------------------------------------";
            Console.SetCursorPosition(posX, ++posY);

            // Output
            Console.WriteLine(new string('_', line.Length + 1));
            Console.SetCursorPosition(posX, ++posY);
            Console.Write('|');
            Console.SetCursorPosition(65, posY);
            Console.Write('|');


            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(posX, ++posY);
          
                Console.Write('|' + new string('-', line.Length - 1) + '|');
                //Start new row table
                Console.SetCursorPosition(posX, ++posY);
                Console.Write('|');

                Console.SetCursorPosition(65, posY);

                //table end
                Console.Write('|');

            }


            Console.SetCursorPosition(posX, posY = 30);
            Console.WriteLine(new string('=', line.Length + 1));
        }

    }
}


using FinalOutput.PageHandling;
using FinalOutput.Services;
using FinalOutput.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalOutput.ViewState
{
    /// <summary>
    /// Class that displays view for check out.
    /// </summary>
    public class CheckOutView : MenuView
    {
        private readonly ProductVM _productVM;
        private readonly ProductService _productService;
        private readonly Stack<int> pageStack;
        private bool isSearching;
        private string searchItem;
        UserPrompt prompt;
        POSView POSView;

        public CheckOutView()
        {
            this._productService = new ProductService(AppData.productFolderName);
            this._productVM = new ProductVM(_productService);
            this.pageStack = new Stack<int>(new int[] { 0 });
            isSearching = false;
            searchItem = null;
            prompt = new UserPrompt();
            POSView = new POSView();
            
        }
        /// <summary>
        /// Displays check out view in the console.
        /// </summary>
        public override void Display()
        {
            // Initialization
            Console.SetWindowSize(97, 45);
            Console.Title = "CheckOut";
            Console.ForegroundColor = ConsoleColor.Cyan;
            var cartProducts = MyCart.GetMyCartProducts().ToList();
            Console.SetWindowSize(102, AppData.height);            
            PageHandler standardPage = new PageHandler(new StandardPageBehavior());
            string[] mainMenuItems = { "[1] Buy", "[2] Remove Product", "[3] Search", "[4] Clear Search","[5] Back"};
            Console.CursorVisible = false;
            Console.Clear();

            if (isSearching)
            {
                cartProducts = POSView.FilterProductsByName(cartProducts, searchItem);
                
            }

            DisplayProducts(pageStack, cartProducts, mainMenuItems);

            AsciiArt.PrintCheckOutAsciiArt(30, AppData.asciiHeaderY); 

            Console.SetCursorPosition(15, 40);

            var inputKey = Console.ReadKey(true);

            // Logic for handling user key input
            switch (inputKey.KeyChar)
            {
                //Use a prompt for cash-on-hand 
                case '1': // [1] Buy

                    if (cartProducts.Count == 0)
                    {
                        Console.SetCursorPosition(15, 40);

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Add products to cart first!");
                        Console.ResetColor();
                        Console.SetCursorPosition(15, 41);
                        Console.WriteLine("Press enter to continue...");
                        Console.ReadLine();
                        return;
                    }
                    else if (cartProducts.Any(product => product.QtyInCart == 0))
                    {
                        Console.SetCursorPosition(15, 40);

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Add quantity to products first!");
                        Console.ResetColor();
                        Console.SetCursorPosition(15, 41);
                        Console.WriteLine("Press enter to continue...");
                        Console.ReadLine();
                        return;
                    }

                    Console.CursorVisible = true;
                    var productsToDisplay = GetCopyOfProducts(cartProducts);

                    decimal cashOnHand = GetCashOnHandInput();
                    ReceiptView receiptView = new ReceiptView(productsToDisplay);
                    receiptView.CashOnHand = cashOnHand;

                    // Reduce Product Quantity
                    foreach (Product product in cartProducts)
                    {
                        product.QtyInStock -= product.QtyInCart;
                        product.QtyInCart = 0;
                        _productService.Update(product);
                    }
                    // Remove Products in Cart
                    foreach (Product product in cartProducts)
                    {
                        InventorySystem.RemoveProductFromInventory(product.ProductName, MyCart.myCartFolderPath);
                    }

                    Menu.SetDisplayView(receiptView);
                    //Menu.SetDisplayView(new ReceiptView(productsToDisplay));
                    Console.Clear();
                    return;

                case '2': // [2] Delete
                    Console.Write("Type a product you would like to remove: ");
                    string productName = Console.ReadLine().Trim().ToLower();

                    Product getProduct = cartProducts.Find(product => product.ProductName.ToLower() == productName.ToLower());

                    if (getProduct == null)
                    {
                        Console.WriteLine("Invalid: Product does not exists");
                        Console.WriteLine("Press enter to continue...");
                        Console.ReadLine();
                    }
                    else
                    {
                        MyCart.RemoveProductFromCart(getProduct);

                    }


                    break;

                case '3': // [3] Search
                    isSearching = true;
                    string input = prompt.UserSearchProduct("Enter the product name to be searched: ", 15, 39, ConsoleColor.Cyan);
                    if (!String.IsNullOrEmpty(input))
                    {
                        searchItem = input;
                    }
                    else
                    {
                        isSearching = false;
                    }
                    break;
                case '4': // [4] Clear Search
                    isSearching = false;


                    break;
                case '5': // [5] Back
                    Menu.SetPreviousView();
                    break;
                default:

                    if (inputKey.Key == ConsoleKey.RightArrow)
                    {
                        //Call previous function
                        standardPage.PerformNextPage(pageStack, AppData.mainProducts);
                    }
                    else if (inputKey.Key == ConsoleKey.LeftArrow)
                    {
                        //Call next function
                        standardPage.PerformPreviousPage(pageStack);
                    }
                    break;

            }        
        }
        /// <summary>
        /// Returns deep copy of products
        /// </summary>
        /// <param name="products">the products to be copied</param>
        /// <returns>a new copy of products</returns>
        private List<Product> GetCopyOfProducts(List<Product> products)
        {
            var newProducts = new List<Product>();
            foreach(Product product in products)
            {
                newProducts.Add(new Product(product.Id, product.ProductName, product.ProductPrice, product.QtyInStock, product.QtyInCart));
            }
            return newProducts;
        }
        /// <summary>
        /// Displays products in the console in tabular format.
        /// </summary>
        /// <param name="pageStack">the current page to be displayed.</param>
        /// <param name="productsParameter">the products to be displayed.</param>
        /// <param name="menuItems">the menu selection options to be displayed to the user.</param>
        public void DisplayProducts(Stack<int> pageStack, IEnumerable<Product> productsParameter, string[] menuItems)
        {
            // Initialization
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
            PrintTable(ref leftCoordTable, ref topCoordTable);
            PrintHeader(ref leftProductItem, ref topProductItem);
            PrintProducts(ref leftProductItem, ref topProductItem, currentPage, count, products);

            Console.SetCursorPosition(15, 31);
            Console.WriteLine($"Count: {products.Count}");

            PrintMenuOptions(leftMenuOptions, topMenuOptions, menuItems);

            Console.SetCursorPosition(29, ++topCoordTable);
            PageHandler prodHand = new PageHandler(new StandardPageBehavior());
            Console.Write("<<   ");
            Console.Write($"Page: {currentPage / 10 + 1} / {prodHand.PerformGetPage(productsParameter.ToList())}   ");
            Console.WriteLine(">>");


            //To be moved
            ProductTransaction checkoutTransaction = new ProductTransaction();
            Console.SetCursorPosition(60, 31);

            checkoutTransaction.CalculateTotal(productsParameter.ToList());
            Console.Write($"Overall Total: ${checkoutTransaction.TotalValue:N2}");
        }
        /// <summary>
        /// Prints the header in the console.
        /// </summary>
        /// <param name="posX">sets the cursor position in the x axis in the console.</param>
        /// <param name="posY">sets the cursor position in the y axis in the console.</param>
        public void PrintHeader(ref int posX, ref int posY)
        {
            Console.SetCursorPosition(posX, posY);
            Console.Write("Product");
            Console.SetCursorPosition(AppData.PriceXLoc, posY);

            Console.Write("Price");
            Console.SetCursorPosition(AppData.QuantityXLoc, posY);
            Console.Write("Quantity");
            
            Console.SetCursorPosition(AppData.TotalPerProductXLoc, posY);
            Console.Write("Total");


            posY += 2;

        }
        /// <summary>
        /// Prints the menu options in the console.
        /// </summary>
        /// <param name="posX">sets the cursor position in the x axis in the console.</param>
        /// <param name="posY">sets the cursor position in the y axis in the console.</param>
        /// <param name="menuItems">the menu selection options to be displayed.</param>
        private void PrintMenuOptions(int posX, int posY, params string[] menuItems)
        {
            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.SetCursorPosition(posX, posY++);
                Console.WriteLine(menuItems[i]);
            }

        }
        /// <summary>
        /// Prints a list of products to the console.
        /// </summary>
        /// <param name="posX">sets the cursor position in the x axis in the console.</param>
        /// <param name="posY">sets the cursor position in the y axis in the console.</param>
        /// <param name="currentPage">sets the current page in the list to be displayed.</param>
        /// <param name="count">sets the number of items to be displayed.</param>
        /// <param name="products"></param>

        public void PrintProducts(ref int posX, ref int posY, int currentPage, int count, List<Product> products)
        {
            // Output
            for (int i = currentPage; i < currentPage + 10 && i < count; i++)
            {
                Console.SetCursorPosition(posX, posY);
                Product product = products[i];
                Console.Write($"{product.ProductName}");

                Console.SetCursorPosition(AppData.PriceXLoc, posY);
                Console.Write($"{product.ProductPrice:C}");

                Console.SetCursorPosition(AppData.QuantityXLoc, posY);
                Console.Write($"{product.QtyInCart:N0}");

                Console.SetCursorPosition(AppData.TotalPerProductXLoc, posY);
                Console.Write($"{product.GetTotalPrice():C}");

                posY += 2;
            }


        }

        /// <summary>
        /// Prints the table border in the console.
        /// </summary>
        /// <param name="posX">sets the cursor position in the x axis in the console.</param>
        /// <param name="posY">sets the cursor position in the y axis in the console.</param>
        public void PrintTable(ref int posX, ref int posY)
        {
            // Initialization
            string line = "------------------------------------------------------------------------";
            Console.SetCursorPosition(posX, ++posY);

            // Output
            Console.WriteLine(new string('_', line.Length + 1));
            Console.SetCursorPosition(posX, ++posY);
            Console.Write('|');

            Console.SetCursorPosition(87, posY);
            Console.Write('|');


            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(posX, ++posY);

                Console.Write('|' + new string('-', line.Length - 1) + '|');
                //Start new row table
                Console.SetCursorPosition(posX, ++posY);
                Console.Write('|');

                Console.SetCursorPosition(87, posY);

                //table end
                Console.Write('|');

            }


            Console.SetCursorPosition(posX, posY = 30);
            Console.WriteLine(new string('=', line.Length + 1));
        }

        /// <summary>
        /// Prompts user for cash on hand.
        /// </summary>
        private decimal GetCashOnHandInput()
        {
            decimal cashOnHand;

            while (true)
            {
                Console.SetCursorPosition(15, 40);
                Console.Write("Enter cash on hand: ");

                if (decimal.TryParse(Console.ReadLine().Trim(), out cashOnHand))
                {
                    var totalAmount = CalculateTotalAmount();

                    if (cashOnHand >= totalAmount)
                    {
                        return cashOnHand;
                    }
                    else
                    {
                        Console.SetCursorPosition(15, 42);
                        Console.WriteLine($"Error: Insufficient cash. Total amount needed: {totalAmount:N2}       ");
                        Console.SetCursorPosition(15, 40);
                        Console.Write(new string(' ', Console.WindowWidth - 20)); // Clear the cash input line
                    }
                }
                else
                {
                    Console.SetCursorPosition(15, 42);
                    Console.WriteLine("Error: Invalid input. Please enter a valid numeric value.");
                    Console.SetCursorPosition(15, 40);
                    Console.Write(new string(' ', Console.WindowWidth - 20)); // Clear the cash input line
                }
            }
        }
        /// <summary>
        /// Returns the total amount from the products.
        /// </summary>
        private decimal CalculateTotalAmount()
        {
            var productsToDisplay = GetCopyOfProducts(MyCart.GetMyCartProducts().ToList());
            var checkoutTransaction = new ProductTransaction();
            checkoutTransaction.CalculateTotal(productsToDisplay);
            return checkoutTransaction.TotalValue;
        }
    }
}

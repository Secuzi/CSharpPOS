using FinalOutput.Common.Helper;
using FinalOutput.Interfaces;
using FinalOutput.PageHandling;
using FinalOutput.Services;
using FinalOutput.State;
using FinalOutput.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml.Linq;

namespace FinalOutput.ViewState
{
    /// <summary>
    /// Class that displays Point of Sale view.
    /// </summary>
    internal class POSView : MenuView
    {
       
        public Stack<int> pageStack;
        public Stack<int> cartPageStack;
        private readonly ICostumerAccountGetService _customersGetService;
        private readonly IProductService _productService;
        private readonly ProductVM _productVM;
        private bool isPosSearch;
        private bool isCartSearch;
        private bool isError;
        private bool IsInMainProductsSection;
        private string SelectedItemPOS;
        private string SelectedItemCart;
        TileSelectInput selectIn;
        MyCartSelectInput selectCart;

        public POSView()
        {
            pageStack = new Stack<int>(new int[] { 0 });
            _customersGetService = new CostumerAccountGetService();
            _productService = new ProductService(AppData.productFolderName);
            _productVM = new ProductVM(_productService);
            cartPageStack = new Stack<int>(new int[] { 0 });
            IsInMainProductsSection = true;
            selectIn = new TileSelectInput(0, 0);
            selectCart = new MyCartSelectInput();
            isPosSearch = false;
            isCartSearch = false;
            isError = false;
            SelectedItemPOS = null;
            SelectedItemCart = null;
        }
        /// <summary>
        /// Displays POS view in the console.
        /// </summary>
        public override void Display()
        {
            // Initialization
            Console.Title = "POS";
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
            int consoleHeight = 50;
            int consoleWidth = 160;

            int parentFrameX = 3;
            int parentFrameY = 2;

            Console.SetWindowSize(consoleWidth, consoleHeight);

            Frame parentFrame = new Frame(consoleWidth - 6, consoleHeight - 4, parentFrameX, parentFrameY);


            parentFrame.PrintFrame(ConsoleColor.Cyan);

            parentFrame.DrawHorizontalLine(parentFrame.Width - 2, parentFrame.MinPosX + 1, parentFrame.PosY - 7, ConsoleColor.Cyan);
            parentFrame.DrawVerticalLine(parentFrame.Height - 2, 117, parentFrameY + 1, ConsoleColor.Cyan);
            string[] menuItems = { "Menu/Cart", "Search", "Enter", "Back", "Navigation", "Prev", "Next", "Page", "Checkout" };

            string headerForPOS = @"POS";
            Console.SetCursorPosition(59, 3);
            Console.Write(headerForPOS);
            string headerForCart = @"My Cart";
            Console.SetCursorPosition(133, 3);
            Console.Write(headerForCart);

            UserInput input = new UserInput();

            bool quantityInput = false;
            bool isCartCurrentInput = false;
            ConsoleKey mainMenuInputKey = ConsoleKey.N;
            ConsoleKey cartMenuInputKey = ConsoleKey.N;

            AppData.mainProducts = _productService.GetAll().Where(prod => prod.QtyInStock > 0).ToList();

            //Output
            UserPrompt prompt = new UserPrompt();
            PrintMenuLegend(menuItems, parentFrame.MinPosX + 2, parentFrame.Height - 3);

            //temporary
            int currCartIndex = 0;
        Label:
            var cartProducts = MyCart.GetMyCartProducts().ToList();

            PageHandler mainPosPage = new PageHandler(new MainPOSPageBehavior());
            //PageHandler.NextPage(pageStack, AppData.mainProducts);
            int currentPage = pageStack.Peek();
            int cartCurrentPage = cartPageStack.Peek();
            Console.CursorVisible = false;

            int selectedIndexI = selectIn.SelectedIndexI;
            int selectedIndexJ = selectIn.SelectedIndexJ;
            List<List<Frame>> frames = new List<List<Frame>>();
            List<Frame> cartFrames = new List<Frame>();
            int ctrY = 5;
            int ctrX = 10;



            int prodCounter = currentPage;

            bool @break = false;
            //Print frame
            ConsoleColor color = ConsoleColor.Cyan;
            ConsoleColor cartColor = ConsoleColor.Cyan;

            int frameRow = 0;
            int frameCol = 0;


            decimal rows = 0;

            if (isPosSearch)
                AppData.mainProducts = FilterProductsByName(AppData.mainProducts, SelectedItemPOS);

            if (isError)
            {
                // instead of products, error message ang mu pop up
                //AppData.mainProducts = "error message"
                Console.WriteLine("otin");
            }
            
            // DISPLAY MAIN PRODUCTS
            for (int prodCurrentPage = currentPage; prodCurrentPage < currentPage + 3; prodCurrentPage++)
            {
                if (AppData.mainProducts.Count != 0)
                {
                    frames.Add(new List<Frame>(AppData.mainProducts.Count));

                }
                for (int col = 0; col < 3; col++)
                {

                    if (prodCounter < AppData.mainProducts.Count)
                    {
                        frames[frameRow].Add(new Frame(30, 10, ctrX, ctrY));

                    }

                    // No more products to display
                    if (prodCounter >= AppData.mainProducts.Count && @break == false)
                    {
                        @break = true;
                        break;
                    }
                    // Input Logic For Selected Product in Main Products
                    else if (selectIn.SelectedIndexI == frameRow && selectIn.SelectedIndexJ == col && IsInMainProductsSection == true)
                    {
                        color = ConsoleColor.Red;

                        // Logic for handling user key input in main products
                        switch (mainMenuInputKey)
                        {
                            #region Removed Selected Quantity+-
                            /* 
                             * case ConsoleKey.A: // [A] Selected Quantity -1  
                                if (AppData.mainProducts[prodCounter].SelectedQuantity > 0)
                                {
                                    var product = AppData.mainProducts[prodCounter];
                                    product.ProductQuantity -= 1;
                                    _productService.Update(product);
                                }
                                break;
                            case ConsoleKey.D: // [D] Selected Quantity +1
                                if (AppData.mainProducts[prodCounter].SelectedQuantity < 999 && AppData.mainProducts[prodCounter].SelectedQuantity < AppData.mainProducts[prodCounter].ProductQuantity)
                                    AppData.mainProducts[prodCounter].SelectedQuantity++;
                                break;
                            */
                            #endregion
                            case ConsoleKey.Spacebar: // [Spacebar] Input Quantity
                                quantityInput = true;
                                break;

                            case ConsoleKey.Enter: // [Enter]  Add To Cart

                                bool confirm = prompt.UserSelectConfirmation("Add to Cart? (Y/N): ", 50, 44, ConsoleColor.Green);

                                if (confirm == false)
                                    return;
                                

                                var productToAdd = _productService.GetProductByName(AppData.mainProducts[prodCounter].ProductName);

                                // If Product does not exist
                                if (productToAdd == null || productToAdd.QtyInStock <= 0)
                                {
                                    Console.SetCursorPosition(50, 46);
                                    Console.WriteLine("Product currently has no stock.");
                                    Thread.Sleep(1200);
                                    return;
                                }
                                
                                // Checking for Cart Product
                                var cartProduct = cartProducts.Find(product => product.ProductName.ToLower() == AppData.mainProducts[prodCounter].ProductName.ToLower());


                                // If Product is not in cart, add to cart file 
                                if (cartProduct == null)
                                {
                                    productToAdd.QtyInCart += 1;
                                    InventorySystem.AddProductToStorage(productToAdd, MyCart.myCartFolderPath);

                                }

                                if (CheckIfPosProductQtyIsZero(mainPosPage, productToAdd) && cartProduct == null)
                                {
                                    MyCart.RemoveProductFromCart(productToAdd);
                                    frames[frameRow].RemoveAt(col);
                                }

                              
                                if (frames[frameRow].Count == 0 && productToAdd.QtyInStock == 0 && frames[frameRow] != frames[0])
                                {

                                    selectIn.SelectedIndexI--;

                                }                              
                                else if (frames[frameRow].Count > 0 && productToAdd.QtyInStock == 0)
                                {
                                    selectIn.SelectedIndexJ--;

                                }                                                              
                                return;
                        }
                    }
                    else
                    {
                        color = ConsoleColor.Cyan;

                    }
                    frames[frameRow][col].PrintFrame(color);
                    frames[frameRow][col].WriteLine($"Product Name: {AppData.mainProducts[prodCounter].ProductName}");
                    frames[frameRow][col].WriteLine($"Price: ${AppData.mainProducts[prodCounter].ProductPrice:N2}");
                    frames[frameRow][col].WriteLine($"Qty in Stock: {AppData.mainProducts[prodCounter].QtyInStock}");

                    #region Removed [+][-] for Main Products
                    /*frames[frameRow][col].CurrentPosX = frames[frameRow][col].PosX + frames[frameRow][col].Width - 12;
                    frames[frameRow][col].CurrentPosY = frames[frameRow][col].PosY - 3;
                    frames[frameRow][col].WriteLine($"[-] {AppData.mainProducts[prodCounter].SelectedQuantity} [+]");

                    int helperX = frames[frameRow][col].PosX + frames[frameRow][col].Width - 11;
                    int helperY = frames[frameRow][col].PosY - 2;


                    frames[frameRow][col].CurrentPosX = frames[frameRow][col].PosX + frames[frameRow][col].Width - 11;
                    frames[frameRow][col].CurrentPosY = frames[frameRow][col].PosY - 2;
                    frames[frameRow][col].WriteLine('A');

                    int aPosX = frames[frameRow][col].PosX + frames[frameRow][col].Width - 11;
                    int aPosY = frames[frameRow][col].PosY - 2;


                    if (AppData.mainProducts[prodCounter].SelectedQuantity > 99)
                    {

                        frames[frameRow][col].CurrentPosX = frames[frameRow][col].PosX + frames[frameRow][col].Width - 3;

                        frames[frameRow][col].Write(new string(' ', 6), helperX + 1, helperY);

                        frames[frameRow][col].CurrentPosX = aPosX + 8;
                        frames[frameRow][col].CurrentPosY = frames[frameRow][col].PosY - 2;
                    }
                    else if (AppData.mainProducts[prodCounter].SelectedQuantity > 9)
                    {
                        frames[frameRow][col].CurrentPosX = aPosX + 7;
                        frames[frameRow][col].CurrentPosY = frames[frameRow][col].PosY - 2;

                    }
                    else
                    {
                        frames[frameRow][col].CurrentPosX = aPosX + 6;
                        frames[frameRow][col].CurrentPosY = frames[frameRow][col].PosY - 2;
                    }
                    frames[frameRow][col].WriteLine('D');
                    


                    if (quantityInput == true)
                    {

                        quantityInput = PosInputQuantity(frames[frameRow][col], frames[frameRow][col].PosX + 2, frames[frameRow][col].PosY - 5, AppData.mainProducts, prodCounter);

                    }
                    Console.CursorVisible = false;

                    frames[frameRow][col].CurrentPosX = frames[frameRow][col].PosX + frames[frameRow][col].Width - 12;
                    frames[frameRow][col].CurrentPosY = frames[frameRow][col].PosY - 3;
                    frames[frameRow][col].Write(new string(' ', 11), frames[frameRow][col].CurrentPosX, frames[frameRow][col].CurrentPosY);
                    frames[frameRow][col].WriteLine($"[-] {AppData.mainProducts[prodCounter].SelectedQuantity} [+]");

                    if (AppData.mainProducts[prodCounter].SelectedQuantity > 99)
                    {

                        frames[frameRow][col].CurrentPosX = frames[frameRow][col].PosX + frames[frameRow][col].Width - 3;

                        frames[frameRow][col].CurrentPosX = aPosX + 8;
                        frames[frameRow][col].CurrentPosY = frames[frameRow][col].PosY - 2;
                    }
                    else if (AppData.mainProducts[prodCounter].SelectedQuantity > 9)
                    {
                        frames[frameRow][col].CurrentPosX = aPosX + 7;
                        frames[frameRow][col].CurrentPosY = frames[frameRow][col].PosY - 2;

                    }
                    else
                    {
                        frames[frameRow][col].CurrentPosX = aPosX + 6;
                        frames[frameRow][col].CurrentPosY = frames[frameRow][col].PosY - 2;
                    }
                    frames[frameRow][col].Write(new string(' ', 7), helperX + 1, helperY);
                    frames[frameRow][col].WriteLine('D');

                    */
                    #endregion

                    ctrX += 35;
                    prodCounter++;
                    frameCol++;
                    rows++;
                }

                if (prodCounter >= AppData.mainProducts.Count)
                    break;
                ctrY += 12;
                ctrX = 10;
                frameRow++;
            }
            // END OF MAIN PRODUCTS DISPLAY



            // DISPLAY CART PRODUCTS 

            int cartPosX = 122;
            int cartPosY = parentFrameY + 3;
            int cartProdCtr = 0;

            Console.SetCursorPosition(parentFrame.MinPosX + 2, 40);
            Console.Write($"Product Count: {AppData.mainProducts.Count}");

            int posPage = mainPosPage.PerformGetPage(AppData.mainProducts);
            mainPosPage.PerformPrintPage(currentPage, posPage, 105, 40);

            PageHandler mainCartPage = new PageHandler(new CartPOSPageBehavior());


            if (isCartSearch)
            {
                cartProducts = FilterProductsByName(cartProducts, SelectedItemCart);
            }

            int cartPage = mainCartPage.PerformGetPage(cartProducts);

            Console.SetCursorPosition(119, 40);


            Console.Write($"Cart Count: {cartProducts.Count}");

            ProductTransaction test = new ProductTransaction();
            test.CalculateTotal(cartProducts);
            Console.SetCursorPosition(119, 43);
            Console.Write(new string(' ', 30));
            Console.SetCursorPosition(119, 43);

            Console.Write($"Total: {test.TotalValue:C}");

            if (cartProducts.Count > 0)
            {
                mainCartPage.PerformPrintPage(cartCurrentPage, cartPage, 147, 40);
            }

            //Depends on cart input


            int testCtr = cartCurrentPage;

            int cartframeRow = 0;

            for (int cartIndex = cartCurrentPage; cartIndex < cartCurrentPage + 3 && cartIndex < cartProducts.Count(); cartIndex++)
            {
                cartFrames.Add(new Frame(30, 10, cartPosX, cartPosY));

                if (!IsInMainProductsSection && selectCart.SelectedIndexI == cartframeRow)
                {
                    cartColor = ConsoleColor.Red;
                    currCartIndex = cartIndex;
                }
                else
                {
                    cartColor = ConsoleColor.Cyan;
                }


                //Display Cart Product
                cartFrames[cartframeRow].PrintFrame(cartColor);
                cartFrames[cartframeRow].WriteLine($"Product name: {cartProducts[cartIndex].ProductName}");
                cartFrames[cartframeRow].WriteLine($"Price: ${cartProducts[cartIndex].ProductPrice:N2}");
                cartFrames[cartframeRow].WriteLine($"Quantity: {cartProducts[cartIndex].QtyInCart}");
                cartFrames[cartframeRow].WriteLine($"Total: ${cartProducts[cartIndex].GetTotalPrice():N2}");

                cartFrames[cartframeRow].CurrentPosX = cartFrames[cartframeRow].PosX + cartFrames[cartframeRow].Width - 12;
                cartFrames[cartframeRow].CurrentPosY = cartFrames[cartframeRow].PosY - 3;
                cartFrames[cartframeRow].WriteLine($"[-] [+]");
                cartFrames[cartframeRow].WriteLine($" A   D");



                if (isCartCurrentInput == true && currCartIndex == cartIndex)
                {
                    CartInputQuantity(cartFrames[cartframeRow], cartFrames[cartframeRow].PosX + cartFrames[cartframeRow].Width - 28, cartFrames[cartframeRow].PosY - 5, cartProducts, cartIndex);

                    if (selectCart.SelectedIndexI == cartProducts.Count)
                    {
                        selectCart.SelectedIndexI--;
                        mainPosPage.PerformPreviousPage(cartPageStack);
                    }
                    isCartCurrentInput = false;
                    return;
                }

                Console.CursorVisible = false;

                cartFrames[cartframeRow].CurrentPosX = cartFrames[cartframeRow].PosX + cartFrames[cartframeRow].Width - 12;
                cartFrames[cartframeRow].CurrentPosY = cartFrames[cartframeRow].PosY - 3;
                cartFrames[cartframeRow].Write(new string(' ', 11), cartFrames[cartframeRow].CurrentPosX, cartFrames[cartframeRow].CurrentPosY);
                cartFrames[cartframeRow].WriteLine($"[-] [+]");


                cartframeRow++;


                cartPosY += 12;
                cartPosX = 122;
                cartProdCtr++;
            }
            // END OF DISPLAY CART PRODUCTS


            int numberOfRows = (int)Math.Ceiling(rows / 3);
            int lastRowCount = 0;
            cartframeRow--;

            selectIn.Row = numberOfRows;
            selectIn.Frames = frames;
            selectIn.CountMax = lastRowCount;
            selectCart.CartProdCounter = cartProdCtr - 1;
            if (IsInMainProductsSection == true)
            {
                input.SetInputBehavior(selectIn);
                mainMenuInputKey = input.PerformCatchInput();

            }
            else
            {
                input.SetInputBehavior(selectCart);
                cartMenuInputKey = input.PerformCatchInput();
            }

            // Logic for handling user key input
            if (IsInMainProductsSection)
            {
                //MAIN PRODUCTS SECTION
                switch (mainMenuInputKey)
                {
                    case ConsoleKey.D1: // [1] Cart
                        IsInMainProductsSection = false;
                        isPosSearch = false;
                        mainMenuInputKey = ConsoleKey.N;                      
                        break;
                    case ConsoleKey.D2: // [2] Search
                        isPosSearch = true;

                        string inputPos = prompt.UserSearchProduct("Enter the product name to be searched: ", 45, 44, ConsoleColor.Green); ;
                        if (!String.IsNullOrEmpty(inputPos))
                        {
                            SelectedItemPOS = inputPos;
                        }
                        else
                        {
                            isPosSearch = false;
                        }

                        return;
                    case ConsoleKey.D4: // [4] Exit Search/Page
                        if (isPosSearch)                        
                            isPosSearch = false;
                        else 
                            Menu.SetPreviousView();                        
                        return;
                    case ConsoleKey.E: // [E] Next
                        mainPosPage.PerformNextPage(pageStack, AppData.mainProducts);
                        IsInMainProductsSection = true;

                        selectIn.SelectedIndexJ = 0;
                        selectIn.SelectedIndexI = 0;
                        return;
                    case ConsoleKey.C: // [C] Checkout
                        Menu.SetDisplayView(new CheckOutView());
                        return;
                    case ConsoleKey.Q: // [Q] Previous
                        mainPosPage.PerformPreviousPage(pageStack);
                        IsInMainProductsSection = true;
                        return;
                    default:
                        //TODO
                        break;
                }
            }
            else
            {
                // CART PRODUCTS SECTION
                switch (cartMenuInputKey)
                {
                    case ConsoleKey.D1: // [1] Menu
                        IsInMainProductsSection = true;
                        isCartSearch = false;
                        cartMenuInputKey = ConsoleKey.N;
                        PrintMenuLegend(menuItems, parentFrame.MinPosX + 2, parentFrame.Height - 3);
                        break;
                    case ConsoleKey.E: // [E] Next Page
                        mainCartPage.PerformNextPage(cartPageStack, cartProducts);
                        selectCart.SelectedIndexI = 0;
                        this.IsInMainProductsSection = false;
                        return;
                    case ConsoleKey.D2: // [2] Search
                        isCartSearch = true;
                        string inputCart = prompt.UserSearchProduct("Enter the product name to be searched: ", 45, 44, ConsoleColor.Green);
                        if (!String.IsNullOrEmpty(inputCart))
                        {
                            SelectedItemCart = inputCart;
                        }
                        else
                        {
                            isCartSearch = false;
                        }
                        return;
                    case ConsoleKey.D4: // [4] Exit
                        if (isCartSearch)
                            isCartSearch = false;
                        else
                            Menu.SetPreviousView();
                        return;

                    case ConsoleKey.Q: // [Q] Previous Page
                        mainCartPage.PerformPreviousPage(cartPageStack);
                        this.IsInMainProductsSection = false;
                        return;
                    case ConsoleKey.C: // [C] Checkout
                        Menu.SetDisplayView(new CheckOutView());
                        return;

                    case ConsoleKey.Spacebar: // [Spacebar] Select Custom Quantity
                        isCartCurrentInput = true;
                        break;
                    case ConsoleKey.A: // [A] Selected Quantity--
                        if ( cartProducts[currCartIndex].QtyInCart > 0)
                        {
                            var product = cartProducts[currCartIndex];
                            product.QtyInCart -= 1;
                            _productService.Update(product);
                        }
                        break;
                    case ConsoleKey.D: // [D] Selected Quantity++
                        if (cartProducts[currCartIndex].QtyInCart < 999 && cartProducts[currCartIndex].QtyInCart < cartProducts[currCartIndex].QtyInStock)
                        {
                            var product = cartProducts[currCartIndex];
                            product.QtyInCart += 1;
                            _productService.Update(product);
                        }
                        break;
                    case ConsoleKey.Enter: // [Enter] Remove from Cart

                        bool confirm = prompt.UserSelectConfirmation($"Remove item from cart? (Y/N): ", 50, 44, ConsoleColor.Green);

                        if (confirm == false)
                        {
                            return;
                        }

                        var selectedProduct = cartProducts.Find(product => product.ProductName.ToLower() == cartProducts[currCartIndex].ProductName.ToLower());

                        MyCart.RemoveProductFromCart(selectedProduct);
                        currCartIndex = currCartIndex > 0 ? currCartIndex-- : 0;

                        if (cartFrames.Count == 0 && selectedProduct.QtyInStock == 0)
                        {
                            selectCart.SelectedIndexI = 0;

                            mainCartPage.PerformPreviousPage(cartPageStack);
                        }
                        //If the selected input is not 0 which means the selection is not in the first row and quantintiy stock is 0 then
                        //then the selected index will go up to 1 row
                        else if (cartFrames.Count > 0 && selectedProduct.QtyInStock == 0 && selectCart.SelectedIndexI != 0)
                        {
                            selectCart.SelectedIndexI--;
                        }                                               
                        return;


                    default:

                       // TODO Implement Default Behavior For Cart

                        break;
                }
            }
            goto Label;
        }
        /// <summary>
        /// Checks if product quantity in POS is zero.
        /// </summary>
        /// <param name="mainPosPage">The current page in POS.</param>
        /// <param name="product">The product to be checked.</param>
        /// <returns>True if quantity is zero; otherwise, false.</returns>
        private bool CheckIfPosProductQtyIsZero(PageHandler mainPosPage, Product product)
        {
            if (ProductQtyValidator.CheckIfProductQuantityIsZero(product))
            {

                InventorySystem.DeleteStock(product, InventorySystem.stockFolderPath);
                AppData.mainProducts.Remove(product);


                if (AppData.mainProducts.Count % 9 == 0)
                {
                    selectIn.SelectedIndexI = 0;
                    selectIn.SelectedIndexJ = 0;
                    mainPosPage.PerformPreviousPage(pageStack);
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// Checks if product quantity in Cart is zero.
        /// </summary>
        /// <param name="cartProducts">The current cart products.</param>
        /// <param name="product">The product to be checked.</param>
        /// <returns>True if quantity is zero; otherwise, false.</returns>
        private bool CheckIfCartProductQtyIsZero(List<Product> cartProducts, Product product)
        {
            if (ProductQtyValidator.CheckIfProductQuantityIsZero(product))
            {
                MyCart.RemoveProductFromCart(product);
                InventorySystem.DeleteStock(product, MyCart.myCartFolderPath);
                //mainPosPage.PerformPreviousPage(cartPageStack);

                cartProducts.Remove(product);

                return true;

            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="products"></param>
        /// <param name="prodCtr"></param>
        public void CartInputQuantity(Frame frame, int posX, int posY, List<Product> products, int prodCtr = 0)
        {
            Console.CursorVisible = true;
            int inputPosX = posX;
            int inputPosY = posY;

            frame.Write("Enter quantity: ", inputPosX, inputPosY);

            // Get input with a length limit and check for Escape key
            string input = UserInput.GetLimitedInputWithCancelDigits(6);


            int quantity;
            //If valid, update product quantity
            if (!string.IsNullOrEmpty(input) && int.TryParse(input, out quantity) && products[prodCtr].QtyInStock >= quantity)
            {
                frame.Write(new string(' ', 27), inputPosX, inputPosY);

                var productToUpdate = products[prodCtr];
                productToUpdate.QtyInCart = quantity;
                _productService.Update(productToUpdate);
            }
            else
            {
                // Clear the previous invalid input
                frame.Write(new string(' ', 27), inputPosX, inputPosY);
                //Console.SetCursorPosition(50, 44);
                //Console.ForegroundColor = ConsoleColor.Red;
                //Console.Write("Invalid input.");
                //Console.ResetColor();
                //Thread.Sleep(1200);
            }

            return;
        }

        /// <summary>
        /// Displays menu selection options in the console.
        /// </summary>
        /// <param name="items">Items to be displayed.</param>
        /// <param name="posX">Sets cursor position in x axis in the console.</param>
        /// <param name="posY">Sets cursor position in y axis in the console.</param>
        public void PrintMenuLegend(string[] items, int posX, int posY)
        {
            int resetX = posX;
            int resetY = posY;
            for (int i = 0; i < items.Length; i++)
            {
                switch (items[i])
                {
                    case "Menu/Cart":
                        Console.SetCursorPosition(posX, posY);
                        Console.Write(new string(' ', 4));
                        Console.SetCursorPosition(posX, posY);

                        break;
                    case "Cart":
                        Console.SetCursorPosition(posX, posY);
                        Console.Write(new string(' ', 4));
                        Console.SetCursorPosition(posX, posY);
                        break;

                    case "Search":
                        Console.SetCursorPosition(posX, ++posY);
                        Console.Write(new string(' ', 6));
                        Console.SetCursorPosition(posX, posY);
                        break;

                    case "Enter":
                        Console.Write(new string(' ', 5));
                        Console.SetCursorPosition(posX += 20, posY);

                        Console.SetCursorPosition(posX, posY);
                        Console.Write("[Enter] Buy/Remove");
                        goto skip;

                    case "Back":
                        Console.SetCursorPosition(posX += 20, ++posY);
                        Console.Write(new string(' ', 12));

                        Console.SetCursorPosition(posX, posY);

                        break;
                    case "Navigation":
                        Console.Write(new string(' ', 7));
                        Console.SetCursorPosition(posX, posY += 2);

                        Console.SetCursorPosition(posX, posY);
                        Console.Write("[Arrow Keys] Navigation");
                        goto skip;

                    case "Prev":
                        Console.SetCursorPosition(90, posY);
                        Console.Write("<< prev page");
                        Console.SetCursorPosition(95, ++posY);
                        Console.Write("[Q]");
                        goto skip;
                    case "Next":
                        Console.SetCursorPosition(105, posY);
                        Console.Write("next page >>");

                        Console.SetCursorPosition(107, ++posY);
                        Console.Write("[E]");
                        goto skip;

                    case "Page":
                        //Console.Write($"Page: {currentPage / 9 + 1} / {page}   ");
                        //Console.SetCursorPosition(95, posY);

                        goto skip;

                    case "Checkout":
                        Console.SetCursorPosition(119, 45);
                        Console.Write("[C] Checkout");
                        goto skip;


                }
                Console.Write($"[{i + 1}] {items[i]}");
            skip:

                posX = resetX;
                posY = resetY;
            }
        }
        /// <summary>
        /// Filters products by product name.
        /// </summary>
        /// <param name="products">The list of products to be filtered.</param>
        /// <param name="productName">The name of product to be filtered.</param>
        /// <returns>Returns list of <see cref="Product"/> with the same name.</returns>
        public List<Product> FilterProductsByName(List<Product> products, string productName)
        {
            List<Product> SelectedItem = null;
            SelectedItem = products.Where(prod => prod.ProductName.ToLower().StartsWith(productName.ToLower())).ToList();
           
            return SelectedItem;
        }

    }
}

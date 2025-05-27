
using FinalOutput.Interfaces;
using FinalOutput.Services;
using FinalOutput.ViewModels;
using System.Collections.Generic;
using System;
using FinalOutput.PageHandling;
using System.Linq;
using System.Windows;
using System.Threading;

namespace FinalOutput.ViewState
{
    /// <summary>
    /// Class that handles displaying product manager view.
    /// </summary>
    public class ProductManagerView : MenuView
    {
        private readonly ICostumerAccountGetService _costumerAccountGetService;
        private readonly ProductService _productService;
        private readonly ProductVM productVM;
        private readonly Stack<int> pageStack;
        private bool IsSearching;
        private string searchedItem;
        POSView PosView;
        UserPrompt prompt;
        public ProductManagerView(ICostumerAccountGetService costumerAccountGetService)
        {
            this._costumerAccountGetService = costumerAccountGetService;
            this._productService = new ProductService(AppData.productFolderName);
            this.productVM = new ProductVM(_productService);
            this.pageStack = new Stack<int>(new int[] { 0 });
            IsSearching = false;
            searchedItem = null;

            PosView = new POSView();
            prompt = new UserPrompt();
        }
        /// <summary>
        /// Displays product manager view in console.
        /// </summary>
        public override void Display()
        {
            // Initialization
            Console.Title = "Product Manaager Menu";
            Console.CursorVisible = false;

            //height 51
            Console.SetWindowSize(AppData.width, 45);
            Console.ForegroundColor = ConsoleColor.Cyan;

            //Admin and Costumer hard coded

            PageHandler standardPage = new PageHandler(new StandardPageBehavior());
            string[] mainMenuItems = { "[1] Add Product", "[2] Remove Product", "[3] Edit Product", "[4] Search", "[5] Clear Search", "[6] Exit"};


            AppData.accounts = _costumerAccountGetService.GetCostumers();
            AppData.mainProducts = _productService.GetAll().ToList();

            Console.CursorVisible = false;

            Console.Clear();
            if (IsSearching)
            {
                AppData.mainProducts = PosView.FilterProductsByName(AppData.mainProducts, searchedItem);
            }

            // Output
            Menu.DisplayProducts(pageStack, AppData.mainProducts, mainMenuItems);
            AsciiArt.PrintProductManagerAsciiArt(15, AppData.asciiHeaderY);
            Console.SetCursorPosition(15, 40);
            ConsoleKeyInfo inputKey;

            do
            {
                inputKey = Console.ReadKey(true);

            } while (Console.KeyAvailable);

            // Logic for handling user key input logic
            switch (inputKey.KeyChar)
            {
                case '1': // [1] Add
                    Console.CursorVisible = true;

                    Product tempProd = productVM.CreateProduct(AppData.mainProducts);

                    if (tempProd != null)
                    {
                        AppData.mainProducts.Add(tempProd);
                        InventorySystem.AddProductToStorage(tempProd, InventorySystem.stockFolderPath);
                    }

                    if (standardPage.PerformCheckIfPageIsFull(AppData.mainProducts))
                    {
                        standardPage.PerformNextPage(pageStack, AppData.mainProducts);
                    }

                    break;

                case '2': // [2] Delete
                    Console.CursorVisible = true;
                    WriteWithPosition("Type a product you would like to delete: ", 15, 41);
                    string productName = Console.ReadLine().Trim().ToLower();

                    if (_productService.GetProductByName(productName) != null)
                    {
                        InventorySystem.RemoveProductFromInventory(productName, InventorySystem.stockFolderPath);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        WriteWithPosition("Invalid: Product does not exist", 15, 42);
                        //Console.WriteLine("Invalid: Product does not exists");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        WriteWithPosition("Press enter to continue...", 15, 43, ConsoleColor.Green); ;
                        //Console.WriteLine("Press enter to continue...");
                        Console.ReadLine();
                    }


                    break;

                case '3': // [3] Edit
                    EditProduct(AppData.mainProducts);
                    break;

                case '4': // [4] Search
                    IsSearching = true;
                    searchedItem = prompt.UserSearchProduct("Enter the product name to be searched: ", 15, 42, ConsoleColor.Cyan);
                    break;
                case '5': // [5] Clear Search
                    IsSearching = false;
                    break;
                case '6': // [6] Exit                    
                    IsSearching = false;
                    Menu.SetPreviousView();
                    break;

                default:

                    if (inputKey.Key == ConsoleKey.RightArrow) // [Right Arrow] Next Page
                    {
                        standardPage.PerformNextPage(pageStack, AppData.mainProducts);
                    }
                    else if (inputKey.Key == ConsoleKey.LeftArrow) // [Left Arrow] Previous Page
                    {
                        standardPage.PerformPreviousPage(pageStack);
                    }
                    break;
            }
        }
        /// <summary>
        /// Prompts user for product to be incremented.
        /// </summary>
        /// <param name="products">Current list of products.</param>
        public void IncrementProduct(List<Product> products)
        {
            Console.Write("Product Name: ");
            string selectedProduct = Console.ReadLine().Trim();

            Product existingProduct = products.Find(product => product.ProductName.ToLower() == selectedProduct.ToLower());

            Console.Write("Enter Quantity: ");
            int quantity = int.Parse(Console.ReadLine());


            _productService.Update(new Product(existingProduct.ProductName, existingProduct.ProductPrice, quantity));

        }
        /// <summary>
        /// Prints text in console with position
        /// </summary>
        /// <param name="value">Value to be printed.</param>
        /// <param name="posX">Sets cursor position in x axis in the console.</param>
        /// <param name="posY">Sets cursor position in y axis in the console.</param>
        public void WriteWithPosition(string value, int posX, int posY)
        {
            Console.SetCursorPosition(posX, posY);
            Console.Write(value);
        }
        /// <summary>
        /// Prints text in console with position
        /// </summary>
        /// <param name="value">Value to be printed.</param>
        /// <param name="posX">Sets cursor position in x axis in the console.</param>
        /// <param name="posY">Sets cursor position in y axis in the console.</param>
        /// <param name="color">Color of text.</param>
        public void WriteWithPosition(string value, int posX, int posY, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(posX, posY);
            Console.Write(value);
            Console.ResetColor();

        }
        /// <summary>
        /// Prompts user to edit a product.
        /// </summary>
        /// <param name="products">Current list of products</param>
        public void EditProduct(List<Product> products)
        {
            Console.CursorVisible = true;
            WriteWithPosition("Product name: ", 15, 41);

            string selectedProduct = Console.ReadLine().Trim();

            Product existingProduct = products.Find(product => product.ProductName.ToLower() == selectedProduct.ToLower());

            if (existingProduct == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                WriteWithPosition("No products were found!", 15, 41);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Thread.Sleep(1200);
                return;
            }

            WriteWithPosition("Select what to edit:", 40, 41);
            WriteWithPosition("[1] Product name", 40, 42);
            WriteWithPosition("[2] Product price", 40, 43);
            Console.CursorVisible = false;


            ConsoleKeyInfo selection = Console.ReadKey(true);
            string newProductName = string.Empty;
            decimal price = 0m;

            // Logic for handling user key input
            switch (selection.KeyChar)
            {
                case '1': // [1] Edit Product Name
                    Console.CursorVisible = true;

                    WriteWithPosition("Enter new product name: ", 15, 45);
                    newProductName = Console.ReadLine().Trim();
                  
                    if (products.Any(product => product.ProductName.ToLower() == newProductName.ToLower()))
                    {
                        Console.WriteLine("Product already exists!");
                    }

                    FileManager.ChangingTxtFileName(AppData.productFolderName, existingProduct.ProductName, newProductName);

                    _productService.Update(new Product(existingProduct.Id, newProductName, existingProduct.ProductPrice, existingProduct.QtyInStock, existingProduct.QtyInCart));
                    break;

                case '2': // [2] Edit Product Price
                    Console.CursorVisible = true;

                    WriteWithPosition("Enter new price: ", 15, 45);
                    
                    if (!decimal.TryParse(Console.ReadLine().Trim(), out price))
                    {
                        WriteWithPosition("Please input a decimal value!", 15, 49, ConsoleColor.Red);
                        Thread.Sleep(1200);
                        break;
                    }
                    _productService.Update(new Product(existingProduct.Id, existingProduct.ProductName, price, existingProduct.QtyInStock, existingProduct.QtyInCart));

                    break;

                default:
                    if (selection.Key == ConsoleKey.Escape)
                    {
                        return;
                    }
                    break;
            }
        }
    }
}

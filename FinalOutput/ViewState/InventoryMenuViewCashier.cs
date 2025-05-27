using FinalOutput.Common.Helper;
using FinalOutput.Interfaces;
using FinalOutput.PageHandling;
using FinalOutput.Services;
using FinalOutput.ViewModels;
using FinalOutput.ViewState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace FinalOutput.State
{
    internal class InventoryMenuViewCashier : MenuView
    {
        private readonly ICostumerAccountGetService _costumerAccountGetService;
        private readonly ProductService _productService;
        private readonly ProductVM productVM;
        private readonly Stack<int> pageStack;
        private bool IsSearching;
        private string searchedItem;
        POSView PosView;
        UserPrompt prompt;


        public InventoryMenuViewCashier(ICostumerAccountGetService costumerAccountGetService)
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
        public override void Display()
        {
            Console.Title = "Inventory Menu (Cashier)";


            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetWindowSize(AppData.width, AppData.height);

            //Admin and Costumer hard coded

            PageHandler standardPage = new PageHandler(new StandardPageBehavior());
            string[] mainMenuItems = { "[1] Stock-In","[2] Search", "[3] Clear Search", "[4] Exit" };


            AppData.accounts = _costumerAccountGetService.GetCostumers();
            AppData.mainProducts = _productService.GetAll().ToList();

            Console.CursorVisible = false;

            Console.Clear();
            if (IsSearching)
            {
                AppData.mainProducts = PosView.FilterProductsByName(AppData.mainProducts, searchedItem);
            }
            var cartProducts = MyCart.GetMyCartProducts().ToList();
            Menu.DisplayProducts(pageStack, AppData.mainProducts, mainMenuItems);

            AsciiArt.PrintInventoryAsciiArt(17, AppData.asciiHeaderY);
            Console.SetCursorPosition(15, 40);

            var inputKey = Console.ReadKey(true);

            

            switch (inputKey.KeyChar)
            {
                case '1': // [1] Stock-In
                    IsSearching = false;
                    productVM.IncrementProduct(AppData.mainProducts);
                    break;

                case '2': // [2] Search
                    IsSearching = true;
                    string input = prompt.UserSearchProduct("Enter the product name to be searched: ", 15, 39, ConsoleColor.Cyan);
                    if (!String.IsNullOrEmpty(input))
                    {
                        searchedItem = input;
                    }
                    else
                    {
                        IsSearching = false;
                    }
                    //searchedItem = prompt.UserSearchProduct("Enter the product name to be searched: ", 15, 39, ConsoleColor.Cyan);
                    break;

                case '3': // [3] Clear Search                 
                    IsSearching = false;
                    break;

                case '4': // [4] Exit
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
    }
}

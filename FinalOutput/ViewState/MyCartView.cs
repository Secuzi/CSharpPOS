using FinalOutput.Common.Helper;
using FinalOutput.PageHandling;
using FinalOutput.Services;
using FinalOutput.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalOutput.ViewState
{
    /// <summary>
    /// Class that displays cart products view.
    /// </summary>
    internal class MyCartView : MenuView
    {
        private readonly ProductService _productService;
        private List<Product> cartProducts;
        private List<Product> mainProducts;

        public MyCartView()
        {
            this._productService = new ProductService(AppData.productFolderName);
            cartProducts = new List<Product>();
            mainProducts = new List<Product>();
        }
        /// <summary>
        /// Displays cart products in console.
        /// </summary>
        public override void Display()
        {
            // Initialization
            Console.Clear();
            Console.CursorVisible = false;
            Stack<int> pageStack = new Stack<int>(new int[] { 0 });

            cartProducts = MyCart.GetMyCartProducts().ToList();
            mainProducts = _productService.GetAll().ToList();

            PageHandler cartPage = new PageHandler(new CartPOSPageBehavior());

            string[] myCartMenuItems = { "[1] Search Product", "[2] Remove Product", "[3] Checkout", "[esc] Back" };

            //Output
            Menu.DisplayProducts(pageStack = new Stack<int>(new int[] { 0 }), cartProducts, myCartMenuItems);
            AsciiArt.PrintMyCartAsciiArt(22, AppData.asciiHeaderY);

            //Create a RemovingProduct function
            var myCartUserInput = Console.ReadKey(true);

            // Logic for handling user key input
            switch (myCartUserInput.KeyChar)
            {
                case '1': // [1] Search
                    //Search
                    break;

                case '2': // [2] Remove product from cart
                    PromptRemoveProductFromCart();
                    break;

                case '3': // [3] Checkout

                    break;

                default:

                    if (myCartUserInput.Key == ConsoleKey.RightArrow)
                    {
                        //Call previous function
                        cartPage.PerformNextPage(pageStack, mainProducts);

                    }
                    else if (myCartUserInput.Key == ConsoleKey.LeftArrow)
                    {
                        //Call next function
                        cartPage.PerformPreviousPage(pageStack);
                    }
                    else if (myCartUserInput.Key == ConsoleKey.Escape)
                    {
                        Menu.SetPreviousView();
                    }

                    break;
            }
        }

        private void PromptRemoveProductFromCart()
        {
            Console.CursorVisible = true;
            Console.SetCursorPosition(AppData.posX, AppData.posY);
            Console.Write("Enter the product: ");

            string productName = Console.ReadLine().Trim();
            Console.SetCursorPosition(AppData.posX, AppData.posY + 1);

            Console.Write("Enter the quantity: ");
            int inputproductQuantity = int.Parse(Console.ReadLine());

            var selectedProduct = cartProducts.Find(product => product.ProductName.ToLower() == productName.ToLower());
            Console.SetCursorPosition(AppData.posX, AppData.posY + 2);

            Console.Write("Remove from cart?: ");

            string yesOrNo = Console.ReadLine().Trim().ToLower();

            //Make a function out of these...

            bool productValidator = ProductQtyValidator.CheckIfProductHasQtyInStock(selectedProduct, inputproductQuantity);

            if (yesOrNo == "y" && productValidator == true)
            {
                var mainProduct = mainProducts.Find(product => product.ProductName.ToLower() == productName.ToLower());

                if (mainProduct == null)
                {
                    selectedProduct.QtyInStock = inputproductQuantity;
                    InventorySystem.AddProductToStorage(new Product(selectedProduct.ProductName, selectedProduct.ProductPrice, inputproductQuantity), InventorySystem.stockFolderPath);

                    selectedProduct.QtyInStock -= inputproductQuantity;

                    //Checking if product quantity is 0
                    if (ProductQtyValidator.CheckIfProductQuantityIsZero(selectedProduct))
                    {
                        MyCart.RemoveProductFromCart(selectedProduct);
                        InventorySystem.DeleteStock(selectedProduct, MyCart.myCartFolderPath);
                    }

                    var myCartProductVM = ProductVMFactory.CreateViewModel(MyCart.myCartFolderPath);

                    myCartProductVM.UpdateProduct(selectedProduct);
                }
                else if (File.Exists(GetFilePath.TextFilePath(InventorySystem.stockFolderPath, mainProduct.ProductName)))
                {
                    //Adding the items to the Main Product path
                    mainProduct.QtyInStock += inputproductQuantity;

                    var inventoryProductVM = ProductVMFactory.CreateViewModel(InventorySystem.stockFolderPath);

                    inventoryProductVM.UpdateProduct(selectedProduct);

                    //Reducing the items in this cart
                    selectedProduct.QtyInStock -= inputproductQuantity;

                    //Checking if product quantity is 0
                    if (ProductQtyValidator.CheckIfProductQuantityIsZero(selectedProduct))
                    {
                        MyCart.RemoveProductFromCart(selectedProduct);
                        InventorySystem.DeleteStock(selectedProduct, MyCart.myCartFolderPath);
                    }

                    var myCartProductVM = ProductVMFactory.CreateViewModel(MyCart.myCartFolderPath);
                    myCartProductVM.UpdateProduct(selectedProduct);

                }


            }
            else if (productValidator == false)
            {
                Console.WriteLine("Invalid operation!");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
        }
    }
}

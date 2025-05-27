
using System.Collections.Generic;
using System.IO;
using System;
using FinalOutput.Interfaces;
using System.Linq;
using System.Threading;
using System.Windows;

namespace FinalOutput.ViewModels
{
    /// <summary>
    /// Class that handles product console functionality.
    /// </summary>
    public class ProductVM
    {
        private readonly IProductService _productService;

        public ProductVM(IProductService productService)
        {
            this._productService = productService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        public void UpdateProduct(Product product)
        {           
            _productService.Update(product);
        }
        /// <summary>
        /// Prompts user for decrementing a quantity of a product.
        /// </summary>
        /// <param name="products"></param>
        public void DecrementProduct(List<Product> products)
        {
            Console.CursorVisible = true;
            Console.Write("Product Name: ");
            string selectedProduct = UserInput.GetLimitedInputWithCancel(20);

            //Returns if the user wants to cancel the input
            if (String.IsNullOrEmpty(selectedProduct))
            {
                return;
            }
            //string selectedProduct = Console.ReadLine().Trim();


            Product existingProduct = products.Find(product => product.ProductName.ToLower() == selectedProduct.ToLower());

            if (existingProduct == null)
            {
                Console.SetCursorPosition(15, 41);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Product not found. Please enter a valid product name.");
                Console.ResetColor();
                Thread.Sleep(1200);
                return;
            }
            int originalCursorTop = Console.CursorTop;
            int originalCursorLeft = Console.CursorLeft;


            Console.SetCursorPosition(originalCursorLeft, originalCursorTop);
            Console.Write(new string(' ', Console.WindowWidth - originalCursorLeft)); // Clear the entire line

            Console.SetCursorPosition(originalCursorLeft + 15, originalCursorTop);
            Console.Write("Enter Quantity: ");


            string quantityInput = Console.ReadLine().Trim();
            // If valid, update product 
            if (int.TryParse(quantityInput, out int quantity) && existingProduct.QtyInStock - int.Parse(quantityInput) >= 0)
            {
                if (quantity > 0)
                {
                    _productService.Update(new Product(existingProduct.Id, existingProduct.ProductName, existingProduct.ProductPrice, existingProduct.QtyInStock - quantity, existingProduct.QtyInCart));

                }
            }
            else
            {
                Console.SetCursorPosition(originalCursorLeft + 15, originalCursorTop + 1);
                Console.WriteLine("Invalid: Input Quantity must be greater than stock quantity!");
                Thread.Sleep(1200);
            }

            Console.CursorVisible = false;
        }




        /// <summary>
        /// Prompts user for incrementing a quantity of a product.
        /// </summary>
        /// <param name="products"></param>
        public void IncrementProduct(List<Product> products)
        {
            Console.CursorVisible = true;
            Console.Write("Product Name: ");
            string selectedProduct = UserInput.GetLimitedInputWithCancel(20);

            //Returns if the user wants to cancel the input
            if (String.IsNullOrEmpty(selectedProduct))
            {
                return;
            }
            //string selectedProduct = Console.ReadLine().Trim();
            

            Product existingProduct = products.Find(product => product.ProductName.ToLower() == selectedProduct.ToLower());

            if (existingProduct == null)
            {
                Console.SetCursorPosition(15, 41);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Product not found. Please enter a valid product name.");
                Console.ResetColor();
                Thread.Sleep(1200);
                return;
            }
            int originalCursorTop = Console.CursorTop;
            int originalCursorLeft = Console.CursorLeft;

                        
            Console.SetCursorPosition(originalCursorLeft, originalCursorTop);
            Console.Write(new string(' ', Console.WindowWidth - originalCursorLeft)); // Clear the entire line

            Console.SetCursorPosition(originalCursorLeft + 15, originalCursorTop);
            Console.Write("Enter Quantity: ");
            string quantityInput = Console.ReadLine().Trim();
            //if (int.TryParse(quantityInput, out int quantity) && quantity > existingProduct.QtyInStock)
            // If valid, update product 
            if (int.TryParse(quantityInput, out int quantity))
            {
                if (quantity > 0)
                {
                    _productService.Update(new Product(existingProduct.Id, existingProduct.ProductName, existingProduct.ProductPrice, quantity + existingProduct.QtyInStock, existingProduct.QtyInCart));

                }
              
            }
            else
            {
                Console.SetCursorPosition(originalCursorLeft + 15, originalCursorTop + 1);
                Console.WriteLine("Invalid: Input Quantity must be greater than stock quantity!");                
                Thread.Sleep(1200);
            }

            Console.CursorVisible = false;
        }
        /// <summary>
        /// Prompts user for editing a product.
        /// </summary>
        /// <param name="products"></param>
        public void EditProduct(List<Product> products)
        {
            Console.Write("Product name: ");
            string selectedProduct = Console.ReadLine().Trim();

            Product existingProduct = products.Find(product => product.ProductName.ToLower() == selectedProduct.ToLower());

            if (existingProduct == null)
            {
                Console.WriteLine("Product not found. Please enter a valid product name.");
                return;
            }

            Console.Write("Enter price: ");
            decimal price;

            int originalCursorTopPrice = Console.CursorTop;
            int originalCursorLeftPrice = Console.CursorLeft;

            while (true)
            {
                Console.SetCursorPosition(originalCursorLeftPrice, originalCursorTopPrice);
                Console.Write(new string(' ', Console.WindowWidth - originalCursorLeftPrice)); // Clear the entire line
                Console.SetCursorPosition(originalCursorLeftPrice, originalCursorTopPrice);

                string priceInput = Console.ReadLine();

                try
                {
                    price = decimal.Parse(priceInput);
                    if (price < 0)
                    {
                        Console.SetCursorPosition(originalCursorLeftPrice, originalCursorTopPrice + 1);
                    }
                    else
                    {
                        break; // Valid input, exit the loop
                    }
                }
                catch
                { 
                    Console.SetCursorPosition(originalCursorLeftPrice, originalCursorTopPrice + 1);
                }
            }

            Console.Write("Enter quantity: ");
            int quantity;

            int originalCursorTopQuantity = Console.CursorTop;
            int originalCursorLeftQuantity = Console.CursorLeft;

            while (true)
            {
                Console.SetCursorPosition(originalCursorLeftQuantity, originalCursorTopQuantity);
                Console.Write(new string(' ', Console.WindowWidth - originalCursorLeftQuantity)); // Clear the entire line
                Console.SetCursorPosition(originalCursorLeftQuantity, originalCursorTopQuantity);

                string quantityInput = Console.ReadLine();

                try
                {
                    quantity = int.Parse(quantityInput);
                    if (quantity < 0)
                    {
                        Console.SetCursorPosition(originalCursorLeftQuantity, originalCursorTopQuantity + 1);
                    }
                    else
                    {
                        break; // Valid input, exit the loop
                    }
                }
                catch
                {
                    Console.SetCursorPosition(originalCursorLeftQuantity, originalCursorTopQuantity + 1);
                }
            }

            if (quantity > existingProduct.QtyInStock)
            {
                _productService.Update(new Product(existingProduct.Id, existingProduct.ProductName, price, quantity, existingProduct.QtyInCart));
            }
        }

        /// <summary>
        /// Prompts a user in console for adding a product.
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public Product CreateProduct(List<Product> products)
        {
            string enterProdName = "Enter product name: ";
            string enterProdPrice = "Enter product price: ";
            Console.Write(enterProdName);
            Console.SetCursorPosition(AppData.posX + enterProdName.Length, AppData.posY);

            string productName = Console.ReadLine().Trim();

            //If product already exists
            if (_productService.GetProductByName(productName) != null)
            {
                Console.WriteLine("Invalid. Product already exists.");
                Thread.Sleep(1200);
                return null;
            }
            Console.SetCursorPosition(AppData.posX, AppData.posY + 1);

            Console.Write(enterProdPrice);

            decimal productPrice;

            while (true)
            {
                Console.SetCursorPosition(AppData.posX + enterProdPrice.Length, AppData.posY + 1);
                Console.Write(new string(' ', Console.WindowWidth - (AppData.posX + enterProdPrice.Length))); // Clear the entire line
                Console.SetCursorPosition(AppData.posX + enterProdPrice.Length, AppData.posY + 1);

                string priceInput = Console.ReadLine();

                try
                {
                    productPrice = decimal.Parse(priceInput);
                    if (productPrice < 0)
                    {
                        Console.SetCursorPosition(AppData.posX, AppData.posY + 2);
                    }
                    else
                    {
                        break; // Valid input, exit the loop
                    }
                }
                catch
                { 
                    Console.SetCursorPosition(AppData.posX, AppData.posY + 2);
                }
            }

            // Put in a different module
            if (products.Any(product => product.ProductName.ToLower() == productName.ToLower()))
            {
                return null;
            }
            if (String.IsNullOrEmpty(productName))
            {
                return null;
            }

            // Get character for the first letter in the product name then add 32 to capitalize it
            return new Product(productName, productPrice);
        }


        /// <summary>
        /// Gets the products in the StockFile folder
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetProducts()
        {
            return _productService.GetAll();

        }
    }
}

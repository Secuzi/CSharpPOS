using FinalOutput.Common.Helper;
using FinalOutput.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FinalOutput
{
    /// <summary>
    /// Class that handles Cart Product Create, Read, Update, Delete (CRUD) functionalities.
    /// </summary>
    public static class MyCart
    {
        public static LinkedList<Product> CartProducts { get; private set; } = new LinkedList<Product>();

        public static decimal TotalCost { get; private set; } = 0;

        public readonly static string myCartFolderPath = @"MyCart";
        /// <summary>
        /// Retrives user cart products in the application.
        /// </summary>
        /// <returns>List of products in the cart.</returns>
        public static IEnumerable<Product> GetMyCartProducts()
        {
            string path = GetFilePath.FilePath(myCartFolderPath);

            var txtFiles = Directory.EnumerateFiles(path, "*.txt");

            var line = FileManager.IterateTextFiles(txtFiles);

            var mainCartProducts = GetMainProducts();
            var myCartProducts = new List<Product>();

            foreach (var property in line)
            {
                string guid = property[0];
                myCartProducts.Add(new Product(Guid.Parse(guid), property[1], decimal.Parse(property[2]), int.Parse(property[3]), int.Parse(property[4])));
            }

            // Return products that are common in both main and my cart
            return mainCartProducts.Intersect(myCartProducts, new ProductComparer()).ToList();
        }
        /// <summary>
        /// Adds product to user cart products.
        /// </summary>
        /// <param name="product">The product to add in the cart products.</param>
        public static void AddToCart(Product product)
        {
            CartProducts.AddLast(product);
            InventorySystem.AddProductToStorage(product, myCartFolderPath);
        }
        /// <summary>
        /// Removes product from user cart products.
        /// </summary>
        /// <param name="product">The product to remove.</param>
        public static void RemoveProductFromCart(Product product)
        {
            CartProducts.Remove(product);
            InventorySystem.RemoveProductFromInventory(product.ProductName, myCartFolderPath);
        }

        /// <summary>
        /// Returns all products in cart.
        /// </summary>
        /// <returns>List of products in cart.</returns>
        private static List<Product> GetMainProducts()
        {
            var productService = new ProductService(AppData.productFolderName);

            return productService.GetAll().ToList();
        }
    }
}

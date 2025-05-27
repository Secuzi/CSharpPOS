using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using Microsoft.SqlServer.Server;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Text;
using FinalOutput.ViewModels;
using FinalOutput.Services;
using FinalOutput.Interfaces;

namespace FinalOutput
{
    /// <summary>
    /// Class that handles creation, updation, and removal of products in inventory.
    /// </summary>
    public class InventorySystem
    {
        /// <summary>
        /// Default folder path of the inventory.
        /// </summary>
        public readonly static string stockFolderPath = @"StockFile";       

        /// <summary>
        /// Removes a product from the inventory. 
        /// </summary>
        /// <param name="productName">Name of product to be removed.</param>
        /// <param name="folderPath">Folder path of the products in the inventory.</param>
        public static void RemoveProductFromInventory(string productName, string folderPath)
        {
            // Initialization
            var productService = new ProductService(stockFolderPath);
            var existingProduct = productService.GetProductByName(productName);

            // If product exists, delete product
            if (existingProduct != null)
            {
                DeleteStock(existingProduct, folderPath);
            }

        }                
        /// <summary>
        /// Creates the product in the inventory.
        /// </summary>
        /// <param name="product">The product to be added.</param>
        /// <param name="folderPath">Folder path of the products in the inventory.</param>
        public static void AddProductToStorage(Product product, string folderPath)
        {
            //Creates textfile using product name
            string path = GetFilePath.TextFilePath(folderPath, product.ProductName);

            FileManager.CreateTextFile(path);
            FileManager.WriteTextFile(path, $"{product.Id},{product.ProductName},{product.ProductPrice},{product.QtyInStock},{product.QtyInCart}");

        }


        /// <summary>
        /// Deletes the stock in the StockFile folder.
        /// </summary>
        /// <param name="product">The product to be removed.</param>
        /// <param name="folderPath">Folder path of the products in the inventory.</param>
        public static void DeleteStock(Product product, string folderPath)
        {
            
            string path = GetFilePath.TextFilePath(folderPath, product.ProductName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }      
    }
}

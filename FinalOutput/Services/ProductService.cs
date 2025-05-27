
using FinalOutput.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace FinalOutput.Services
{
    /// <summary>
    /// Class for product Create, Read, Update, Delete (CRUD) functionality in the application.
    /// </summary>
    public class ProductService : IProductService
    {
        private string _dBFilePath;
        /// <summary>
        /// Default constructor of ProductService
        /// </summary>
        /// <param name="dbFilePath">e.g. AppData.mainFolderPath, InventorySystem.stockFolderPath</param>

        public ProductService(string dbFilePath)
        {
            this._dBFilePath = dbFilePath;
        }
        /// <remarks>Not Implemented: Do not Use.</remarks>
        /// <exception cref="System.NotImplementedException"></exception>
        public Product Add(Product product)
        {
            throw new System.NotImplementedException();
        }
        /// <remarks>Not Implemented: Do not Use.</remarks>
        /// <exception cref="System.NotImplementedException"></exception>
        public Product Delete(Product product)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Retrieves all products in the application.
        /// </summary>
        public IEnumerable<Product> GetAll()
        {
            string path = GetFilePath.FilePath(_dBFilePath);

            var txtFiles = Directory.EnumerateFiles(path, "*.txt");

            var line = FileManager.IterateTextFiles(txtFiles);

            foreach (var property in line)
            {
                yield return new Product( Guid.Parse(property[0]) ,property[1], decimal.Parse(property[2]), int.Parse(property[3]), int.Parse(property[4]));
            }
        }
        /// <summary>
        /// Example: var productsWithStock = ProductService.GetFilteredProducts(x => x.ProductQuantity > 0);
        /// </summary>
        /// <param name="expression">The conditional expression for filtering.</param>
        /// <returns>The filtered products.</returns>
        public IEnumerable<Product> GetFilteredProducts(Func<Product, bool> expression)
        {
            return AppData.mainProducts.Where(expression).ToList();
        }
        /// <summary>
        /// Retrieves product in application with corresponding name.
        /// </summary>
        /// <param name="productName">The name of the product to be searched.</param>
        /// <returns>The product in application with corresponding name.</returns>
        public Product GetProductByName(string productName)
        {
            var existingProduct = AppData.mainProducts.Find(product => product.ProductName.ToLower() == productName.ToLower());
            return existingProduct;
        }
        /// <summary>
        /// Updates product in the application.
        /// </summary>
        /// <param name="product">The new product.</param>
        /// <returns>The product that has been edited.</returns>
        /// <exception cref="ArgumentException">Throws if product is invalid</exception>
        public Product Update(Product product)
        {
            string path = GetFilePath.TextFilePath(_dBFilePath, product.ProductName);

            FileManager.WriteTextFile(path, $"{product.Id},{product.ProductName},{product.ProductPrice},{product.QtyInStock},{product.QtyInCart}");

            return product;
        }
    }
}

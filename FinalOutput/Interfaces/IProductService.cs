
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FinalOutput.Interfaces
{
    /// <summary>
    /// Interface for product Create, Read, Update, Delete (CRUD) functionality.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Adds product in the application.
        /// </summary>
        /// <param name="product">The product to be added.</param>
        /// <returns>The product that has been added.</returns>
        /// <exception cref="ArgumentException"></exception>
        Product Add(Product product);
        /// <summary>
        /// Updates product in the application.
        /// </summary>
        /// <param name="product">The new product.</param>
        /// <returns>The product that has been edited.</returns>
        /// <exception cref="ArgumentException">Throws if product is invalid</exception>
        Product Update(Product product);
        /// <summary>
        /// Deletes product in the application.
        /// </summary>
        /// <param name="product">The product to be deleted.</param>
        /// <returns>The product that has been deleted.</returns>
        /// <exception cref="ArgumentException">Throws if product is invalid.</exception>
        Product Delete(Product product);
        /// <summary>
        /// Retrieves product in application with corresponding name.
        /// </summary>
        /// <param name="productName">The name of the product to be searched.</param>
        /// <returns>The product in application with corresponding name.</returns>

        Product GetProductByName(string productName);
        /// <summary>
        /// Retrieves products with the corresponding conditional expression.
        /// </summary>
        /// <param name="expression">The conditional expression for filtering.</param>
        /// <returns>The filtered products.</returns>
        IEnumerable<Product> GetFilteredProducts(Func<Product, bool> expression);
        /// <summary>
        /// Retrieves all products in the application.
        /// </summary>
        IEnumerable<Product> GetAll();
    }
}

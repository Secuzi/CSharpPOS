using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalOutput
{
    /// <summary>
    /// Class that handles product quantity validation.
    /// </summary>
    public class ProductQtyValidator
    {
        /// <summary>
        /// Checks if product quantity is zero.
        /// </summary>
        /// <param name="product"></param>
        /// <returns>True if product quantity equals zero; otherwise, false.</returns>
        public static bool CheckIfProductQuantityIsZero(Product product)
        {
            return product.QtyInStock == 0;
        }
        /// <summary>
        /// Checks if product quantity is more than or equal to given quantity.
        /// </summary>
        /// <param name="product"></param>
        /// <returns>True if product quantity is equal or more than given quantity; otherwise, false.</returns>
        public static bool CheckIfProductHasQtyInStock(Product selectedProduct, int quantity)
        {
            return selectedProduct.QtyInStock >= quantity;
        }
    }
}

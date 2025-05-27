
using System.Collections.Generic;

namespace FinalOutput.Common.Helper
{
    /// <summary>
    /// Class that defines comparison method for product.
    /// </summary>
    public class ProductComparer : IEqualityComparer<Product>
    {
        /// <summary>
        /// Compares if product IDs match.
        /// </summary>
        /// <returns>True if IDs match; otherwise, false.</returns>
        public bool Equals(Product x, Product y)
        {
            return x.Id == y.Id;
        }
        /// <summary>
        /// Returns hashcode of ID of product.
        /// </summary>
        public int GetHashCode(Product obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace FinalOutput
{
    /// <summary>
    /// Class that handles transaction fo product
    /// </summary>
    public class ProductTransaction
    {
        public decimal CashOnHand { get; private set; }

        public decimal TotalValue { get; set; } 

                
        /// <summary>
        /// Calculates total amount from the list of products.
        /// </summary>
        /// <param name="products">List of products to sum total.</param>
        public void CalculateTotal(List<Product> products)
        {
            int left = 0;
            int right = products.Count - 1;
            
            while (left <= right)
            {
                if (left == right)
                {
                    TotalValue += products[left].GetTotalPrice();
                    break;
                }
                TotalValue += products[left].GetTotalPrice() + products[right].GetTotalPrice();

                left++;
                right--;
            }
        }

    }
}

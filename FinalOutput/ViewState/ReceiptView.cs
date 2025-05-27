
using FinalOutput.ViewModels;
using System;
using System.Collections.Generic;

namespace FinalOutput.ViewState
{
    /// <summary>
    /// Class that handles displaying Receipt for bought products.
    /// </summary>
    public class ReceiptView : MenuView
    {
        public decimal CashOnHand { get; set; }        
        private readonly IEnumerable<Product> boughtProducts;

        /// <summary>
        /// Default constructor of ReceiptView
        /// </summary>
        /// <param name="boughtProducts">List of products to be calculated and displayed in receipt.</param>
        public ReceiptView(IEnumerable<Product> boughtProducts)
        {
            this.boughtProducts = boughtProducts;
        }

        /// <summary>
        /// Displays receipt in the console.
        /// </summary>
        public override void Display()
        {
            Console.Clear(); // Clear the console for a clean display

            Console.WriteLine("_______________________________________________________________________________________________");
            Console.WriteLine("|                                                                                             |");
            Console.WriteLine("|                                  *****SUPPORT LOCAL SHOP*****                               |");
            Console.WriteLine("|                                                                                             |");
            Console.WriteLine("|                                        OFFICIAL RECEIPT                                     |");
            Console.WriteLine("|---------------------------------------------------------------------------------------------|");
            Console.WriteLine("| Items Purchased:                                                                            |");
            Console.WriteLine("|                                                                                             |");

            // Displays each products information
            foreach (var product in boughtProducts)
            {
                Console.WriteLine($"|\t {product.ProductName,-63} {product.GetTotalPrice(),-20:C} |");
            }

            Console.WriteLine("|---------------------------------------------------------------------------------------------|");
            Console.WriteLine("|                                                                                             |");
            Console.WriteLine($"| {"Total:",-70} {CalculateTotal(),-20:C} |");
            Console.WriteLine($"| {"Cash on Hand:",-70} {CashOnHand,-20:C} |");
            Console.WriteLine($"| {"Change:",-70} {CalculateChange(),-20:C} |");
            Console.WriteLine("|---------------------------------------------------------------------------------------------|");
            Console.WriteLine("| Payment Details:                                                                            |");
            Console.WriteLine("|                                                                                             |");
            Console.WriteLine($"| {"\tPayment Method:",-65} {"Cash",-20} |");
            Console.WriteLine($"| {"\tTransaction Date:",-65} {DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),-20} |");
            Console.WriteLine("|---------------------------------------------------------------------------------------------|");
            Console.WriteLine("|                                                                Thank you for your purchase! |");
            Console.WriteLine("|_____________________________________________________________________________________________|");

            Console.ReadKey();
            Console.Clear();

            // Boots back to the main manu
            Menu.SetDisplayView(new AdminMenuView());
        }


        /// <summary>
        /// Calculates the Total price from product.
        /// </summary>
        /// <returns></returns>
        private decimal CalculateTotal()
        {
            decimal totalPrice = 0;

            foreach (var product in boughtProducts)
            {
                totalPrice += product.GetTotalPrice();
            }

            return totalPrice;
        }

        /// <summary>
        /// Subtract totalPrice from CashOnHand to calculate the change.
        /// </summary>
        /// <returns>Total change.</returns>
        private decimal CalculateChange()
        {
            return CashOnHand - CalculateTotal();
        }
    }
}

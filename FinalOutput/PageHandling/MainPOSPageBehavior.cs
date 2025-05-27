using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalOutput.PageHandling
{
    /// <summary>
    /// Class that handles main page navigation in POS
    /// </summary>
    public class MainPOSPageBehavior : IPageHandlerBehavior
    {
        /// <summary>
        /// Returns number of pages from list of product
        /// </summary>
        public int GetPage(List<Product> products)
        {
            if (products.Count % 9 == 0)
            {
                return products.Count / 9;
            }
            else
            {
                decimal count = Convert.ToDecimal(products.Count);
                return (int)Math.Ceiling(count / 9m);
            }

        }
        /// <summary>
        /// Checks if cart page is full. 
        /// </summary>
        /// <returns>True if page is full; otherwise, false.</returns>
        public bool CheckIfPageIsFull(List<Product> mainProducts)
        {

            return (mainProducts.Count % 9 == 0 && mainProducts.Count != 0) ? true : false;

        }

        /// <summary>
        /// Navigates to next page of cart products.
        /// </summary>
        /// <param name="pageStack">The current page stack.</param>
        /// <param name="products">The current list of cart products.</param>
        public void NextPage(Stack<int> pageStack, List<Product> products)
        {

            int count = products.Count;
            int currentPage = pageStack.Peek(); ///
            int nextPage = currentPage + 9;

            if (nextPage < count)
            {
                pageStack.Push(nextPage);
            }
        }
        /// <summary>
        /// Navigates to previous page of cart products.
        /// </summary>
        /// <param name="pageStack">The current page stack.</param>
        public void PreviousPage(Stack<int> pageStack)
        {
            if (pageStack.Count > 1)
            {
                pageStack.Pop();
            }
        }
        /// <summary>
        /// Prints page in the console.
        /// </summary>
        /// <param name="currentPage">The current page number.</param>
        /// <param name="page">The total number of pages.</param>
        /// <param name="posX">Sets cursor position in x axis in the console.</param>
        /// <param name="posY">Sets cursor position in y axis in the console.</param>
        public void PrintPage(int currentPage, int page, int posX, int posY)
        {
            Console.SetCursorPosition(posX, posY);
            Console.Write($"{currentPage / 9 + 1} / {page}");

        }
    }
}

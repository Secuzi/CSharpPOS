using FinalOutput.PageHandling;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FinalOutput
{
    /// <summary>
    /// Class that handles page navigation in the application.
    /// </summary>
    public class PageHandler
    {

        IPageHandlerBehavior _page;

        public PageHandler(IPageHandlerBehavior page)
        {
            _page = page;

        }
        /// <summary>
        /// Returns number of pages from list of product.
        /// </summary>
        public int PerformGetPage(List<Product> products)
        {
            return this._page.GetPage(products);
        }
        /// <summary>
        /// Prints current page.
        /// </summary>
        /// <param name="currentPage">The current page number.</param>
        /// <param name="page">The total number of pages.</param>
        /// <param name="posX">Sets cursor position in x axis in the console.</param>
        /// <param name="posY">Sets cursor position in y axis in the console.</param>
        public void PerformPrintPage(int currentPage, int page, int posX, int posY)
        {
            this._page.PrintPage(currentPage, page, posX, posY);
        }


        /// <summary>
        /// Checks if  page is full. 
        /// </summary>
        /// <returns>True if page is full; otherwise, false.</returns>
        public bool PerformCheckIfPageIsFull(List<Product> products)
        {
            return this._page.CheckIfPageIsFull(products);
        }
        /// <summary>
        /// Navigates to next page.
        /// </summary>
        /// <param name="pageStack">The current page stack.</param>
        /// <param name="products">The current list of cart products.</param>
        public void PerformNextPage(Stack<int> pageStack, List<Product> products)
        {
            this._page.NextPage(pageStack, products);
        }
        /// <summary>
        /// Navigates to previous page.
        /// </summary>
        /// <param name="pageStack">The current page stack.</param>
        public void PerformPreviousPage(Stack<int> pageStack)
        {
            this._page.PreviousPage(pageStack);
        }
        /// <summary>
        /// Sets the page behavior of the class.
        /// </summary>
        public void SetPageBehavior(IPageHandlerBehavior page)
        {
            _page = page;
        }
        


    }
}

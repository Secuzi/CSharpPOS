using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalOutput.PageHandling
{
    public interface IPageHandlerBehavior
    {
        int GetPage(List<Product> products);
        bool CheckIfPageIsFull(List<Product> mainProducts);
        void NextPage(Stack<int> pageStack, List<Product> products);

        void PreviousPage(Stack<int> pageStack);

        void PrintPage(int currentPage, int page, int posX, int posY);


    }
}

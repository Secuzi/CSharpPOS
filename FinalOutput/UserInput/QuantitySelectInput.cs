using FinalOutput.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalOutput
{
    /// <summary>
    /// Class that handles key 
    /// </summary>
    public class QuantitySelectInput : IUserInputService
    {
        public Product SelectedProduct { get; set; }
        public ConsoleKey GetInput()
        {
            var userInput = Console.ReadKey();

            switch (userInput.Key)
            {
                case ConsoleKey.A: 
                    if (SelectedProduct.QtyInStock > 0)
                    {
                        SelectedProduct.QtyInCart--;
                    }
                return ConsoleKey.A;
                case ConsoleKey.D:
                    if (SelectedProduct.QtyInStock < 999)
                    {
                        SelectedProduct.QtyInCart++;
                    }
                    return ConsoleKey.D;

                case ConsoleKey.Enter:
                    


                    break;


            }

            return ConsoleKey.N;

        }
    }
}

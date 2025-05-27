using FinalOutput.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalOutput
{
    /// <summary>
    /// Class that handles key input for user cart
    /// </summary>
    public class MyCartSelectInput : IUserInputService
    {

        public int SelectedIndexI { get; set; }
        public int CartProdCounter { get; set; }

        public ConsoleKey GetInput()
        {
            ConsoleKeyInfo userInput;
            do
            {
                userInput = Console.ReadKey(true);

            } while (Console.KeyAvailable);

            // Logic for handling user key input
            switch(userInput.Key)
            {
                case ConsoleKey.DownArrow:
                    if (SelectedIndexI < CartProdCounter)
                    SelectedIndexI++;
                    return ConsoleKey.DownArrow;

                case ConsoleKey.UpArrow:

                    if (SelectedIndexI > 0)
                    {
                        SelectedIndexI--;
                    }
                    return ConsoleKey.UpArrow;
                default:
                    return userInput.Key;
                    

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinalOutput
{
    /// <summary>
    /// Class that handles default prompts to user.
    /// </summary>
    public class UserPrompt
    {
        /// <summary>
        /// Prompts user for YES/NO input in the console.
        /// </summary>
        /// <param name="prompt">The text query the be displayed.</param>
        /// <param name="posX">Sets cursor position in x axis in the console.</param>
        /// <param name="posY">Sets cursor position in y axis in the console.</param>
        /// <param name="textColor"></param>
        /// <returns>True if input is "Y" or "y"; otherwise, false if "N" or "n".</returns>
        public bool UserSelectConfirmation(string prompt, int posX, int posY, ConsoleColor textColor)
        {
        Start:
            Console.CursorVisible = true;
            Console.SetCursorPosition(posX, posY);

            Console.Write(new string(' ', prompt.Length + 30));

            Console.SetCursorPosition(posX, posY);
            Console.ForegroundColor = textColor;
            Console.Write($"{prompt}");
            string input = Console.ReadLine().ToLower().Trim();
            Console.ResetColor();
            if (input == "y")
            {
                return true;
            }
            else if (input == "n")
            {
                return false;
            }
            else{

                goto Start;
            
            }
        }

        /// <summary>
        /// Prompt user for product name to be searched in the console.
        /// </summary>
        /// <param name="prompt"></param>
        /// <param name="posX">Sets cursor position in x axis in the console.</param>
        /// <param name="posY">Sets cursor position in y axis in the console.</param>
        /// <param name="textColor">Sets color of text.</param>
        /// <returns>The name of product to be searched.</returns>
        public string UserSearchProduct(string prompt, int posX, int posY, ConsoleColor textColor)
        {
            Console.CursorVisible = true;
            Console.CursorVisible = true;
            Console.SetCursorPosition(posX, posY);

            Console.Write(new string(' ', prompt.Length + 2));

            Console.SetCursorPosition(posX, posY);
            Console.ForegroundColor = textColor;
            Console.WriteLine($"{prompt}");
            Console.SetCursorPosition(posX, posY + 1);
            string input = UserInput.GetLimitedInputWithCancel(20);
            //string input = Console.ReadLine().ToLower();

            return input;
        }

    }
}

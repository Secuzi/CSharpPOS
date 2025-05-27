using FinalOutput.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalOutput
{
    public class UserInput
    {
        protected IUserInputService _userInputService;


        //public bool PerformCatchInput(ref int selectedIndexI, ref int selectedIndexJ, int countMax, int row)
        //{
        //    return _userInputService.GetInput(ref selectedIndexI, ref selectedIndexJ, countMax, row);
        //    return _userInputService.GetInput();
        //}
        public ConsoleKey PerformCatchInput()
        {
            return _userInputService.GetInput();
        }


        public static string GetLimitedInputWithCancelDigits(int maxLength)
        {
            StringBuilder inputBuffer = new StringBuilder();
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // Check if the key is a valid digit and the length limit is not exceeded
                if (char.IsDigit(key.KeyChar) && inputBuffer.Length < maxLength)
                {
                    Console.Write(key.KeyChar);
                    inputBuffer.Append(key.KeyChar);
                }
                else if (key.Key == ConsoleKey.Backspace && inputBuffer.Length > 0)
                {
                    // Handle backspace to erase the last character
                    Console.Write("\b \b");
                    inputBuffer.Length--;
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    // Return null to indicate input cancellation
                    return null;
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine(); // Move to the next line after user input

            return inputBuffer.ToString();
        }

        public static string GetLimitedInputWithCancel(int maxLength)
        {
            StringBuilder inputBuffer = new StringBuilder();
            ConsoleKeyInfo key;
            do
            {
                Console.CursorVisible = true;

                key = Console.ReadKey(true);

                // Check if the key is a valid digit and the length limit is not exceeded
                if (inputBuffer.Length < maxLength && key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Escape && key.Key != ConsoleKey.Enter)
                {
                    Console.Write(key.KeyChar);
                    inputBuffer.Append(key.KeyChar);
                }
                else if (key.Key == ConsoleKey.Backspace && inputBuffer.Length > 0)
                {
                    // Handle backspace to erase the last character
                    Console.Write("\b \b");
                    inputBuffer.Length--;
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    // Return null to indicate input cancellation
                    return null;
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine(); // Move to the next line after user input

            return inputBuffer.ToString();
        }

        public void SetInputBehavior(IUserInputService userInputService)
        {
            this._userInputService = userInputService;
        }
        
    }
}

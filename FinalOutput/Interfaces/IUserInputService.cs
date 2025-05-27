using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalOutput.Interfaces
{
    /// <summary>
    /// Interface for handling user key input
    /// </summary>
    public interface IUserInputService
    {
        /// <summary>
        /// Returns <see cref="ConsoleKey"/> input from user
        /// </summary>
        ConsoleKey GetInput();
    }
}

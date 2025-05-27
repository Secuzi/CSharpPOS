
using System.Collections.Generic;

namespace FinalOutput.Interfaces
{
    /// <summary>
    /// An interface for retrieving costumer accounts in the application.
    /// </summary>
    public interface ICostumerAccountGetService
    {
        /// <summary>
        /// Retrieves costumer accounts in the application.
        /// </summary>
        /// <returns>List of costumer accounts in the application.</returns>
        IEnumerable<Costumer> GetCostumers();
    }
}

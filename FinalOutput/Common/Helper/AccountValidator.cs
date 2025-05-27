using FinalOutput.Interfaces;
using FinalOutput.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalOutput
{
    /// <summary>
    /// Class that handles account validation of user
    /// </summary>
    public static class AccountValidator
    {    
        private static ICostumerAccountGetService _costumerGetService = new CostumerAccountGetService();
        /// <summary>
        /// Validates if the account is registered.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>True if account exist; otherwise, false.</returns>
        public static bool Login(string username, string password)
        {
            var costumerList = _costumerGetService.GetCostumers();

            return costumerList.Any(account => account.Username.ToLower() == username.ToLower() && account.Password == password);
        }

        /// <summary>
        /// Checks if account of costumer type exist.
        /// </summary>
        /// <param name="username">The username of the costumer account.</param>
        /// <returns>True if account exist; otherwise, false.</returns>
        public static bool DoesCostumerAccountExist(string username)
        {
            var costumerList = _costumerGetService.GetCostumers();

            return costumerList.Any(user => user.Username.ToLower() == username.ToLower());

        }
        
    }
}

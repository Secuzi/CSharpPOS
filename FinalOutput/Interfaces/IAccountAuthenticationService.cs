using FinalOutput.Models;
using System;

namespace FinalOutput.Interfaces
{
    /// <summary>
    /// An interface for account authentication funcitonality.
    /// </summary>
    public interface IAccountAuthenticationService
    {
        /// <summary>
        /// Throws an UnauthorizedAccessException if the account is not authenticated.
        /// </summary>
        /// <param name="account"></param>
        /// <exception cref="UnauthorizedAccessException" >Thrown when the account is not authenticated.</exception>
        void LoginAccount(Account account);
        /// <summary>
        /// Logs out an account from the application.
        /// </summary>
        void LogoutAccount();
        /// <summary>
        /// Returns logged in account
        /// </summary>
        /// <returns>The account of the logged in user</returns>
        /// <exception cref="UnauthorizedAccessException">throws exception if account is not logged in.</exception>
        Account GetAuthenticatedAccount();
    }
}

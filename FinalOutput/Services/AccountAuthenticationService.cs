using FinalOutput.Interfaces;
using FinalOutput.Models;
using System;
using System.Security.Principal;

namespace Ecommerce.Core.Services
{
    /// <summary>
    /// Class that handles account authentication funcitonality.
    /// </summary>
    public class AccountAuthenticationService : IAccountAuthenticationService
    {
        private readonly Admin adminAccount;
        private readonly Cashier cashierAccount;
        private Account loggedInAccount;
        public AccountAuthenticationService()
        {
            adminAccount = new Admin("admin", "admin");
            cashierAccount = new Cashier("cashier", "cashier");
        }

        public Account GetAuthenticatedAccount()
        {
            if (loggedInAccount == null)
                throw new UnauthorizedAccessException();

            return loggedInAccount;
        }

        public void LoginAccount(Account account)
        {
            if (account.Password == null || account.Username == null)
            {
                throw new Exception("username and/or password must not be null.");
            }

            if (IsAdminAccount(account))
            {
                loggedInAccount = adminAccount;
            }
            else if (IsCashierAccount(account))
            {
                loggedInAccount = cashierAccount;
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public void LogoutAccount()
        {
            loggedInAccount = null;
        }
        /// <summary>
        /// Checks if user type of account is admin
        /// </summary>
        /// <param name="account"></param>
        /// <returns>True if account is admin type; otherwise, false.</returns>

        private bool IsAdminAccount(Account account)
        {
            return account.Username == adminAccount.Username && account.Password == adminAccount.Password;  //admin account                
        }
        /// <summary>
        /// Checks if user type of account is cashier
        /// </summary>
        /// <param name="account"></param>
        /// <returns>True if account is cashier type; otherwise, false.</returns>

        private bool IsCashierAccount(Account account)
        {
            return account.Username == cashierAccount.Username && account.Password == cashierAccount.Password;  //cashier account
        }
    }
}

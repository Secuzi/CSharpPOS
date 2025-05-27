using FinalOutput.Interfaces;
using FinalOutput.Models;
using System;

namespace FinalOutput
{
    /// <summary>
    /// Class that handles user console login.
    /// </summary>
    public class LoginVM
    {
        private readonly IAccountAuthenticationService authService;

        public LoginVM(IAccountAuthenticationService accountAuthenticationService)
        {
            this.authService = accountAuthenticationService;
        }
        /// <summary>
        /// Prompts user for login in the application.
        /// </summary>
        /// <exception cref="UnauthorizedAccessException">Throws exception if user is unauthorized.</exception>
        public void PromptUserLogin()
        {
            
            Console.CursorVisible = true;
            Console.Clear();
            
            Console.Write("Enter username: ");
            string username = Console.ReadLine().Trim();

            Console.Write("Enter password: ");
            string password = Console.ReadLine().Trim();

            var account = new AuthenticationAccount(username, password);

            try
            {
                authService.LoginAccount(account);
            }
            catch(UnauthorizedAccessException)
            {
                throw;
            }

        }
        public eUserType GetUserType()
        {
            try
            {
                var loggedInAccount = authService.GetAuthenticatedAccount();
                return loggedInAccount.UserType;
            }
            catch(UnauthorizedAccessException)
            {
                throw;
            }
        }

    }
}

using System;
using FinalOutput.Interfaces;
using FinalOutput.Models;

namespace FinalOutput.Services
{
    /// <summary>
    /// This class is redundant and no longer in use.
    /// </summary>
    /// <remarks>
    /// This class was previously used for admin authentication, but it has been replaced 
    /// by the AccountAuthenticationaService. It is kept here for 
    /// historical reference but should not be used in new code.
    /// </remarks>
    public class AdminAuthenticationService : IAccountAuthenticationService
    {
        private readonly Admin defaultAccount;

        public AdminAuthenticationService()
        {
            this.defaultAccount = new Admin("admin", "admin");
        }

        public Account GetAuthenticatedAccount()
        {
            throw new NotImplementedException();
        }

        public void LoginAccount(Account account)
        {
            if(account.UserType != eUserType.Admin)
            {
                throw new UnauthorizedAccessException();
            }

            if(account.Password == null || account.Username == null) 
            {
                throw new Exception("username and/or password must not be null.");
            }

            if (account.Username != defaultAccount.Username || account.Password != defaultAccount.Password)
                throw new UnauthorizedAccessException();
        }

        public void LogoutAccount()
        {
            throw new NotImplementedException();
        }
    }
}

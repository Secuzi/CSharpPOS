using System;

namespace FinalOutput.Models { 
    public abstract class Account
    {
        protected Guid Id {  get; private set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public eUserType UserType { get; protected set; }

        public Account(string username, string password)
        {
            Username = username;
            Password = password;
            this.Id = Guid.NewGuid();
        }        
    }


    





}

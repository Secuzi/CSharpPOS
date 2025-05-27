using System.Collections.Generic;
using System.IO;
using System;
using FinalOutput.Models;

namespace FinalOutput
{
    public class Costumer : Account
    {
        //Create a static function such that we can subtract balance to price
        public decimal Balance { get; set; }
    
        public Costumer(string username, string password, decimal balance = 0m) : base(username, password)
        {
            Balance = balance;
            this.UserType = eUserType.Costumer;
        }        


    }

 

    





}

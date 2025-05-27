using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalOutput.Models
{
    public static class AccountCreation
    {
        public static Costumer CreateCostumerAccount()
        {
            Console.Write("Please enter your username: ");
            string username = Console.ReadLine().Trim();

            Console.Write("Please enter your password: ");
            string password = Console.ReadLine().Trim();

            if (AccountValidator.DoesCostumerAccountExist(username) == false)
            {
                return new Costumer(username, password);
            }

            return null;


        }

        public static Admin CreateAdminAccount()
        {
            Console.Write("Please enter your username: ");
            string username = Console.ReadLine().Trim();

            Console.Write("Please enter your password: ");
            string password = Console.ReadLine().Trim();

            return new Admin(username, password);
        }
        

    }
}

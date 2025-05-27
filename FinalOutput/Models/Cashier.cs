
namespace FinalOutput.Models
{
    public class Cashier : Account
    {
        public Cashier(string username, string password) : base(username, password)
        {
            this.UserType = eUserType.Cashier;
        }
    }
}

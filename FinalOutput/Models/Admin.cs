
namespace FinalOutput.Models
{
    public class Admin : Account
    {
        public static Admin staticAdmin = new Admin("admin", "admin");
        public Admin(string username, string password) : base(username, password)
        {
            this.UserType = eUserType.Admin;
        }
    }


    





}

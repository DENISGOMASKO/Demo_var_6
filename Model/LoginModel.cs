using System.Linq;
using System.Data;

namespace Demo_var_6
{
    internal class LoginModel
    {
        public User Login(string login, string password)
        {
            using (TradeCompletedContext context = new())
            {
                User result = (from user in context.Users
                              where user.Login == login && user.Password == password
                              select user).FirstOrDefault();
                return result;
            }
        }
    }
}

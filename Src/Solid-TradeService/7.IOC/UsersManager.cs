using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeApp
{
    public interface IUsersManager
    {
        string GetCurrentUserName();
    }

    public class UsersManager : IUsersManager
    {
        public string GetCurrentUserName()
        {
            return Environment.UserName;
        }
    }
}

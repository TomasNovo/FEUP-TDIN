using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class User
    {
        public UserInfo userInfo;
        public Client client;

        public string IP;
        public string port;

        public User()
        {

        }

        public User(UserInfo info)
        {
            this.userInfo = info;
        }
    }
}

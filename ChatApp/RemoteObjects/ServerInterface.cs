using System;
using System.Collections.Generic;
using System.Text;

namespace RemoteObjects
{
    public abstract class ServerInterface : MarshalByRefObject
    {


        public override object InitializeLifetimeService() { return (null); }

        public abstract bool AddUser(String username);
        public abstract void ShowUsers();

    }
}

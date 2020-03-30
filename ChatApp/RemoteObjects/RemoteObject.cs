using System;

namespace RemoteObjects
{
    public delegate void CallBack(String message);

    public class RemoteObject : MarshalByRefObject
    {
        public ServerInterface serverInterface = null;
        public int a = 0;

        public RemoteObject()
        {

        }

        public override object InitializeLifetimeService() { return (null); }



    }
}
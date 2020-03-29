using System;

namespace RemoteObjects
{
    public class RemoteObject : MarshalByRefObject
    {
        String msg = "";

        public RemoteObject()
        {

        }

        public void SetMessage(string message)
        {
            msg = message;
        }


        public String GetMessage()
        {
            return msg;
        }
    }
}
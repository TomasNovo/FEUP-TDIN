using System;

public class RemoteObject : MarshalByRefObject
{
    public RemoteObject()
    {
    }
    public void SetMessage(string message)
    {
        Cache.GetInstance().MessageString = message;
    }
}

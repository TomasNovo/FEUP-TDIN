using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    public class OnlineUsersEventArgs : EventArgs
    {
        public ArrayList ou { get; set; }

        public OnlineUsersEventArgs(ArrayList al)
        {
            ou = al;
        }

        //public delegate void OnlineUsersChangeEventHandler(object source, OnlineUsersEventArgs e);
        //public event OnlineUsersChangeEventHandler OnlineUsersChanged;
    };
}

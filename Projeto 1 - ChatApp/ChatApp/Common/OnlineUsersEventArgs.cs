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
        public List<string> ou { get; set; }

        public OnlineUsersEventArgs(List<string> al)
        {
            ou = al;
        }
    };
}

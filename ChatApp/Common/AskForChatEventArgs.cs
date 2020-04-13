using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    public class AskForChatEventArgs : EventArgs
    {
        public int roomId { get; set; }
        public string creator;
        public List<string> userList { get; set; }

    };
}

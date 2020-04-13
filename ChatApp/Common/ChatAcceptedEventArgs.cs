using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    public class ChatAcceptedEventArgs
    {
        public int roomId { get; set; }
        public List<string> userList { get; set; }

    }
}

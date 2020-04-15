using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    public class MessageReceivedEventArgs
    {
        public int roomId { get; set; }
        public string sender { get; set; }
        public string timestamp { get; set; } // ?
        public string message { get; set; }

    }
}

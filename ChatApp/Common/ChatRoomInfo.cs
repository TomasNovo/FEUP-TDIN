using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    public class ChatRoomInfo
    {
        public int roomId { get; set; }
        public string creator { get; set; }
        public List<Client> clients { get; set; }
        public List<string> accepted = new List<string>();
        //public List<>
    }
}

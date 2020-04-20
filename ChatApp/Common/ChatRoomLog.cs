using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ChatRoomLog
    {
        [BsonId]
        public int roomId { get; set; }
        public List<Tuple<string, string>> messageList = new List<Tuple<string, string>>();


        public void AddMessage(string sender, string message)
        {
            messageList.Add(CreateMessageTuple(sender, message));
        }

        public static Tuple<string, string> CreateMessageTuple(string sender, string message)
        {
            return new Tuple<string, string>(sender, message);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Common
{
    public class UserInfo
    {
        public ObjectId Id { get; set; }
        public string username { get; set; }
        public string realname { get; set; }
        public string password { get; set; }

        public UserInfo(String username, String password, String realname)
        {
            this.username = username;
            this.password = password;
            this.realname = realname;
        }

        public static BsonDocument toBsonDocument(String username, String password, String realname)
        {
            BsonDocument doc = new BsonDocument(false);
            
            doc.Add("username", username);
            doc.Add("password", password);
            doc.Add("realname", realname);

            return doc;
        }
    }

}

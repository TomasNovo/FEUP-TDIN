using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace TTService
{
    public class User
    {
        public ObjectId Id { get; set; }
        public string username { get; set; }
        public string email { get; set; }

        public User(string username, string email)
        {
            this.username = username;
            this.email = email;
        }
    }
}

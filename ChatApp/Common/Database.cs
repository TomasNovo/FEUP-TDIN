using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections;

namespace Common
{
    class Database
    {
        private MongoClient mongo;
        private IMongoDatabase database;
        private IMongoCollection<UserInfo> users;
        private IMongoCollection<ChatRoomLog> chatRoomLogs;

        public bool StartMongo()
        {
            try
            {
                mongo = new MongoClient("mongodb://localhost:27017");
                database = mongo.GetDatabase("server");
                users = database.GetCollection<UserInfo>("users");
                chatRoomLogs = database.GetCollection<ChatRoomLog>("chatRoomLogs");
            }
            catch (Exception e)
            {
                Console.WriteLine($"DB Error: {e.Message}");
                return false;
            }

            Console.WriteLine("Started database");
            return true;
        }

        public bool Register(string username, string password, string realname)
        {
            if (UsernameExists(username))
                return false;

            users.InsertOne(new UserInfo(username, password, realname));

            return true;
        }

        public UserInfo Login(string username, string password)
        {
            var userList = users.Find(x => (x.username == username && x.password == password)).ToList();

            if (userList.Count != 1)
                return null;

            return userList.First();
        }

        public ChatRoomLog CreateChatRoomLog(int roomId)
        {
            var chatRoomsList = chatRoomLogs.Find(x => x.roomId == roomId).ToList();
            if (chatRoomsList.Count == 1)
                return chatRoomsList[0];

            ChatRoomLog chatRoomLog = new ChatRoomLog(roomId);

            chatRoomLogs.InsertOne(chatRoomLog);

            return chatRoomLog;
        }

        public void AddMessageToLog(int roomId, string sender, string message)
        {
            ChatRoomLog log = chatRoomLogs.Find(x => x.roomId == roomId).Single();

            log.AddMessage(sender, message);

            chatRoomLogs.ReplaceOne(x => x.roomId == roomId, log);
        }

        public bool UsernameExists(string username)
        {
            return (users.Find(x => x.username == username).ToList().Count == 1);
        }

        public List<string> GetUsersArraylist()
        {
            return users.Find(_ => true).ToList().Select(e => e.username).ToList();
        }
    }
}

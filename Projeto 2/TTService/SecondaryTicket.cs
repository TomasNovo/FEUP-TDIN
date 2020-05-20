using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTService
{
    public class SecondaryTicket
    {
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; set; }
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId originalTicketId { get; set; }

        public string solver { get; set; }
        public string secondarySolver { get; set; }
        public DateTime date { get; set; }
        public bool received { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string response { get; set; }

        public SecondaryTicket(ObjectId Id, ObjectId originalTicketId, string solver, string secondarySolver, string title, string description)
        {
            this.Id = Id;
            this.originalTicketId = originalTicketId;
            this.solver = solver;
            this.secondarySolver = secondarySolver;
            this.date = DateTime.Now;
            this.received = false;
            this.title = title;
            this.description = description;
            this.response = null;
        }
    }
}

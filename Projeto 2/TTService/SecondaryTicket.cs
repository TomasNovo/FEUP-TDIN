using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTService
{
    class SecondaryTicket
    {
        public ObjectId Id { get; set; }
        public ObjectId originalTicketId { get; set; }
        public string solver { get; set; }
        public string secondarySolver { get; set; }
        public DateTime date { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string response { get; set; }

        public SecondaryTicket(ObjectId originalTicketId, string solver, string secondarySolver, string title, string description)
        {
            this.originalTicketId = originalTicketId;
            this.solver = solver;
            this.secondarySolver = secondarySolver;
            this.date = DateTime.Now;
            this.title = title;
            this.description = description;
            this.response = null;
        }
    }
}

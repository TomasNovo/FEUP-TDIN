using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTService
{
    public class Ticket
    {
        public ObjectId Id { get; set; }
        public string user { get; set; }
        public DateTime date { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public TicketStatus status { get; set; }
        public string solver { get; set; }

        public Ticket(string u, DateTime da, string t, string d)
        {
            this.user = u;
            this.date = da;
            this.title = t;
            this.description = d;
            this.status = TicketStatus.Unassigned;
            this.solver = null;
        }
    }

    public enum TicketStatus
    {
        Unassigned,
        Assigned,
        WaitingForAnswers,
        Solved
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTService
{
    public class Ticket
    {
        public string user;
        public DateTime date;
        public string title;
        public string description;

        public Ticket(string u, DateTime da, string t, string d)
        {
            this.user = u;
            this.date = da;
            this.title = t;
            this.description = d;
        }
    }
}

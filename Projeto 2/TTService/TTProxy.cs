using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace TTService
{
    public class TTProxy : ClientBase<ITTService>, ITTService
    {
        public DataTable GetUsers()
        {
            return Channel.GetUsers();
        }

        public DataTable GetTicketsByUser(string author)
        {
            return Channel.GetTicketsByUser(author);
        }

        public DataTable GetTickets()
        {
            return Channel.GetTickets();
        }

        public int AddTicket(string author, string email, string title, string description)
        {
            return Channel.AddTicket(author, email, title, description);
        }

        public int AssignSolver(string solver, string id)
        {
            return Channel.AssignSolver(solver, id);
        }

        public DataTable GetTicketsSolver(string s)
        {
            return Channel.GetTicketsSolver(s);
        }
    }
}

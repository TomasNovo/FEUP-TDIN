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

        // Secondary Tickets
        public int AddSecondaryTicket(string originalTicketId, string solver, string secondarySolver, string title, string description)
        {
            return Channel.AddSecondaryTicket(originalTicketId, solver, secondarySolver, title, description);
        }

        public DataTable GetSecondaryTickets()
        {
            return Channel.GetSecondaryTickets();
        }

        public DataTable GetSecondaryTicketsBySolver(string solver)
        {
            return Channel.GetSecondaryTicketsBySolver(solver);
        }

        public DataTable GetSecondaryTicketsBySecondarySolver(string secondarySolver)
        {
            return Channel.GetSecondaryTicketsBySecondarySolver(secondarySolver);
        }

        public int ChangeSecondaryTicketAnswer(string id, string response)
        {
            return Channel.ChangeSecondaryTicketAnswer(id, response);
        }


    }
}

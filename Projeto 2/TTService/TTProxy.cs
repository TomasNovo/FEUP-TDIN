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
        public int AddSecondaryTicket(string originalTicketId, string solver, string secondarySolver, string title, string question)
        {
            return Channel.AddSecondaryTicket(originalTicketId, solver, secondarySolver, title, question);
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

        public int DeleteSecondaryTicket(string originalTicketId)
        {
            return Channel.DeleteSecondaryTicket(originalTicketId);
        }


        public int AddSecondaryTicketNewQuestions(string originalTicketId, string solver, string secondarySolver, string title, List<string> questions, List<string> answers)
        {
            return Channel.AddSecondaryTicketNewQuestions(originalTicketId, solver, secondarySolver, title, questions, answers);
        }

            //public List<SecondaryTicket> GetSecondaryTicketInfoByID(string secondaryID)
            //{
            //    return Channel.GetSecondaryTicketInfoByID(secondaryID);
            //}

        }
}

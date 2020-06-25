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
        public List<string> questions { get; set; }
        public List<string> answers { get; set; }

        public SecondaryTicket()
        {

        }

        public SecondaryTicket(ObjectId Id, ObjectId originalTicketId, string solver, string secondarySolver, string title, string question)
        {
            this.Id = Id;
            this.originalTicketId = originalTicketId;
            this.solver = solver;
            this.secondarySolver = secondarySolver;
            this.date = DateTime.Now;
            this.received = false;
            this.title = title;

            this.questions = new List<string>();
            this.answers = new List<string>();

            questions.Add(question);
            answers.Add("waiting for answer");
        }

        public SecondaryTicket(ObjectId Id, ObjectId originalTicketId, string solver, string secondarySolver, string title, List<string> questions, List<string> answers)
        {
            this.Id = Id;
            this.originalTicketId = originalTicketId;
            this.solver = solver;
            this.secondarySolver = secondarySolver;
            this.date = DateTime.Now;
            this.received = false;
            this.title = title;

            this.questions = questions;
            this.answers = answers;
        }

        public void AddQuestion(string q)
        {
            this.questions.Add(q);
        }

        public void AddResponse(string r)
        {
            this.answers.Add(r);
        }
    }
}

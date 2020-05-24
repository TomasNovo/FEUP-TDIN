using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using TTClient;

namespace TTClient
{
    public class MailTicket
    {
        string origin = "TicketFactoryTDIN@sapo.pt";
        string subject = "[Ticket Factory] Solved Ticket with id ";

        string id;
        string title;
        string description;
        string to;
        string solution;
        public MailTicket(string id, string title, string description, string to, string solution)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.to = to;
            this.solution = solution;
        }

        public void setSolution(string s)
        {
            this.solution = s;
        }

        public string getID()
        {
            return this.id;
        }

        public void sendMail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.sapo.pt");

                mail.From = new MailAddress(origin);
                mail.To.Add(this.to);
                mail.Subject = subject + id;
                mail.Body = "Recently you submitted the following ticket: \n\n"
                             +
                             this.title + '\n'
                             +
                             this.description + "\n\n"
                             +
                             "We propose the following solution: \n" 
                             + 
                             solution;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("TicketFactoryTDIN@sapo.pt", "TDINamite420");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                CustomOkMessageBox box = new CustomOkMessageBox("Email sent to " + to + " !");
                box.Show();
            }
            catch (Exception ex)
            {
                CustomOkMessageBox box = new CustomOkMessageBox(ex.ToString());
                box.Show();
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace TTService
{
    [ServiceContract]
    public interface ITTService
    {
        [WebInvoke(Method = "POST", UriTemplate = "/tickets", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        int AddTicket(string author, string email, string title, string description);

        [WebInvoke(Method = "POST", UriTemplate = "/ticketStatus", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        int ChangeTicketStatus(string id, TicketStatus status);

        [WebGet(UriTemplate = "/tickets/{author}", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        DataTable GetTicketsByUser(string author);


        // Tickets assigned to solver
        [WebInvoke(Method = "POST", UriTemplate = "/tickets/{solver}", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        int AssignSolver(string solver, string id);

        [WebGet(UriTemplate = "/ticketsSolver/{s}", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        DataTable GetTicketsSolver(string s);

        // Get Tickets
        [WebGet(UriTemplate = "/tickets", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        DataTable GetTickets();
            
        // Get Users
        [WebGet(UriTemplate = "/users", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        DataTable GetUsers();


        // Add Secondary Tickets
        [WebInvoke(Method = "POST", UriTemplate = "/secondaryTickets", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        int AddSecondaryTicket(string originalTicketId, string solver, string secondarySolver, string title, string question);

        // Add new question to secondary ticket
        [WebInvoke(Method = "POST", UriTemplate = "/secondaryTicketNewQuestion", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        int AddSecondaryTicketNewQuestions(string id, string originalTicketId, string solver, string secondarySolver, string title, List<string> questions, List<string> answers);

        // Delete secondary ticket
        [WebInvoke(Method = "POST", UriTemplate = "/secondaryTicketDelete", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        int DeleteSecondaryTicket(string originalTicketId);

        // Add answer
        [WebInvoke(Method = "POST", UriTemplate = "/secondaryTicketsAnswer", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        int ChangeSecondaryTicketAnswer(string originalTicketId, string response);

        // Get Secondary Tickets
        [WebGet(UriTemplate = "/secondaryTickets", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        DataTable GetSecondaryTickets();

        // Get Secondary Tickets by Solver
        [WebGet(UriTemplate = "/secondaryTicketsBySolver/{solver}", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        DataTable GetSecondaryTicketsBySolver(string solver);

        // Get Secondary Tickets by secondarySolver
        [WebGet(UriTemplate = "/secondaryTicketsBySecondarySolver/{secondarySolver}", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        DataTable GetSecondaryTicketsBySecondarySolver(string secondarySolver);

        // Get Secondary Ticket by ID
        //[WebGet(UriTemplate = "/secondaryTicket/{secondaryID}", ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //List<SecondaryTicket> GetSecondaryTicketInfoByID(string secondaryID);

        // Ping to start server
        [WebGet(UriTemplate = "/ping", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        void Ping();


    }

}

    

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

    }

}

    

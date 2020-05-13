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
        int AddTicket(string author, string problem);

        [WebGet(UriTemplate = "/tickets/{author}", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        DataTable GetTickets(string author);

        [WebGet(UriTemplate = "/users", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        DataTable GetUsers();

        // Our methods
        [WebInvoke(Method = "POST", UriTemplate = "/users", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        int AddUserToDB(string username, string email);

        [WebGet(UriTemplate = "/getUsers", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<string> GetUsersMongo();

        [WebInvoke(Method = "POST", UriTemplate = "/addTickets", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        int AddTicketToDB(string username, System.DateTime date, string title, string description);


        


    }

}

    

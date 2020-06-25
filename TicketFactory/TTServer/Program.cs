using System;
using System.ServiceModel.Web;

namespace TTServer {
    class Program {

        //private Database db = new Database();

        static void Main() {

            //db.StartMongo();
            //db.Register("zecas", "zecas@gmail.com");

            // For a REST WCF service host notice the use of WebServiceHost class instead of ServiceHost

            TTService.TTService service = new TTService.TTService();

            WebServiceHost host = new WebServiceHost(service);
            host.Open();
            Console.WriteLine("TT service running");
            Console.WriteLine("Press ENTER to stop the service");
            Console.ReadLine();
            host.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTService
{
    public class SocketConstants
    {

        public static readonly int BUFFER_SIZE = 1024;
        public static readonly string EOF = "<EOF>";
        public static readonly string OK = "<OK>";
        public static readonly string SECONDARYTICKETS = "SECONDARYTICKETS";
        public static readonly string ANSWER = "ANSWER";
        public static readonly string ID = "ID";

        public static readonly int port = 5001;
    }
}

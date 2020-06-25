using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTService
{
    [Serializable]
    public class DepartmentAnsweredEventArgs
    {
        public ObjectId id { get; set; }
        public string message { get; set; }
    }
}

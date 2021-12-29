using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Models
{
    public class ServiceResponse<T>
    {
        public ServiceResponse()
        {
            Messages = new List<string>();
        }
        public T Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<string> Messages { get; set; }
    }
}


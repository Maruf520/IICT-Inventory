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

        public void SetMessage(List<string> messages = null, HttpStatusCode statusCode = HttpStatusCode.NotFound)
        {
            if (messages == null)
            {
                this.Messages = new List<string>() {"Not Found"};
            }
            this.Messages = messages;
            this.StatusCode = statusCode;
        }

        public void SetOkMessage()
        {
            this.Messages = new List<string>() { "Ok" }; ;
            this.StatusCode = HttpStatusCode.OK;
        }

        public void SetNotFoundMessage()
        {
            this.Messages = new List<string>() { "Not Found" }; ;
            this.StatusCode = HttpStatusCode.NotFound;
        }

        public T Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<string> Messages { get; set; }


    }
}


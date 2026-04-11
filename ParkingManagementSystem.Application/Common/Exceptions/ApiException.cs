using System.Net;
using System.Runtime.Serialization;

namespace CleanTemplate.Application.Common.Exceptions
{
    public class ApiException : Exception
    {
        [IgnoreDataMember]
        public HttpStatusCode Code { get; }

        [DataMember(Name = "message")]
        public override string Message { get; }


        public ApiException(HttpStatusCode code, string message)
        {
            Code = code;
            Message = message;

        }
    }
}

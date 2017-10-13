using System.Net;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Lob.Internal
{
    public class Response : IResponse
    {
        public Response() : this(new Dictionary<string, string>())
        {

        }

        public Response(IDictionary<string, string> headers)
        {
            Headers = new ReadOnlyDictionary<string, string>(headers);
        }

        public Response(HttpStatusCode statusCode, object body, IDictionary<string, string> headers, string contentType)
        {
            StatusCode = statusCode;
            Body = body;
            Headers = new ReadOnlyDictionary<string, string>(headers);
            ContentType = contentType;
        }

        public object Body { get; private set; }

        public IReadOnlyDictionary<string, string> Headers { get; private set; }

        public HttpStatusCode StatusCode { get; private set; }

        public string ContentType { get; private set; }
    }
}
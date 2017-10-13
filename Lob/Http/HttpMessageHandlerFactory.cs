using System.Net;
using System.Net.Http;

namespace Lob.Internal
{
    public static class HttpMessageHandlerFactory
    {
        public static HttpMessageHandler CreateDefault()
        {
            return CreateDefault(null);
        }

        public static HttpMessageHandler CreateDefault(IWebProxy proxy)
        {
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = false
            };

            return handler;
        }
    }
}
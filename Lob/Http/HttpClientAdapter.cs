using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lob.Internal
{
    public class HttpClientAdapter : IHttpClient
    {
        readonly HttpClient _http;

        public HttpClientAdapter(Func<HttpMessageHandler> getHandler)
        {
            _http = new HttpClient(new RedirectHandler { InnerHandler = getHandler() });
        }

        public async Task<IResponse> Send(IRequest request)
        {
            var requestMessage = BuildRequestMessage(request);
            var responseMessage = await SendAsync(requestMessage).ConfigureAwait(false);
            return await BuildResponse(responseMessage).ConfigureAwait(false);
        }

        protected virtual async Task<IResponse> BuildResponse(HttpResponseMessage responseMessage)
        {
            object responseBody = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            string contentType = GetContentMediaType(responseMessage.Content);
            return new Response(
                responseMessage.StatusCode,
                responseBody,
                responseMessage.Headers.ToDictionary(h => h.Key, h => h.Value.First()),
                contentType);
        }

        protected virtual HttpRequestMessage BuildRequestMessage(IRequest request)
        {
            HttpRequestMessage requestMessage = null;
            try
            {
                var fullUri = new Uri(request.BaseAddress, request.Endpoint);
                requestMessage = new HttpRequestMessage(request.Method, fullUri);

                foreach (var header in request.Headers)
                {
                    requestMessage.Headers.Add(header.Key, header.Value);
                }

                var httpContent = request.Body as HttpContent;
                if (httpContent != null)
                {
                    requestMessage.Content = httpContent;
                }

                var body = request.Body as string;
                if (body != null)
                {
                    requestMessage.Content = new StringContent(body, Encoding.UTF8, request.ContentType);
                }

                var bodyStream = request.Body as Stream;
                if (bodyStream != null)
                {
                    requestMessage.Content = new StreamContent(bodyStream);
                    requestMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(request.ContentType);
                }
            }
            catch (Exception)
            {
                if (requestMessage != null)
                {
                    requestMessage.Dispose();
                }
                throw;
            }

            return requestMessage;
        }

        static string GetContentMediaType(HttpContent httpContent)
        {
            if (httpContent.Headers != null && httpContent.Headers.ContentType != null)
            {
                return httpContent.Headers.ContentType.MediaType;
            }
            return null;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_http != null) _http.Dispose();
            }
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            var clonedRequest = await CloneHttpRequestMessageAsync(request).ConfigureAwait(false);

            return await _http.SendAsync(request, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
        }

        public static async Task<HttpRequestMessage> CloneHttpRequestMessageAsync(HttpRequestMessage oldRequest)
        {
            var newRequest = new HttpRequestMessage(oldRequest.Method, oldRequest.RequestUri);

            var ms = new MemoryStream();
            if (oldRequest.Content != null)
            {
                await oldRequest.Content.CopyToAsync(ms).ConfigureAwait(false);
                ms.Position = 0;
                newRequest.Content = new StreamContent(ms);

                if (oldRequest.Content.Headers != null)
                {
                    foreach (var h in oldRequest.Headers)
                    {
                        newRequest.Content.Headers.Add(h.Key, h.Value);
                    }
                }
            }

            newRequest.Version = oldRequest.Version;

            foreach (var header in oldRequest.Headers)
            {
                newRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            foreach (var property in oldRequest.Properties)
            {
                newRequest.Properties.Add(property);
            }

            return newRequest;
        }

        internal class RedirectHandler : DelegatingHandler
        {
        }
    }
}
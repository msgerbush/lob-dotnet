using System;
using System.Net;
using Lob.Models.Response;

namespace Lob.Http
{
    public class LobException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; }
        public LobError LobError { get; set; }
        public IApiResponse<LobError> ApiResponse { get; set; }

        public LobException(IApiResponse<LobError> response) : base(response.Body.Message)
        {
            ApiResponse = response;
            LobError = response.Body;
            HttpStatusCode = response.HttpResponse.StatusCode;
        }
    }
}

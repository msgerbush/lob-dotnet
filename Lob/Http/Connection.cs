using System;
using Lob.Internal;
using System.Threading.Tasks;
using System.Net.Http;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Lob
{
    public class Connection : IConnection
    {
        static readonly Uri _defaultLobApiUrl = LobClient.LobApiUrl;
        static readonly TimeSpan _defaultTimeout = LobClient.Timeout;
        static readonly string _defaultContentType = LobClient.ContentType;

        readonly Authenticator _authenticator;
        readonly IHttpClient _httpClient;

        public Connection(ProductHeaderValue productInformation, Uri baseAddress, ICredentialStore credentialStore)
            : this(productInformation, baseAddress, credentialStore, new HttpClientAdapter(HttpMessageHandlerFactory.CreateDefault))
        {

        }

        public Connection(
            ProductHeaderValue productInformation,
            Uri baseAddress,
            ICredentialStore credentialStore,
            IHttpClient httpClient)
        {
            UserAgent = FormatUserAgent(productInformation);
            BaseAddress = baseAddress;
            CredentialStore = credentialStore;
            _httpClient = httpClient;
            _authenticator = new Authenticator(credentialStore);

        }

        public Task<IApiResponse<T>> Post<T>(Uri uri, object body, string accepts)
        {
            return SendData<T>(uri, HttpMethod.Post, body, accepts, _defaultContentType, _defaultTimeout);
        }

        Task<IApiResponse<T>> SendData<T>(
            Uri uri,
            HttpMethod method,
            object body,
            string accepts,
            string contentType,
            TimeSpan timeout
            )
        {
            var request = new Request
            {
                Method = method,
                Endpoint = uri,
                Timeout = timeout
            };

            return SendDataInternal<T>(body, accepts, contentType, request);
        }

        Task<IApiResponse<T>> SendDataInternal<T>(object body, string accepts, string contentType, Request request)
        {
            request.Body = body;
            return Run<T>(request);
        }

        async Task<IApiResponse<T>> Run<T>(IRequest request)
        {
            var response = await RunRequest(request).ConfigureAwait(false);
            return DeserializeResponse<T>(response);
        }

        async Task<IResponse> RunRequest(IRequest request)
        {
            request.Headers.Add("User-Agent", UserAgent);
            SerializeRequest(request);
            await _authenticator.Apply(request).ConfigureAwait(false);
            var response = await _httpClient.Send(request).ConfigureAwait(false);
            return response;
        }

        public IApiResponse<T> DeserializeResponse<T>(IResponse response)
        {
            var body = response.Body as string;
            DefaultContractResolver contractRersolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            var json = JsonConvert.DeserializeObject<T>(body, new JsonSerializerSettings
            {
                ContractResolver = contractRersolver
            });
            return new ApiResponse<T>(response, json);
        }

        public void SerializeRequest(IRequest request)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            request.Body = JsonConvert.SerializeObject(request.Body, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
        }

        public Uri BaseAddress { get; private set; }

        public string UserAgent { get; private set; }

        public ICredentialStore CredentialStore { get; private set; }

                public Credentials Credentials
        {
            get { return CredentialStore.GetCredentials().Result; }
            set { _authenticator.CredentialStore = new CredentialStore(value);  }
        }

        static string FormatUserAgent(ProductHeaderValue productInformation)
        {
            return string.Format(CultureInfo.InvariantCulture, "Lob/v1 C# .NET/", productInformation);
        }
    }
}
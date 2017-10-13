using System;
using System.Threading.Tasks;

namespace Lob
{
    public interface IConnection
    {
        Task<IApiResponse<T>> Post<T>(Uri uri, object body, string accepts);

        Uri BaseAddress { get; }

        ICredentialStore CredentialStore { get; }

        Credentials Credentials { get; set; }
    }
}
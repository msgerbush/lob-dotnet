using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lob.Internal
{
    public interface IHttpClient : IDisposable
    {
        Task<IResponse> Send(IRequest request);
    }
}
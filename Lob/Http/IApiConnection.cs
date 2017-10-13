using System;
using System.Threading.Tasks;

namespace Lob
{
    public interface IApiConnection
    {
        IConnection Connection { get; }

        Task<T> Post<T>(Uri uri, object data, string contentType);
    }
}
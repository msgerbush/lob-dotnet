using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Lob.Internal;

namespace Lob
{
    public class ApiConnection : IApiConnection
    {
        public ApiConnection(IConnection connection)
        {
            Connection = connection;
        }

        public IConnection Connection { get; set; }

        public async Task<T> Post<T>(Uri uri, object data, string contentType)
        {
            var response = await Connection.Post<T>(uri, data, contentType).ConfigureAwait(false);
            return response.Body;
        }
    }
}
namespace Lob
{
    public abstract class ApiClient
    {
        protected ApiClient(IApiConnection apiConnection)
        {
            ApiConnection = apiConnection;
            Connection = apiConnection.Connection;
        }

        protected IApiConnection ApiConnection { get; private set; }

        protected IConnection Connection { get; private set; }
    }
}
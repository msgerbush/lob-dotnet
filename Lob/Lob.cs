using System;

namespace Lob
{
    public class LobClient : ILobClient
    {
        public static readonly Uri LobApiUrl = new Uri("https://api.lob.com/v1/");
        public static readonly TimeSpan Timeout = TimeSpan.FromSeconds(20);
        public static readonly string ContentType = "application/json";

        public LobClient(string appIdentifier, ICredentialStore credentialStore)
            : this(new Connection(new ProductHeaderValue(appIdentifier), LobApiUrl, credentialStore))
        {
        }

        public LobClient(IConnection connection)
        {
            Connection = connection;
            var apiConnection = new ApiConnection(connection);
            Letter = new LettersClient(apiConnection);
        }

        public Credentials Credentials
        {
            get { return Connection.Credentials; }
        }

        public Uri BaseAddress
        {
            get { return LobApiUrl; }
        }

        public IConnection Connection { get; private set; }

        public ILettersClient Letter { get; private set; }

    }
}
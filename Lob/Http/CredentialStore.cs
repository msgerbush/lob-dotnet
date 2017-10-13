using System.Threading.Tasks;

namespace Lob.Internal
{
    public class CredentialStore : ICredentialStore
    {
        readonly Credentials _credentials;

        public CredentialStore(Credentials credentials)
        {
            _credentials = credentials;
        }

        public Task<Credentials> GetCredentials()
        {
            return Task.FromResult(_credentials);
        }
    }
}
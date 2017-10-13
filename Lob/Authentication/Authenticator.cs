using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lob.Internal
{
    class Authenticator
    {
        public Authenticator(ICredentialStore credentialStore)
        {
            CredentialStore = credentialStore;
        }

        public async Task Apply(IRequest request)
        {
            Credentials credentials = await CredentialStore.GetCredentials().ConfigureAwait(false);
            BasicAuthenticator basicAuthenticator = new BasicAuthenticator();
            basicAuthenticator.Authenticate(request, credentials);
        }

        public ICredentialStore CredentialStore { get; set; }
    }
}
using System.Threading.Tasks;

namespace Lob
{
    public interface ICredentialStore
    {
        Task<Credentials> GetCredentials();
    }
}
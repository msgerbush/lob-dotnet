using System.Threading.Tasks;

namespace Lob
{
    public interface ICredentialStore
    {
        // [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Not Required")]
        Task<Credentials> GetCredentials();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lob
{
    public class LettersClient : ApiClient, ILettersClient
    {
        public LettersClient(IApiConnection apiConnection) : base(apiConnection)
        {

        }

        public Task<Letter> Create(NewLetter newLetter)
        {
            return ApiConnection.Post<Letter>(ApiUrls.Letters(), newLetter, "application/json");
        }
    }
}
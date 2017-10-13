
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lob
{
    public interface ILettersClient
    {
        Task<Letter> Create(NewLetter newLetter);
    }
}
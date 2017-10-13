using System;

namespace Lob
{
    public interface ILobClient
    {
        IConnection Connection { get; }

        ILettersClient Letter { get; }
    }
}
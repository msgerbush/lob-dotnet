using System;

namespace Lob
{
    public class Credentials
    {
        public Credentials(string apiKey)
        {
            ApiKey = apiKey;
        }

        public string ApiKey
        {
            get;
            private set;
        }
    }
}
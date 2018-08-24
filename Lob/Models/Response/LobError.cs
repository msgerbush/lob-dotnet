using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Lob.Models.Response
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class LobError
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
    }
}

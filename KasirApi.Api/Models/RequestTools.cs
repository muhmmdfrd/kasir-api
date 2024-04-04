using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace KasirApi.Api.Models
{
    internal class RequestTools
    {
        public string Token { get; set; } = "";
    }

    internal static class RequestToolsExtension
    {
        public static RequestTools ToObject(this string value)
        {
            DefaultContractResolver contractResolver = new()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var result = JsonConvert.DeserializeObject<RequestTools>(value, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.None
            });

            if (result == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return result;
        }

    }
}

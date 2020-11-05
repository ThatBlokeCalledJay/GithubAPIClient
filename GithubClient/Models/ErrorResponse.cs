using Newtonsoft.Json;

namespace GithubClient.Models
{
    public class ErrorResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("documentation_url")]
        public string DocumentationUrl { get; set; }

        public static ErrorResponse Parse(string json) => JsonConvert.DeserializeObject<ErrorResponse>(json);
    }
}
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GithubClient.GithubGraphQL.Models
{
    public class GraphQLRequest
    {
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("variables", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, object> Variables { get; set; }

        [JsonProperty("operationName", NullValueHandling = NullValueHandling.Ignore)]
        public string OperationName { get; set; }
    }
}
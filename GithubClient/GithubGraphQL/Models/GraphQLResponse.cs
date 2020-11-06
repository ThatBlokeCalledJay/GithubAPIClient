using System.Collections.Generic;
using System.Linq;
using GithubClient.Models;
using Newtonsoft.Json;

namespace GithubClient.GithubGraphQL.Models
{
    public class GraphQLResponse : IGithubResponse
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public Data Data { get; set; }

        [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
        public List<Error> Errors { get; set; }

        public static GraphQLResponse Parse(string json)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<GraphQLResponse>(json);

                if (data.Errors != null && data.Errors.Any())
                    data.Message = "Errors have been identified.";

                return data;
            }
            catch
            {
                return new GraphQLResponse
                {
                    Message = $"Parse Error: {json}"
                };
            }
        }

        /// <inheritdoc />
        public string Message { get; set; }

        /// <inheritdoc />
        public string DocumentationUrl { get; set; }
    }
}
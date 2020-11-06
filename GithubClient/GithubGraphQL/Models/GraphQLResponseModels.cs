using System.Collections.Generic;
using Newtonsoft.Json;

namespace GithubClient.GithubGraphQL.Models
{
    public class Error
    {
        [JsonProperty("extensions")]
        public Extensions Extensions { get; set; }

        [JsonProperty("locations")]
        public Location[] Locations { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class Extensions
    {
        [JsonProperty("value")]
        public object Value { get; set; }

        [JsonProperty("problems")]
        public Problem[] Problems { get; set; }
    }

    public class Problem
    {
        [JsonProperty("path")]
        public object[] Path { get; set; }

        [JsonProperty("explanation")]
        public string Explanation { get; set; }
    }

    public class Location
    {
        [JsonProperty("line")]
        public long Line { get; set; }

        [JsonProperty("column")]
        public long Column { get; set; }
    }
    
    public class Data
    {
        [JsonProperty("search")]
        public Search Search { get; set; }
    }

    public class Search
    {
        [JsonProperty("repositoryCount")]
        public long RepositoryCount { get; set; }

        [JsonProperty("pageInfo")]
        public PageInfo PageInfo { get; set; }

        [JsonProperty("edges")]
        public List<RepositoryEdge> RepositoryEdges { get; set; }
    }

    public class RepositoryEdge
    {
        [JsonProperty("node")]
        public RepositoryNode Node { get; set; }
    }

    public class RepositoryNode
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("languages")]
        public Languages Languages { get; set; }
    }

    public class Languages
    {
        [JsonProperty("edges")]
        public List<LanguagesEdge> Edges { get; set; }
    }

    public class LanguagesEdge
    {
        [JsonProperty("node")]
        public LanguageNode Node { get; set; }
    }

    public class LanguageNode
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class PageInfo
    {
        [JsonProperty("endCursor")]
        public string EndCursor { get; set; }

        [JsonProperty("startCursor")]
        public string StartCursor { get; set; }
    }
}
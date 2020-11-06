namespace GithubClient.Models
{
    public interface IGithubResponse
    {
        /// <summary>Request status message.</summary>
        string Message { get; set; }

        /// <summary>A documentation url may be provided if the request was unsuccessful.</summary>
        string DocumentationUrl { get; set; }
    }
}
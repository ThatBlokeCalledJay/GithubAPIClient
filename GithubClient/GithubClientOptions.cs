using System;
using System.Net.Http;
using GithubClient.TokenProvider;

namespace GithubClient
{
    public class GithubClientOptions
    {
        /// <summary>Github token provider.</summary>
        public ITokenProvider TokenProvider { get; }

        /// <summary>
        /// [optional] Configure and provide your own <see cref="System.Net.Http.HttpClient"/> to be used by the <see cref="GithubClient"/>.
        /// </summary>
        public HttpClient HttpClient { get; }

        /// <summary>
        /// Initialise a new instance of the <see cref="GithubClientOptions"/> class with the provided <see cref="ITokenProvider"/>.
        /// </summary>
        /// <param name="tokenProvider"></param>
        public GithubClientOptions(ITokenProvider tokenProvider)
        {
            TokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
        }

        /// <summary>
        /// Initialise a new instance of the <see cref="GithubClientOptions"/> class with a default <see cref="ITokenProvider"/> using the given <paramref name="apiKey"/>.
        /// </summary>
        /// <param name="apiKey"></param>
        public GithubClientOptions(string apiKey)
        {
            TokenProvider = new DefaultTokenProvider(apiKey);
        }

        /// <summary>
        /// Initialise a new instance of the <see cref="GithubClientOptions"/> class with a custom <see cref="System.Net.Http.HttpClient"/>.
        /// </summary>
        /// <param name="httpClient"></param>
        public GithubClientOptions(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
    }
}
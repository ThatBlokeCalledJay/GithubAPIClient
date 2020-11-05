using System;
using System.Net.Http;
using System.Threading.Tasks;
using GithubClient.Models;

namespace GithubClient
{
    public class GithubClient
    {
        private const string GithubApiUrl = "https://api.github.com";

        private readonly HttpClient _client;

        public GithubClient(GithubClientOptions options)
        {
            
            _client = options.HttpClient ?? new HttpClient(new GithubClientMessageHandler(options.TokenProvider))
            {
                BaseAddress = new Uri(GithubApiUrl)
            };
        }

        public async Task<GithubApiResponse<UserRepositories>> GetUserRepos(string userName)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, new Uri($"users/{userName}/repos", UriKind.Relative));

            var response = await _client.SendAsync(message).ConfigureAwait(false);

            var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return response.IsSuccessStatusCode
                ? new GithubApiResponse<UserRepositories>(response.StatusCode, UserRepositories.Parse(body))
                : new GithubApiResponse<UserRepositories>(response.StatusCode, ErrorResponse.Parse(body));
        }
    }
}
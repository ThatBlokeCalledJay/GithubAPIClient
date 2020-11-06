using System;
using System.Net.Http;
using System.Threading.Tasks;
using GithubClient.GithubGraphQL.Models;
using GithubClient.GithubGraphQL.Queries;
using GithubClient.Models;

namespace GithubClient.GithubGraphQL
{
    public class GithubGraphQLClient
    {
        private const string GithubApiUrl = "https://api.github.com/graphql";

        private readonly HttpClient _client;

        public GithubGraphQLClient(GithubClientOptions options)
        {
            _client = options.HttpClient ?? new HttpClient(new GithubGraphQLClientMessageHandler(options.TokenProvider))
            {
                BaseAddress = new Uri(GithubApiUrl)
            };
        }

        public async Task<GithubApiResponse<GraphQLResponse>> GetUserRepos(string userName)
        {
            return await ExecuteQuery(new GithubQueryGetUserRepos(userName, 25, 8));
        }

        public async Task<GithubApiResponse<GraphQLResponse>> ExecuteQuery(GithubGraphQLQuery query)
        {
            var message = query.BuildRequestMessage();

            var response = await _client.SendAsync(message).ConfigureAwait(false);

            var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return new GithubApiResponse<GraphQLResponse>(response.StatusCode, GraphQLResponse.Parse(body), response.StatusCode.ToString());
        }
    }
}
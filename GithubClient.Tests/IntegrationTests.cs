using GithubClient.GithubGraphQL;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace GithubClient.Tests
{
    [TestClass]
    public class IntegrationTests
    {
        private IConfiguration _configuration;

        [TestInitialize]
        public void Init()
        {
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder
                .AddEnvironmentVariables()
                .AddUserSecrets("4986e3bc-c552-4e89-a375-49cc6cf3baef");

            _configuration = configurationBuilder.Build();
        }

        [TestMethod]
        public async Task Test_GraphQL_GetRepositories()
        {
            var authToken = _configuration["githubAuthToken"];

            var client = new GithubGraphQLClient(new GithubClientOptions(authToken));

            var result = await client.GetUserRepos("ThatBlokeCalledJay");

            Assert.IsTrue(result.Success, "Request was successful.");
            Assert.IsTrue(result.Content.Data.Search.RepositoryCount > 0, "Found repositories.");
        }

        [TestMethod]
        public async Task Test_API_GetRepositories()
        {
            var authToken = _configuration["githubAuthToken"];

            var client = new GithubClient(new GithubClientOptions(authToken));

            var result = await client.GetUserRepos("ThatBlokeCalledJay");

            Assert.IsTrue(result.Success, "Request was successful.");
            Assert.IsTrue(result.Content.Repositories.Count > 0, "Found repositories.");
        }
    }
}
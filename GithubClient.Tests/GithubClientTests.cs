using GithubClient.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GithubClient.Tests
{
    [TestClass]
    public class GithubClientTests
    {
        [TestMethod]
        public async Task Test_ParseResponseData()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Get, "https://test.com/users/TestUser/repos")
                .Respond("application/json", JsonConvert.SerializeObject(CreateRepoList()));

            var httpClient = new HttpClient(mockHttp)
            {
                BaseAddress = new Uri("https://test.com")
            };

            var clientOptions = new GithubClientOptions(httpClient);

            var client = new GithubClient(clientOptions);

            var result = await client.GetUserRepos("TestUser");

            Assert.IsTrue(result.Success, "Is success response.");
            Assert.AreEqual(HttpStatusCode.OK, result.ResponseCode);

            Assert.AreEqual(1, result.Content.Repositories.Count, "A single test repo was returned.");
        }

        [TestMethod]
        public async Task Test_ParseErrorData()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When(HttpMethod.Get, "https://test.com/users/TestUser/repos")
                .Respond(HttpStatusCode.BadRequest, (m) => new StringContent(JsonConvert.SerializeObject(CreateError()), Encoding.UTF8, "application/json"));

            var httpClient = new HttpClient(mockHttp)
            {
                BaseAddress = new Uri("https://test.com")
            };

            var clientOptions = new GithubClientOptions(httpClient);

            var client = new GithubClient(clientOptions);

            var result = await client.GetUserRepos("TestUser");

            Assert.IsFalse(result.Success, "Is failed response.");
            Assert.AreEqual(HttpStatusCode.BadRequest, result.ResponseCode);
            Assert.AreEqual("https://docs.com", result.DocumentationUrl);
            Assert.AreEqual("Naughty Robot", result.Message);
        }

        private List<Repository> CreateRepoList()
        {
            return new List<Repository>
            {
                new Repository
                {
                    Name = "UnitTestRepo",
                    Language = "C#",
                    Owner = new Owner{Login = "TestUser"}
                }
            };
        }

        private ErrorResponse CreateError()
        {
            return new ErrorResponse
            {
                DocumentationUrl = "https://docs.com",
                Message = "Naughty Robot"
            };
        }
    }
}
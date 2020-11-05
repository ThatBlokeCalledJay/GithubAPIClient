using GithubClient.TokenProvider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace GithubClient.Tests
{
    [TestClass]
    public class GithubClientMessageHandlerTests
    {
        [TestMethod]
        public async Task Test_AppliesMissingHeaders()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("https://test.com")
                .Respond("application/json", "");

            var handle = new GithubClientMessageHandler(new DefaultTokenProvider("my-token"), mockHttp);
            var client = new HttpClient(handle);

            var result = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://test.com"));

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            var authHeader = result.RequestMessage.Headers.GetValues("Authorization").First();
            var acceptHeader = result.RequestMessage.Headers.GetValues("Accept").First();
            var agentHeader = result.RequestMessage.Headers.GetValues("User-Agent").First();

            Assert.AreEqual("token my-token", authHeader, "Auth header was added.");
            Assert.AreEqual("application/vnd.github.v3+json", acceptHeader, "Accept header was added.");
            Assert.AreEqual("ThatBlokeCalledJay.GithubClient", agentHeader, "User-Agent header was added.");
        }

        [TestMethod]
        public async Task Test_DoesntOverrideCustomHeaders()
        {
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("https://test.com")
                .Respond("application/json", "");

            var handle = new GithubClientMessageHandler(new DefaultTokenProvider("my-token"), mockHttp);
            var client = new HttpClient(handle);

            client.DefaultRequestHeaders.Add("Authorization", "token custom.token");
            client.DefaultRequestHeaders.Add("Accept", "application/custom");
            client.DefaultRequestHeaders.Add("User-Agent", "custom.agent");

            var result = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "https://test.com"));

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            var authHeader = result.RequestMessage.Headers.GetValues("Authorization").First();
            var acceptHeader = result.RequestMessage.Headers.GetValues("Accept").First();
            var agentHeader = result.RequestMessage.Headers.GetValues("User-Agent").First();

            Assert.AreEqual("token custom.token", authHeader, "Custom auth header was used.");
            Assert.AreEqual("application/custom", acceptHeader, "Custom accept header was used.");
            Assert.AreEqual("custom.agent", agentHeader, "Custom agent header was used");
        }
    }
}
using GithubClient.TokenProvider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace GithubClient.Tests
{
    [TestClass]
    public class GithubClientOptionsTests
    {
        [TestMethod]
        public void Test_RequiresTokenProvider()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new GithubClientOptions((DefaultTokenProvider)null));
        }

        [TestMethod]
        public async Task Test_InitialiseWithToken_UseDefaultProvider()
        {
            var options = new GithubClientOptions("my-token");

            Assert.IsInstanceOfType(options.TokenProvider, typeof(DefaultTokenProvider), "Is expected DefaultTokenProvider.");

            var token = await options.TokenProvider.GetTokenAsync();

            Assert.AreEqual("my-token", token, "Retrieves provided token.");
        }

        [TestMethod]
        public async Task Test_InitialiseWithCustomProvider()
        {
            var provider = new DefaultTokenProvider("my-token");

            var options = new GithubClientOptions(provider);

            Assert.AreEqual(provider, options.TokenProvider, "Token provider instance used.");

            var token = await options.TokenProvider.GetTokenAsync();

            Assert.AreEqual("my-token", token, "Retrieves provided token.");
        }
    }
}
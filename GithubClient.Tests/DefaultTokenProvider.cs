using GithubClient.TokenProvider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace GithubClient.Tests
{
    [TestClass]
    public class DefaultTokenProviderTests
    {
        [TestMethod]
        public async Task Test_ReturnsToken()
        {
            var provider = new DefaultTokenProvider("my-token");

            var tokenAsync = await provider.GetTokenAsync();

            Assert.AreEqual("my-token", tokenAsync);
        }
    }
}
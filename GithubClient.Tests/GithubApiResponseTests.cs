using GithubClient.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace GithubClient.Tests
{
    [TestClass]
    public class GithubApiResponseTests
    {
        [TestMethod]
        public void Test_SuccessResult()
        {
            var response = new GithubApiResponse<string>(HttpStatusCode.OK, "Response Content", "Good Robot");

            Assert.IsTrue(response.Success);
            Assert.AreEqual(HttpStatusCode.OK, response.ResponseCode);
            Assert.AreEqual("Response Content", response.Content);
            Assert.AreEqual("Good Robot", response.Message);
        }

        [TestMethod]
        public void Test_NonSuccessResult()
        {
            var errorResponse = new ErrorResponse { DocumentationUrl = "http://docs.com", Message = "Naughty Robot" };

            var response = new GithubApiResponse<string>(HttpStatusCode.BadRequest, errorResponse);

            Assert.IsFalse(response.Success);
            Assert.AreEqual(HttpStatusCode.BadRequest,response.ResponseCode);
            Assert.AreEqual("Naughty Robot", response.Message);
            Assert.AreEqual("http://docs.com", response.DocumentationUrl);
        }
    }
}
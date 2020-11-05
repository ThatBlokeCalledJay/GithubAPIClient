using System.Threading.Tasks;

namespace GithubClient.TokenProvider
{
    public class DefaultTokenProvider : ITokenProvider
    {
        private readonly string _token;

        public DefaultTokenProvider(string token)
        {
            _token = token;
        }

        public Task<string> GetTokenAsync()
        {
            return Task.FromResult(_token);
        }
    }
}
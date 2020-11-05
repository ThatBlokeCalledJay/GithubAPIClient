using System.Threading.Tasks;

namespace GithubClient.TokenProvider
{
    public interface ITokenProvider
    {
        Task<string> GetTokenAsync();
    }
}
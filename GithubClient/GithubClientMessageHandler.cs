using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using GithubClient.TokenProvider;

namespace GithubClient
{
    /// <summary>
    /// Check out-going messages for required Github api headers and applies them if they are missing.
    /// </summary>
    public class GithubClientMessageHandler : DelegatingHandler
    {
        private readonly ITokenProvider _tokenProvider;

        
        /// <summary>
        /// Initialise a new instance of the <see cref="GithubClientMessageHandler"/> class with the provided <see cref="ITokenProvider"/>.
        /// </summary>
        /// <param name="tokenProvider"></param>
        /// <param name="innerHandler"></param>
        public GithubClientMessageHandler(ITokenProvider tokenProvider, HttpMessageHandler innerHandler = null)
        {
            InnerHandler = innerHandler ?? new HttpClientHandler();

            _tokenProvider = tokenProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization == null)
            {
                var token = await _tokenProvider.GetTokenAsync().ConfigureAwait(false);
                request.Headers.Authorization = new AuthenticationHeaderValue("token", token);
            }

            if (!request.Headers.Contains("Accept"))
                request.Headers.Add("Accept", "application/vnd.github.v3+json");

            if (!request.Headers.Contains("User-Agent"))
                request.Headers.Add("User-Agent", "ThatBlokeCalledJay.GithubClient");

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
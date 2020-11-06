using System.Net;

namespace GithubClient.Models
{
    /// <summary></summary>
    public class GithubApiResponse<T>: IGithubResponse
    {
        /// <summary></summary>
        public T Content { get; set; }

        /// <summary>Request status message.</summary>
        public string Message { get; set; }

        /// <summary>A documentation url may be provided if the request was unsuccessful.</summary>
        public string DocumentationUrl { get; set; }

        /// <summary>Indicates whether the request returned a success code (200 - 299) or not.</summary>
        public bool Success =>
            (int)ResponseCode >= 200 && (int)ResponseCode <= 299;

        /// <summary>Github api request response code.</summary>
        public HttpStatusCode ResponseCode { get; }

        /// <summary>
        /// Initialise a new instance of the <see cref="GithubApiResponse{T}"/> class that represents a successful response.
        /// </summary>
        /// <param name="responseCode"></param>
        /// <param name="content"></param>
        /// <param name="message"></param>
        public GithubApiResponse(HttpStatusCode responseCode, T content, string message = "Ok")
        {
            ResponseCode = responseCode;
            Content = content;
            Message = message;
        }

        /// <summary>
        /// Initialise a new instance of the <see cref="GithubApiResponse{T}"/> class that represents an unsuccessful response.
        /// </summary>
        /// <param name="responseCode"></param>
        /// <param name="error"></param>
        public GithubApiResponse(HttpStatusCode responseCode, ErrorResponse error)
        {
            ResponseCode = responseCode;
            Message = error.Message;
            DocumentationUrl = error.DocumentationUrl;
        }
    }
}
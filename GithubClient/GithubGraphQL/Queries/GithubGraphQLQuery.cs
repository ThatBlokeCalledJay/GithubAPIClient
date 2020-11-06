using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using GithubClient.GithubGraphQL.Models;
using Newtonsoft.Json;

namespace GithubClient.GithubGraphQL.Queries
{
    public abstract class GithubGraphQLQuery
    {
        private string _query;
        private IDictionary<string, object> _variables;

        internal void SetQuery(string query)
        {
            _query = query;
        }

        internal void SetVariables(IDictionary<string, object> variables)
        {
            _variables = variables;
        }

        protected GithubGraphQLQuery(){ }

        private GraphQLRequest BuildRequestModel()
        {
            var request = new GraphQLRequest();

            if (string.IsNullOrWhiteSpace(_query))
                throw new ArgumentException("Query cannot be null or empty.");

            request.Query = _query;

            if (_variables != null && _variables.Any())
                request.Variables = _variables;

            return request;
        }

        public HttpRequestMessage BuildRequestMessage()
        {
            var requestModel = BuildRequestModel();

            var message = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json")
            };

            return message;
        }
    }
}
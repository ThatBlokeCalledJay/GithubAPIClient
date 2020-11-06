using System.Collections.Generic;

namespace GithubClient.GithubGraphQL.Queries
{
    public class GithubQueryGetUserRepos : GithubGraphQLQuery
    {
        private const string Query =
            "query ($max_repos: Int!, $max_lang: Int!) {{ search(query: \"is:public user:{0}\", type: REPOSITORY, first: $max_repos) {{ repositoryCount edges {{ node {{ ... on Repository {{ name description url languages(last: $max_lang) {{ edges {{ node {{ name }} }} }} }} }} }} }} }}";

        public GithubQueryGetUserRepos(string userName, int maxRepos, int maxLanguages)
        {
            var variables = new Dictionary<string, object>
            {
                {"max_repos", maxRepos},
                {"max_lang", maxLanguages}
            };

            SetQuery(string.Format(Query, userName));
            SetVariables(variables);
        }
    }
}
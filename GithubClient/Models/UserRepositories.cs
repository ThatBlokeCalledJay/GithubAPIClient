using System.Collections.Generic;
using Newtonsoft.Json;

namespace GithubClient.Models
{
    public class UserRepositories
    {
        public List<Repository> Repositories { get; set; }

        public static UserRepositories Parse(string json)
        {
            var repos = JsonConvert.DeserializeObject<List<Repository>>(json);

            return new UserRepositories
            {
                Repositories = repos
            };
        }
    }
}
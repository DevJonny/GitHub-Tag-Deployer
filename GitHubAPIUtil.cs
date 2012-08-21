using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace GitHub_Tag_Deployer
{

    class GitHubAPIUtil
    {

        private RestClient restClient;

        public GitHubAPIUtil()
        {
            restClient = new RestClient { BaseUrl = "https://github.com" };
        }

        public void fetchTag(string username, string password, string tagUrl, string deploymentDir)
        {

            restClient.Authenticator = new HttpBasicAuthenticator(username, password);

            var restRequest = new RestRequest { Resource = tagUrl.Replace("https://github.com", "") };

            var response = restClient.Execute(restRequest);

            FileStream fileStream = new FileStream(FileUtil.getTagFilePath(tagUrl, deploymentDir), FileMode.Create, FileAccess.Write);
            fileStream.Write(response.RawBytes, 0, response.RawBytes.Length);
            fileStream.Close();
        }
    }
}

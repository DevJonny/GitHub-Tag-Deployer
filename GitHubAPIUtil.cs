using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Configuration;

namespace GitHub_Tag_Deployer
{

    class GitHubAPIUtil
    {

        private RestClient restClient;

        public GitHubAPIUtil()
        {
            restClient = new RestClient { BaseUrl = "https://github.com" };
        }

        public void fetchTag(string username, string password, string tagUrl, string deploymentDir, bool? proxy)
        {

            restClient.Authenticator = new HttpBasicAuthenticator(username, password);
            
            SetupProxy(proxy);

            var restRequest = new RestRequest { Resource = tagUrl.Replace("https://github.com", "") };

            var response = restClient.Execute(restRequest);

            FileStream fileStream = new FileStream(FileUtil.getTagFilePath(tagUrl, deploymentDir), FileMode.Create, FileAccess.Write);
            fileStream.Write(response.RawBytes, 0, response.RawBytes.Length);
            fileStream.Close();
        }

        private void SetupProxy(bool? useProxy)
        {
            if (useProxy == true)
            {
                string proxyUserName = ConfigurationManager.AppSettings["ProxyUsername"];
                string proxyPassword = ConfigurationManager.AppSettings["ProxyPassword"];
                string proxyDomain = ConfigurationManager.AppSettings["ProxyDomain"];
                string proxyServer = ConfigurationManager.AppSettings["ProxyServer"];
                string proxyPort = ConfigurationManager.AppSettings["ProxyPort"];

                ICredentials credentials = new NetworkCredential(proxyUserName, proxyPassword, proxyDomain);

                restClient.Proxy = new WebProxy(proxyServer + ":" + proxyPort, true, new string[0], credentials);
            }
        }

        
    }
}

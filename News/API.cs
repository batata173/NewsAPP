using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace SantanderN
{
    class API
    {        
        public string Invoke(string url, string method)
        {
            var result = string.Empty;

            using (var client = new System.Net.WebClient()) //WebClient  
            {
                client.Headers.Add("Content-Type:application/json"); //Content-Type  
                client.Headers.Add("Accept:application/json");
                result = client.DownloadString(url + method); //URI  
            }
                 
            return  result;
        }

        internal List<string> InvokeGetStories(string url, string method)
        {
            System.Net.WebClient client = new System.Net.WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = System.Text.Encoding.UTF8;
            string json = client.DownloadString(url + method);

            return (JsonConvert.DeserializeObject<List<string>>(json));
        }

        internal void GetDetailFromStories(List<string> newStories, string url, string method)
        {
            

            foreach (var ns in newStories)
            {
                if(!new DB().StoryExists(ns))
                    new DB().SaveNews(GetStory(url, method, ns));
            }

        }

        public News GetStory(string url, string method, string ns)
        {
            System.Net.WebClient client = new System.Net.WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = System.Text.Encoding.UTF8;

            return JsonConvert.DeserializeObject<News>(client.DownloadString(url + method + ns + ".json"));
        }
      
    }
}

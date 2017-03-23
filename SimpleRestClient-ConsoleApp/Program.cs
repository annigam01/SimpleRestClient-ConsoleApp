using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Step2 add the usings
using System.Net.Http;
using System.Net.Http.Headers;

//Step 5 - Optional - if you want to parse the json to an object follow along
//add service reference dll System.Web.Extensions
using System.Web.Script.Serialization;
using System.IO;

namespace SimpleRestClient_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Step1 - install the nugetpackage - Microsoft.AspNet.WebApi.Client

            //Step4 -  call the method here and wait - so that main thread is not blocked
            RunAsync().Wait();

            //wait for user to press enter before quit
            Console.Read();
        }
        //Step3 create a async task method - just a standard practice, and put logic in that
        private static async Task RunAsync()
        {
            //create httpclient variable
            using (var client = new HttpClient())
            {
                //rest endpoint = https://api.github.com/users/annigam01
                // so base address is only https://api.github.com/, last part is variable

                string SharepointBaseURI = "https://api.github.com/";
                string UserAgentString = "User-Agent";
                string UserAgentValue = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident / 6.0)";

                //set the address to SP root
                client.BaseAddress = new Uri(SharepointBaseURI);

                //clear all the default headers
                client.DefaultRequestHeaders.Accept.Clear();

                //add "application/json" header in the request
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //add the user-agent header
                client.DefaultRequestHeaders.Add(UserAgentString, UserAgentValue);
                
                // make the request and get the responce
                HttpResponseMessage Resp = await client.GetAsync("/users/annigam01");

                if (Resp.IsSuccessStatusCode)
                {

                    //gets the json from responce object
                    String JSON = await Resp.Content.ReadAsStringAsync();
                    
                    //shows to user
                    Console.WriteLine(JSON);

                    //optional if you want to parse/serilise the JSON object, first create the class with all prop- same as you get JSON as
                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    GitHubUser User = ser.Deserialize<GitHubUser>(JSON);

                    Console.WriteLine(User.login);


                }


            }
        }
    }

    class GitHubUser
    {
        public string login { get; set; }
        public int id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
        public string name { get; set; }
        public string company { get; set; }
        public string blog { get; set; }
        public string location { get; set; }
        public string email { get; set; }
        public string hireable { get; set; }
        public string bio { get; set; }
        public string public_repos { get; set; }
        public string public_gists { get; set; }
        public string followers { get; set; }
        public string following { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }


    }
}

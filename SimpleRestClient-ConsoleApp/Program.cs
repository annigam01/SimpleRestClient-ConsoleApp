using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Step2 add the using
using System.Net.Http;
using System.Net.Http.Headers;

namespace SimpleRestClient_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Step4 -  call the method here and wait - so that main thread is not blocked
            RunAsync().Wait();

            //Step1 - install the nugetpackage - Microsoft.AspNet.WebApi.Client

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
                    string JSON = await Resp.Content.ReadAsStringAsync();

                    Console.WriteLine(JSON);

                }


            }
        }
    }
}

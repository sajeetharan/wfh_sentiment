using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace WorkFromHome
{
    public static class DecideSentinment
    {
        [FunctionName("DecideSentinment")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestMessage req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

             string Sentiment = "POSITIVE";

            //Getting the score from the Cognitive Service and determining the sentiment
             double score = await req.Content.ReadAsAsync<double>();

             if(score < 0.3){
                 Sentiment = "NEGATIVE";
             }
             else if(score < 0.6){
                 Sentiment = "NEUTRAL";
             }
             return req.CreateResponse(System.Net.HttpStatusCode.OK,Sentiment);
        }
    }
}

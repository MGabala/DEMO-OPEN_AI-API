using Microsoft.AspNetCore.Mvc;

using RestSharp;

namespace AI_API.Controllers
{
    [ApiController]
    [Route("modern-it.pl/ai")]
    public class AIController : ControllerBase
    {
        private readonly string APIURL = "https://api.openai.com/";

        [HttpGet,Route("modules")]
        public async Task<IActionResult> GetAllModels()
        {

            var client = new RestClient(APIURL);
            var request = new RestRequest("/v1/models", Method.Get);
            request.AddHeader("Authorization", $"Bearer {Environment.GetEnvironmentVariable("OPENAIKEY")}");
            RestResponse response = await client.ExecuteAsync(request);
            return Ok(response.Content);
        }

        [HttpGet, Route("model")]
        public async Task<IActionResult> GetModel(string model)
        {

            var client = new RestClient(APIURL);
            var request = new RestRequest($"/v1/models/{model}", Method.Get);
            request.AddHeader("Authorization", $"Bearer {Environment.GetEnvironmentVariable("OPENAIKEY")}");
            RestResponse response = await client.ExecuteAsync(request);
            return Ok(response.Content);
        }

        [HttpPost, Route("completions")]
        public async Task<IActionResult> PostCompletion(string model, string prompt, string maxTokens, string temperature)
        {
            //var client = new RestClient(APIURL);
            //var request = new RestRequest($"/v1/completions", Method.Post);
            //request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Authorization", $"Bearer {Environment.GetEnvironmentVariable("OPENAIKEY")}");
            //string body = $$"""
            //    {
            //    "model": "{{model}}",
            //    "prompt": "{{prompt}}",
            //    "max_tokens": {{maxTokens}},
            //    "temperature": {{temperature}}
            //     }            
            //    """;
            //request.AddParameter("application/json", body, ParameterType.RequestBody);
            //RestResponse response = await client.ExecuteAsync(request);

            //return Ok(response.Content);

            var options = new RestClientOptions("https://api.openai.com")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/v1/completions", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer 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");
            var body = @"{
" + "\n" +
            @"    ""model"": ""text-davinci-003"",
" + "\n" +
            @"    ""prompt"": ""Tell me a joke"",
" + "\n" +
            @"    ""max_tokens"": 25,
" + "\n" +
            @"    ""temperature"": 0
" + "\n" +
            @"}
" + "\n" +
            @"
" + "\n" +
            @"
" + "\n" +
            @"";
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
            return Ok(response.Content);
        }
    }
}

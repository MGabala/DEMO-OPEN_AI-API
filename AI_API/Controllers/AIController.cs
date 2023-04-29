using Microsoft.AspNetCore.Mvc;

using RestSharp;

using System.Reflection;

namespace AI_API.Controllers
{
    [ApiController]
    [Route("modern-it.pl/ai")]
    public class AIController : ControllerBase
    {
        private readonly string APIURL = "https://api.openai.com";

        [HttpGet,Route("modules")]
        public async Task<IActionResult> GetAllModels()
        {

            var client = new RestClient(APIURL);
            var request = new RestRequest("/v1/models", Method.Get);
            request.AddHeader("Authorization", $"Bearer {Environment.GetEnvironmentVariable("OPENAIKEY")}");
            RestResponse response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                return Ok(response.Content);
            }
            else
            {
                return BadRequest(response.Content);
            }
        }

        [HttpGet, Route("model")]
        public async Task<IActionResult> GetModel(string model)
        {

            var client = new RestClient(APIURL);
            var request = new RestRequest($"/v1/models/{model}", Method.Get);
            request.AddHeader("Authorization", $"Bearer {Environment.GetEnvironmentVariable("OPENAIKEY")}");
            RestResponse response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                return Ok(response.Content);
            }
            else
            {
                return BadRequest(response.Content);
            }
        }

        [HttpPost, Route("completions")]
        public async Task<IActionResult> PostCompletion(string model, string prompt, int maxTokens, decimal temperature)
        {
            var client = new RestClient(APIURL);
            var request = new RestRequest($"/v1/completions", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {Environment.GetEnvironmentVariable("OPENAIKEY")}");
            string body = $$"""
                {
                "model": "{{model}}",
                "prompt": "{{prompt}}",
                "max_tokens": {{maxTokens}},
                "temperature": {{temperature}}
                 }            
                """;
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return Ok(response.Content);
            }
            if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest(response.Content);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.Content);
            }

        }
        [HttpPost, Route("chat")]
        public async Task<IActionResult> PostChat()
        {
            var options = new RestClientOptions("https://api.openai.com")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/v1/chat/completions", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer BearerToken");
            var body = """
                {
                  "model": "gpt-3.5-turbo",
                  "messages": [{"role": "user", "content": "Show me simple PageModel class with used authorization attribute"}]
                }
                """;
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);

            return Ok(response.Content);
        }

        [HttpPost,Route("createImage")]
        public async Task<IActionResult> PostCreateImage(string prompt, int numberOfImages, string size)
        {
            var client = new RestClient(APIURL);
            var request = new RestRequest($"/v1/images/generations", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {Environment.GetEnvironmentVariable("OPENAIKEY")}");
            string body = $$"""
                {
                "prompt": "{{prompt}}",
                "n": {{numberOfImages}},
                "size": "{{size}}"
                 }            
                """;
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return Ok(response.Content);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest(response.Content);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.Content);
            }
        }

        [HttpPost, Route("createEmbeddings")]
        public async Task<IActionResult> PostCreateEmbeddings(string model, string input)
        {
            var client = new RestClient(APIURL);
            var request = new RestRequest($"/v1/embeddings", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {Environment.GetEnvironmentVariable("OPENAIKEY")}");
            string body = $$"""
                {
                "model": "{{model}}",
                "input": "{{input}}"
                 }            
                """;
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return Ok(response.Content);
            }
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return BadRequest(response.Content);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.Content);
            }
        }
    }
}

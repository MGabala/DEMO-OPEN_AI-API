using Microsoft.AspNetCore.Mvc;

using RestSharp;

namespace AI_API.Controllers
{
    [ApiController]
    [Route("modern-it.pl/ai")]
    public class AIController : ControllerBase
    {
        private readonly string APIURL = "https://api.openai.com/";

        [HttpGet,Route("getallmodules")]
        public async Task<IActionResult> GetAllModels()
        {

            var client = new RestClient(APIURL);
            var request = new RestRequest("/v1/models", Method.Get);
            request.AddHeader("Authorization", $"Bearer {Environment.GetEnvironmentVariable("OPENAIKEY")}");
            RestResponse response = await client.ExecuteAsync(request);
            return Ok(response.Content);
        }
    }
}

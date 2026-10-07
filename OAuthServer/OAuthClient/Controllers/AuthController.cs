using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace OAuthClient.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet("/callback")]
        public async Task<IActionResult> Callback(string code, string state)
        {
            var client = new HttpClient();
            var response = await client.PostAsync("http://localhost:5009/OAuth/token", new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("client_id", "client_app")
            }));

            var content = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<Dictionary<string, object>>(content);

            return Content($"Access Token: {tokenResponse["access_token"]}");
        }
    }
}

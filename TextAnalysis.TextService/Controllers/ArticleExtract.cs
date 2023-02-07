using Microsoft.AspNetCore.Mvc;

namespace TextAnalysis.TextService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleExtract : ControllerBase
    {
        private readonly ILogger<ArticleExtract> _logger;

        public ArticleExtract(ILogger<ArticleExtract> logger)
        {
            _logger = logger;
        }

        [HttpGet("ExtractText")]
        public async Task<string> ExtractText(string language, string url)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
              {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://text-analysis12.p.rapidapi.com/article-extraction/api/v1.3"),
                Headers =
                {
                    { "X-RapidAPI-Key", "b471e3c33emsh22a713e0f11efb6p13dd4djsnc6b9edf10c4d" },
                    { "X-RapidAPI-Host", "text-analysis12.p.rapidapi.com" },
                },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        { "language", language },
                        { "url", url },
                    }),
              };
            using (var response = await client.SendAsync(request))
                {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
                return body;
                }           
        }
    }
}
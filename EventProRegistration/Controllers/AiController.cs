using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using EventProRegistration.DTOs;

namespace EventProRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        : ControllerBase
    {
        private readonly string _googleApiKey = configuration["GoogleApiKey"]
                                                ?? throw new ArgumentNullException(nameof(configuration),
                                                    "Google API Key is not configured.");

        [HttpPost("generate-event")]
        public async Task<IActionResult> GenerateEventDetails([FromBody] AiPromptRequest request)
        {
            var client = httpClientFactory.CreateClient();
            var apiUrl =
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={_googleApiKey}";

            const string systemPrompt =
                "You are an event planning assistant. Based on the user's idea, generate a JSON object with four fields: 'name' (a creative and professional event name), 'location' (a plausible city or venue), 'date' (a plausible future date in yyyy-MM-dd format), and 'price' (a plausible ticket price as a number, like 25 or 100, or 0 for a free event). Your response must be ONLY the raw JSON object, without any markdown formatting like ```json.";
            var userPrompt = $"The user's event idea is: '{request.Prompt}'";

            var requestBody = new GeminiRequest
            {
                contents = [new Content { parts = [new Part { text = $"{systemPrompt}\n\n{userPrompt}" }] }]
            };

            var jsonRequestBody = JsonSerializer.Serialize(requestBody);
            var httpContent = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(apiUrl, httpContent);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, $"Error from AI service: {errorContent}");
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var geminiResponse = JsonSerializer.Deserialize<GeminiResponse>(jsonResponse,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var aiRawText = geminiResponse?.candidates[0].content.parts[0].text;

                if (string.IsNullOrEmpty(aiRawText))
                {
                    return StatusCode(500, "AI returned an empty or invalid response.");
                }

                var startIndex = aiRawText.IndexOf('{');
                var endIndex = aiRawText.LastIndexOf('}');

                if (startIndex == -1 || endIndex == -1)
                {
                    return StatusCode(500, "AI did not return valid JSON in its response.");
                }

                var cleanJson = aiRawText.Substring(startIndex, endIndex - startIndex + 1);

                return Content(cleanJson, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal error occurred: {ex.Message}");
            }
        }
    }
}
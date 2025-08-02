using System.Text.Json.Serialization; 

namespace EventProRegistration.DTOs
{
    public class GeminiRequest
    {
        [JsonPropertyName("contents")] 
        public Content[] contents { get; set; }
    }

    public class Content
    {
        [JsonPropertyName("parts")]
        public Part[] parts { get; set; }
    }

    public class Part
    {
        [JsonPropertyName("text")]
        public string text { get; set; }
    }
    public class GeminiResponse
    {
        [JsonPropertyName("candidates")]
        public Candidate[] candidates { get; set; }
    }

    public class Candidate
    {
        [JsonPropertyName("content")]
        public Content content { get; set; }
    }

    public class AiPromptRequest
    {
        public string Prompt { get; set; }
    }
}
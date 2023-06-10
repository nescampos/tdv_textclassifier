using System.Text.Json.Serialization;

namespace TextAIClassifierWeb;

public class ClassificationResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    
    [JsonPropertyName("classifications")]
    public List<Classification> Classifications { get; set; }
}

public class Classification
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    
    [JsonPropertyName("input")]
    public string? Input { get; set; }
    
    [JsonPropertyName("prediction")]
    public string Prediction { get; set; }
    
    [JsonPropertyName("confidence")]
    public float Confidence { get; set; }
}
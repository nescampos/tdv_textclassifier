using System.Text.Json.Serialization;

namespace TextAIClassifierWeb;


public class ClassificationRequest
{
    [JsonPropertyName("inputs")]
    public List<string> Inputs { get; set; }
    
    [JsonPropertyName("examples")]
    public List<CategoryRequestExample> Examples { get; set; }
    
    [JsonPropertyName("truncate")]
    public string? Truncate { get; set; }
}

public class CategoryRequestExample
{
    [JsonPropertyName("text")]
    public string Text { get; set; }
    
    [JsonPropertyName("label")]
    public string Label { get; set; }
}

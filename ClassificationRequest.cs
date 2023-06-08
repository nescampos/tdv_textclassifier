namespace TextAIClassifierWeb;

public class ClassificationRequest
{
    public List<string>? inputs { get; set; }
    public List<CategoryRequestExample>? examples { get; set; }
    public string? truncate { get; set; }
}

public class CategoryRequestExample
{
    public string? text { get; set; }
    public string? label { get; set; }
}

public class ClassificationResponse
{
    public string? id { get; set; }
    public List<Classification>? classifications { get; set; }
}

public class Classification
{
    public string? id { get; set; }
    public string? input { get; set; }
    public string? prediction { get; set; }
    public double? confidence { get; set; }
}
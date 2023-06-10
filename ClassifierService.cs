using System.Text.Json;
using System.Text;

namespace TextAIClassifierWeb;

public class ClassifierService
{
    private static readonly List<CategoryRequestExample> CategoryRequestExamples = new()
    {
        new() {Text = "I have a problem with my account", Label = "Customer Service"},
        new() {Text = "I need help with my service", Label = "Customer Service"},
        new() {Text = "I am sending you the sales forecast for the next month", Label = "Finance"},
        new() {Text = "I need help to understand what is the requirements for the next advertising", Label = "Marketing"},
        new() {Text = "Here is the clustering with the customers", Label = "Marketing"},
        new() {Text = "I need to request vacation days", Label = "Human Resources"},
        new() {Text = "You have to prepare the internal training meeting", Label = "Human Resources"},
        new() {Text = "Tomorrow we will go to the company party", Label = "Other (or manual classification)"},
        new() {Text = "There are no pending requests", Label = "Other (or manual classification)"}
    };
    
    private readonly IConfiguration configuration;
    private readonly HttpClient httpClient;

    public ClassifierService(IConfiguration configuration, HttpClient httpClient)
    {
        this.configuration = configuration;
        this.httpClient = httpClient;
    }

    public async Task<List<Classification>> ClassifyText(string message)
    {
        var classificationRequest = new ClassificationRequest
        {
            Inputs = new List<string> {message},
            Truncate = "END",
            Examples = CategoryRequestExamples
        };
        var json = JsonSerializer.Serialize(classificationRequest);
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.cohere.ai/classify"),
            Headers =
            {
                {"accept", "application/json"},
                {"Cohere-Version", "2022-12-06"},
                {"authorization", "Bearer " + configuration["CohereApiKey"]},
            },
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        using var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        var bodyResponse = JsonSerializer.Deserialize<ClassificationResponse>(body);
        return bodyResponse.Classifications;
    }
}
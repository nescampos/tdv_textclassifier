using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Configuration;


namespace TextAIClassifierWeb;

public class ClassifierService
{
    private readonly IConfiguration configuration;
    public ClassifierService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<List<Classification>> ClassifyText(string message)
    {
        var client = new HttpClient();
        List<CategoryRequestExample> exampleList = GenerateExamples();
        ClassificationRequest classificationRequest = new ClassificationRequest
        {
            inputs = new List<string> { message },
            truncate = "END",
            examples = exampleList
        };
        var json = JsonSerializer.Serialize(classificationRequest);
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.cohere.ai/classify"),
            Headers =
            {
                { "accept", "application/json" },
                { "Cohere-Version", "2022-12-06" },
                { "authorization", "Bearer "+configuration["CohereApiKey"] },
            },
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var bodyResponse = JsonSerializer.Deserialize<ClassificationResponse>(body);
            if (bodyResponse != null)
            {
                return bodyResponse.classifications;
            }
        }
        return null;
    }



    private static List<CategoryRequestExample> GenerateExamples()
    {
        List<CategoryRequestExample> exampleList = new List<CategoryRequestExample>();
        exampleList.Add(new CategoryRequestExample { text = "I have a problem with my account", label = "Customer Service" });
        exampleList.Add(new CategoryRequestExample { text = "I need help with my service", label = "Customer Service" });
        exampleList.Add(new CategoryRequestExample { text = "I am sending you the sales forecast for the next month", label = "Finance" });
        exampleList.Add(new CategoryRequestExample { text = "I am sending you the sales forecast for the next month", label = "Finance" });
        exampleList.Add(new CategoryRequestExample { text = "I need help to understand what is the requeriments for the next advertising", label = "Marketing" });
        exampleList.Add(new CategoryRequestExample { text = "Here is the clustering with the customers", label = "Marketing" });
        exampleList.Add(new CategoryRequestExample { text = "I need to request vacation days", label = "Human Resources" });
        exampleList.Add(new CategoryRequestExample { text = "You have to prepare the internal training meeting", label = "Human Resources" });
        exampleList.Add(new CategoryRequestExample { text = "Tomorrow we will go to the company party", label = "Other (or manual classification)" });
        exampleList.Add(new CategoryRequestExample { text = "There are no pending requests", label = "Other (or manual classification)" });
        return exampleList;
    }


}

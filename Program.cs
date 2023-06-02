using Microsoft.Extensions.Configuration;

IConfigurationRoot configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();


Console.WriteLine("Please, enter your message:");

string message = Console.ReadLine();

if(!string.IsNullOrWhiteSpace(message))
{
    ClassifierService classifierService = new ClassifierService(configuration);
    await classifierService.ClassifyText(message);
}
else
{
    Console.WriteLine("Please, enter a valid message");
}
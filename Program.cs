using TextAIClassifierWeb;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddTransient<ClassifierService>();

var app = builder.Build();

app.MapControllers();

app.Run();
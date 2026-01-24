using BookInformation.WebAPI.Extentions;

var builder = WebApplication.CreateBuilder(args).ConfigureApplication();

var app = builder.Build();

app.ConfigurePipeline();

app.Run();

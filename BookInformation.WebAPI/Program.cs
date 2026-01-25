using BookInformation.WebAPI.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args).ConfigureApplication();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.ConfigurePipeline();

app.Run();

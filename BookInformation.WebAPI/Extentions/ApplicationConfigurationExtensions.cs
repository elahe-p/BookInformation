using BookInformation.Infrastructure.Database;
using BookInformation.WebAPI.Middleware;
using Microsoft.EntityFrameworkCore;

namespace BookInformation.WebAPI.Extentions;

public static class ApplicationConfigurationExtensions
{
    public static WebApplicationBuilder ConfigureApplication(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        #region Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        #endregion

        services.AddControllers();


        #region Services
        // services.AddScoped<>();

        #endregion

        #region DataBase
        string? connectionString = configuration.GetConnectionString("applicationConnectionString");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        #endregion

        return builder;

    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web v1"));
        }
        else
        {
            app.UseHttpsRedirection();
        }

        app.UseRouting();

        #region MiddleWare
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        #endregion

        app.MapControllers();

        return app;
    }
}

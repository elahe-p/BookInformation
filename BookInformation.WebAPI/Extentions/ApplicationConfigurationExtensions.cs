using BookInformation.Application.Abstraction.Repositories;
using BookInformation.Application.Abstraction.Services;
using BookInformation.Application.Services;
using BookInformation.Infrastructure.Database;
using BookInformation.Infrastructure.Repositories;
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

        #region Repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IAuditLogRepository, AuditLogRepository>();

        #endregion

        #region Services
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IAuditLogService, AuditLogService>();

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
            app.UseSwaggerUI();
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

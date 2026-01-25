using BookInformation.Application.Abstraction;
using BookInformation.Application.Abstraction.Repositories;
using BookInformation.Application.Abstraction.Services;
using BookInformation.Application.Mappings;
using BookInformation.Application.Services;
using BookInformation.Infrastructure.Database;
using BookInformation.Infrastructure.Repositories;
using BookInformation.WebAPI.Middleware;
using Microsoft.EntityFrameworkCore;

namespace BookInformation.WebAPI.Extensions;

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
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IAuditLogRepository, AuditLogRepository>();

        #endregion

        #region Services
        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IAuditLogService, AuditLogService>();

        #endregion

        builder.Services.AddAutoMapper(typeof(ConfigureMapping));

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
        app.UseHttpsRedirection();

        #region MiddleWare
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        #endregion

        app.UseRouting();
        app.MapControllers();

        return app;
    }
}

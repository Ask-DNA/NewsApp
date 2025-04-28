using Microsoft.AspNetCore.Mvc;
using NewsApp.Server.Domain.Interfaces.ExternalServices;
using NewsApp.Server.Domain.Interfaces.InternalServices;
using NewsApp.Server.Domain.Services;
using NewsApp.Server.Infrastructure.Data.Interfaces;
using NewsApp.Server.Infrastructure.Data.Repository;
using NewsApp.Server.Infrastructure.WebParser;
using NewsApp.Server.UseCases.InputPorts;
using NewsApp.Server.UseCases.Interactors;
using NewsApp.Server.UseCases.OutputPorts;
using NewsApp.Server.WebAPI.Presenters;

namespace NewsApp.Server.WebAPI
{
    internal static class ConfigurationManager
    {
        internal static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddCors(options => {
                options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            // В данном варианте распределенный кэш на самом деле не распределенный,
            // но через этот же интерфейс можно подключить распределенную реализацию (к примеру, Redis)
            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddOpenApiDocument();

            builder.Services.AddTransient<IWebParser, HtmlParser>();
            builder.Services.AddTransient<INewsRepository, NewsRepository>();

            builder.Services.AddTransient<INewsService, NewsService>();

            builder.Services.AddTransient<IGetNewsInputPort<IActionResult>, GetNewsInteractor<IActionResult>>();

            builder.Services.AddTransient<INewsPagePresenter<IActionResult>, NewsPagePresenter>();
        }

        internal static void ConfigureApp(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                // Add OpenAPI 3.0 document serving middleware
                // Available at: http://localhost:<port>/swagger/v1/swagger.json
                app.UseOpenApi();

                // Add web UIs to interact with the document
                // Available at: http://localhost:<port>/swagger
                app.UseSwaggerUi(); // UseSwaggerUI Protected by if (env.IsDevelopment())
            }
            app.UseCors("AllowAll");
            app.MapControllers();
        }
    }
}

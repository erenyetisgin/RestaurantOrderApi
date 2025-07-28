using System.Reflection;
using RestaurantOrderApi.Repositories;
using RestaurantOrderApi.Services;

namespace RestaurantOrderApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        
        // Configure Swagger with XML documentation
        builder.Services.AddSwaggerGen(options =>
        {
            // Set the comments path for the Swagger JSON and UI
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
            
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Restaurant Order API",
                Version = "v1",
                Description = "An API for managing restaurant orders and menus"
            });
        });

        // Register Services and Repositories.
        builder.Services.AddSingleton<IMenuRepository, InMemoryMenuRepository>();
        builder.Services.AddSingleton<IMenuService, MenuService>();

        builder.Services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();
        builder.Services.AddSingleton<IOrderService, OrderService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();                        // Serve Swagger JSON
            app.UseSwaggerUI();                      // Serve Swagger UI
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
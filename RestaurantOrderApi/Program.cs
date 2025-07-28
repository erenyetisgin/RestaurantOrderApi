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
        builder.Services.AddEndpointsApiExplorer(); // Enables endpoint discovery
        builder.Services.AddSwaggerGen();           // Generates OpenAPI docs

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
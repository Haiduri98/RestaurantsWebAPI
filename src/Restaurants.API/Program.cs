using Restaurants.API.Extensions;
using Restaurants.API.Middleware;
using Restaurants.Application.Extensions;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);


    builder.Services.AddEndpointsApiExplorer();


    builder.AddPresentation();
    builder.Services.AddInfrastructure(builder.Configuration); // extension method
    builder.Services.AddApplication();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    // builder.Services.AddEndpointsApiExplorer();
    // builder.Services.AddSwaggerGen();


    var app = builder.Build();

    // for turning the seeding mechanism on.
    var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
    await seeder.SeedAsync();

    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseSerilogRequestLogging();




    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.MapGroup("api/identity")
        .WithTags("Identity")
        .MapIdentityApi<User>();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program { }
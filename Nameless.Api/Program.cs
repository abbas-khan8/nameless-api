using Serilog;
using Nameless.Domain.Helpers;
using Nameless.Domain.Repositories;
using Nameless.Domain.Services;
using Nameless.Infrastructure.Helpers;
using Nameless.Infrastructure.Repositories;
using Nameless.Infrastructure.Services;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Serilog configuration
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Debug()
        .CreateLogger();
    builder.Host.UseSerilog(Log.Logger);

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Dependency injections
    builder.Services.AddScoped<IConnectionFactory, ConnectionFactory>();

    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<IStockRepository, StockRepository>();

    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddScoped<IStockService, StockService>();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.RoutePrefix = "";
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        });
    }

    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();

    app.UseRouting();

    //Enable CORS
    app.UseCors("AllowAll");

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

    Log.Information("Application Starting");
}
catch (Exception ex)
{
    Log.Error(ex, "An error occured during application startup");
}
finally
{
    Log.CloseAndFlush();
}
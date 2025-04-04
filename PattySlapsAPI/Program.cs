using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PattySlaps;
using PattySlaps.Data;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Enable CORS for all origins
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        // Add services to the container.
        builder.Services.AddControllers();

        // Register DbContext with dependency injection
        string connectionString = "server=localhost;port=3307;database=PattySlapsDB;user=root;password=patty2025$slaps;";
        builder.Services.AddDbContext<PattySlapsDbContext>(options =>
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 23))));

        // Add Swagger services
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Register repositories
        Type serviceType = typeof(Repository<>);

        builder.Services.AddScoped<Repository<InventoryRecord>>();
        builder.Services.AddScoped<Repository<Item>>();
        builder.Services.AddScoped<Repository<WasteRecord>>();
        builder.Services.AddScoped<Repository<HireRequest>>();
        builder.Services.AddScoped<Repository<Applicant>>();

        builder.Services.AddScoped(serviceType);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PattySlaps API V1");
                c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
            });
        }

        // Use CORS before routing and other middleware
        app.UseCors("AllowAll");  // Ensure CORS policy is actually applied

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}

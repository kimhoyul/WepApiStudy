using Microsoft.EntityFrameworkCore;
using ServerStudy.Data;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        var app = builder.Build();
        app.MapGet("/", () => "HelloWrold");
        app.MapGet("/test", () => "HelloWrold");
        
        app.MapControllers();

        app.Run();
    }
}
using CustomerPortal.ReportingService.BackgroundServices;

namespace CustomerPortal.ReportingService;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        builder.Services.AddHostedService<UsersReportBackgroundService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
            app.MapOpenApi();

        app.MapControllers();
        app.Run();
    }
}

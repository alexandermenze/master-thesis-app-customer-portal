using CustomerPortal.Extensions;
using CustomerPortal.ReportingService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<UsersBackgroundService>();

builder.Services.AddHttpClient(
    "UserAuthService",
    o =>
        o.BaseAddress = new Uri(
            builder.Configuration.GetValueOrThrow<string>("UserAuthService:BaseUrl")
        )
);

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

await app.RunAsync();

using RazorPagesWeb.NetCore.EF.Demo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.ApplicationInsights;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("AzureSqlConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddRazorPages();


//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
//builder.Logging.AddAzureWebAppDiagnostics();// Log Stream
//builder.Logging.AddApplicationInsights( configureTelemetryConfiguration: );

//builder.Logging.AddFilter<ApplicationInsightsLoggerProvider>(
//    "",
//    LogLevel.Information
//);
//builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);

builder.Logging.AddApplicationInsights(
    configureTelemetryConfiguration: config =>
    {
        config.ConnectionString =
            builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"];
    },
    configureApplicationInsightsLoggerOptions: options =>
    {
        // options.TrackExceptionsAsExceptionTelemetry = true;
        // options.IncludeScopes = true;
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

using Serilog;
using TimeCounterFrontend.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Configuration["SplunkRum:Token"] = Environment.GetEnvironmentVariable("SPLUNK_RUM_TOKEN") ?? "";
builder.Configuration["SplunkRum:Environment"] = Environment.GetEnvironmentVariable("SPLUNK_RUM_ENVIRONMENT") ?? "";
builder.Configuration["SplunkRum:Enabled"] = Environment.GetEnvironmentVariable("SPLUNK_RUM_ENABLED") ?? "false";
builder.Configuration["Serilog:Seq:ServerUrl"] = Environment.GetEnvironmentVariable("SEQ_SERVER_URL") ?? "http://localhost:5341";

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .WriteTo.Seq(context.Configuration["Serilog:Seq:ServerUrl"] ?? "http://localhost:5341")
    .WriteTo.Console());


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

using Serilog;
using SquibbBot13K.Blazor.Client.Pages;
using SquibbBot13K.Blazor.Components;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

Log.Logger = new LoggerConfiguration()
     .MinimumLevel.Debug()
     .Enrich.FromLogContext()
     .ReadFrom.Configuration(builder.Configuration)
     .CreateLogger();

builder.Services.Configure<SquibbBot13K.Blazor.Models.Options.Discord>(builder.Configuration.GetSection("Discord"));
builder.Services.AddHostedService<SquibbBot13K.Blazor.Services.Discord>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

app.Run();

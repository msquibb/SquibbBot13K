// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using Nefarius.DSharpPlus.Extensions.Hosting;
using Serilog;
using SquibbBot13K.Discord.Bot.Services;

//Console.WriteLine("Hello, World!");

using IHost host = CreateHostBuilder(args).Build();
//await RunBot(host.Services.GetRequiredService<StartupService>());


static IHostBuilder CreateHostBuilder(string[] args) =>
	Host.CreateDefaultBuilder(args).ConfigureServices(async (context, services) =>
{
	services.AddLogging(loggingBuilder =>
	{
		var logger = new LoggerConfiguration()
			.ReadFrom.Configuration(context.Configuration)
			.WriteTo.Console()
			.CreateLogger();
		loggingBuilder.AddSerilog(logger);
	});

	

	//services.AddDiscord(opt =>
	//{
	//	opt.Token = context.Configuration["SquibbBot13K:Token"];
	//});
	//services.AddDiscordHostedService();
	services.AddHttpClient();
	services.AddHostedService<SquibbBot13K.Discord.Bot.SquibbBot13K>();

});
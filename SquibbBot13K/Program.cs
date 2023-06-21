//using Discord;
//using Discord.Commands;
//using Discord.WebSocket;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SquibbBot13K.Modules.Admin;
using SquibbBot13K.Services;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace SquibbBot13K
{
	class Program
	{
		//private DiscordSocketClient _client;
		//private readonly IConfiguration _config;
		//private readonly string _environment;

		public Program()
		{
			//var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty;
			//_environment = environmentName;

			//var _builder = new ConfigurationBuilder()
			//        .SetBasePath(AppContext.BaseDirectory)
			//        .AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: true)
			//        .AddUserSecrets("0522ca63-cbdc-4658-922d-14d332b29535")
			//        .AddEnvironmentVariables();


			//_config = _builder.Build(); 
		}

		static async Task Main(string[] args)
		{
			var services = new ServiceCollection();
			ConfigureServices(services);
			var serviceProvider = services.BuildServiceProvider();

			//var discord = serviceProvider.GetService<DiscordClient>();
			//var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
			//{
			//	Services = serviceProvider,
			//	StringPrefixes = new[] { "!" }
			//});

			await serviceProvider.GetService<SquibbBot13K>().StartAsync();
			//new SquibbBot13K().StartAsync().GetAwaiter().GetResult();
			//new Program().MainAsync().GetAwaiter().GetResult();
		}

		private static void ConfigureServices(IServiceCollection services)
		{
			var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty;
			var isDevelopment = string.IsNullOrEmpty(environmentName) || environmentName.Equals("development", StringComparison.InvariantCultureIgnoreCase);

			services.AddLogging(builder =>
			{
				builder.AddConsole();
				builder.AddDebug();
			});

			var config = new ConfigurationBuilder()
				.SetBasePath(AppContext.BaseDirectory)
				.AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: true)
				.AddEnvironmentVariables()
				.AddUserSecrets<Program>()
				.Build();
			

			services.Configure<SquibbBot13KSecrets>(config.GetSection(nameof(SquibbBot13KSecrets)));

			services.AddTransient<SquibbBot13K>();

			services.AddHttpClient()
			.AddSingleton<CommandHandler>()
			.AddSingleton<StartupService>()
			.AddSingleton<UserInteraction>()
			//.AddSingleton<Modules.Feline>()
			.AddSingleton<LoggingService>();

			services.AddSingleton(new DiscordClient(new DiscordConfiguration()
			{
				Token = config["SquibbBot13KSecrets:Token"],
				TokenType = TokenType.Bot,
				Intents = DiscordIntents.All
			}));
		}

	}
}

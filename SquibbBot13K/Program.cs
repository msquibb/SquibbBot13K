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

			//var discord = new DiscordClient(new DiscordConfiguration()
			//{
			//	Token = config["SquibbBot13KSecrets:Token"],
			//	TokenType = TokenType.Bot
			//});


			//services.AddSingleton(discord);

			services.AddSingleton(new DiscordClient(new DiscordConfiguration()
			{
				Token = config["SquibbBot13KSecrets:Token"],
				TokenType = TokenType.Bot
			}));
		}


		//public async Task MainAsync()
		//{

		//    using (var services = ConfigureServices())
		//    {
		//        var client = services.GetRequiredService<DiscordSocketClient>();                
		//        var secureSettings = _config.GetSection(nameof(SquibbBot13KSecrets)).Get<SquibbBot13KSecrets>();

		//        _client = client;

		//        _client.Log += LogAsync;
		//        _client.Ready += ReadyAsync;
		//        services.GetRequiredService<CommandService>().Log += LogAsync;

		//        string token = string.Empty;

		//        token = secureSettings.Token;


		//        await _client.LoginAsync(TokenType.Bot, token);
		//        await _client.StartAsync();

		//        await services.GetRequiredService<CommandHandler>().InitializeAsync();

		//        await Task.Delay(Timeout.Infinite);
		//    }
		//}

		//private Task ReadyAsync()
		//{
		//    Console.WriteLine($"Connected as -> [{_client.CurrentUser}] :)");
		//    return Task.CompletedTask;
		//}

		//private Task LogAsync(LogMessage msg)
		//{
		//    Console.WriteLine(msg.ToString());
		//    return Task.CompletedTask;
		//}

		//private ServiceProvider ConfigureServices()
		//{
		//    var services = new ServiceCollection();
		//    services.AddSingleton(_config);
		//    services.AddSingleton<DiscordSocketClient>();
		//    services.AddSingleton<CommandService>();
		//    services.AddSingleton<CommandHandler>();
		//    services.AddSingleton<LoggingService>();

		//    return services.BuildServiceProvider();
		//}

	}
}

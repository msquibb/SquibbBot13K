using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using SquibbBot13K.Services;
using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace SquibbBot13K
{
	class Program
	{
		private DiscordSocketClient _client;
		private readonly IConfiguration _config;

		public Program()
		{
			var _builder = new ConfigurationBuilder()
					.SetBasePath(AppContext.BaseDirectory)
					.AddJsonFile(path: "config.json");

			_config = _builder.Build();
		}

		static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();


		public async Task MainAsync()
		{

			using (var services = ConfigureServices())
			{
				var client = services.GetRequiredService<DiscordSocketClient>();
				_client = client;

				_client.Log += LogAsync;
				_client.Ready += ReadyAsync;
				services.GetRequiredService<CommandService>().Log += LogAsync;

				var token = _config["Token"];					
				// Some alternative options would be to keep your token in an Environment Variable or a standalone file.
				// var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
				// var token = File.ReadAllText("token.txt");
				// var token = JsonConvert.DeserializeObject<AConfigurationClass>(File.ReadAllText("config.json")).Token;

				await _client.LoginAsync(TokenType.Bot, token);
				await _client.StartAsync();

				await services.GetRequiredService<CommandHandler>().InitializeAsync();

				await Task.Delay(Timeout.Infinite);
			}
		}

		private Task ReadyAsync()
		{
			Console.WriteLine($"Connected as -> [{_client.CurrentUser}] :)");
			return Task.CompletedTask;
		}

		private Task LogAsync(LogMessage msg)
		{
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}

		private ServiceProvider ConfigureServices()
		{
			var services = new ServiceCollection();
			services.AddSingleton(_config);
			services.AddSingleton<DiscordSocketClient>();
			services.AddSingleton<CommandService>();
			services.AddSingleton<CommandHandler>();
			services.AddSingleton<LoggingService>();

			return services.BuildServiceProvider();
		}

	}
}

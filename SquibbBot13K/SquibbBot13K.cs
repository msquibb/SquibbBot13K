//using Discord;
//using Discord.Commands;
//using Discord.WebSocket;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;
using SquibbBot13K.Modules.Admin;
using SquibbBot13K.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SquibbBot13K
{
	public class SquibbBot13K
	{

		private DiscordClient _discord;
		private readonly ILogger<SquibbBot13K> _logger;


		public SquibbBot13K(ILogger<SquibbBot13K> logger, DiscordClient client)
		{
			_logger = logger;
			_discord = client;
		}

		public async Task StartAsync()
		{
			_logger.LogInformation("Started!");

			//var depco = new ServiceCollection()
			//	.AddSingleton<Modules.Feline>().BuildServiceProvider();
				
			var commands = _discord.UseCommandsNext(new CommandsNextConfiguration()
			{
				StringPrefixes = new[] { "!" },
				//Services = depco,
				EnableMentionPrefix = true,
				EnableDms = true
			});

			commands.RegisterCommands<Modules.Feline>();

			await _discord.ConnectAsync();
			
			
			//.AddSingleton(new CommandService(new CommandServiceConfig
			//{
			//    DefaultRunMode = RunMode.Async,
			//    LogLevel = LogSeverity.Verbose,
			//    CaseSensitiveCommands = false,
			//    ThrowOnError = false
			//}))
			//.AddSingleton<DiscordSocketClient>()
			//.AddSingleton<CommandHandler>()
			//.AddSingleton<StartupService>()
			//.AddSingleton<UserInteraction>()
			//.AddSingleton<LoggingService>();


			//Instantiate logger/tie-in logging
			//serviceProvider.GetRequiredService<LoggingService>();

			//Start the bot
			//await serviceProvider.GetRequiredService<StartupService>().StartAsync();

			//Load up services
			//serviceProvider.GetRequiredService<CommandHandler>();
			//serviceProvider.GetRequiredService<UserInteraction>();
			//_client = serviceProvider.GetRequiredService<DiscordShardedClient>();

			//_client.Log += LogAsync;
			//_client.ShardReady += _client_ShardReady;
			//_client.Ready += ReadyAsync;
			//serviceProvider.GetRequiredService<CommandService>().Log += LogAsync;

			//_client = serviceProvider.GetRequiredService<DiscordClient>();


			//await serviceProvider.GetRequiredService<CommandHandler>().InitializeAsync();

			//Block this program until it is closed.
			await Task.Delay(-1);
		}

		//private Task _client_ShardReady(DiscordSocketClient arg)
		//{
		//    Console.WriteLine($"Connected as -> [{arg.CurrentUser}] =)");
		//    return Task.CompletedTask;
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


	}
}

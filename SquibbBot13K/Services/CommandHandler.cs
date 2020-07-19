using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SquibbBot13K.Services
{
	public class CommandHandler
	{
		private readonly IConfiguration _config;
		private readonly DiscordSocketClient _client;
		private readonly CommandService _commands;
		private readonly IServiceProvider _services;

		// Retrieve client and CommandService instance via ctor
		public CommandHandler(IServiceProvider services)
		{
			_config = services.GetRequiredService<IConfiguration>();
			_commands = services.GetRequiredService<CommandService>();
			_client = services.GetRequiredService<DiscordSocketClient>();
			_services = services;

			_commands.CommandExecuted += CommandExecutedAsync;
			_client.MessageReceived += MessageReceivedAsync;
		}
		
		public async Task InitializeAsync()
		{
			await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
		}
		
		public async Task MessageReceivedAsync(SocketMessage rawMessage)
		{
			if (!(rawMessage is SocketUserMessage message)) return;
			if (message.Source != MessageSource.User) return;

			var argPos = 0;

			char prefix = Char.Parse(_config["Prefix"]);
			//if (!message.HasCharPrefix('!', ref argPos)) return;
			//if (!message.HasMentionPrefix(_client.CurrentUser, ref argPos)) return;
			if (!(message.HasMentionPrefix(_client.CurrentUser, ref argPos) || message.HasCharPrefix(prefix, ref argPos)))
			{
				return;
			}

			var context = new SocketCommandContext(_client, message);

			await _commands.ExecuteAsync(context, argPos, _services);


		}

		public async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
		{
			if (!command.IsSpecified)
			{
				System.Console.WriteLine($"Command failed to execute for [{context.User.Username}] <-> [{result.ErrorReason}]!");
				return;
			}

			if (result.IsSuccess)
			{
				System.Console.WriteLine($"Command [{command.Value.Name}] executed for -> [{context.User.Username}]");
				return;
			}

			await context.Channel.SendMessageAsync($"Sorry, {context.User.Username}... something went wrong -> [{result}]!");
		}


	}
}

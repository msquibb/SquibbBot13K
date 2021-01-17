//using Discord;
//using Discord.Commands;
//using Discord.WebSocket;
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
		private readonly IConfigurationRoot _config;
		//private readonly DiscordShardedClient _client;
		//private readonly DiscordSocketClient _client;
		//private readonly CommandService _commands;
		private readonly IServiceProvider _services;

		// Retrieve client and CommandService instance via ctor
		public CommandHandler(IServiceProvider services)
		{
			_config = services.GetRequiredService<IConfigurationRoot>();
			//_commands = services.GetRequiredService<CommandService>();
			//_client = services.GetRequiredService<DiscordShardedClient>();
			//_client = services.GetRequiredService<DiscordSocketClient>();
			_services = services;

			//_commands.CommandExecuted += CommandExecutedAsync;
			//_client.MessageReceived += MessageReceivedAsync;
		}
		
		public async Task InitializeAsync()
		{
			//await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
		}
		
		//public async Task MessageReceivedAsync(SocketMessage rawMessage)
		//{
		//	// Don't handle the command if it is a system message
		//	var message = rawMessage as SocketUserMessage;
		//	if (message == null) return;

		//	// Don't listen to bots
		//	if (message.Source != MessageSource.User)
		//	{
		//		return;
		//	}

		//	// Mark where the prefix ends and the command begins
		//	int argPos = 0;

		//	// Create a Command Context
		//	var context = new ShardedCommandContext(_client, message);

		//	char prefix = Char.Parse(_config["prefix"]);

		//	//var serverPrefix = Char.Parse(_config["Prefix"]);

		//	//if (serverPrefix != null)
		//	//{
		//	//	prefix = serverPrefix.Prefix;
		//	//}

		//	// Determine if the message has a valid prefix, adjust argPos
		//	if (!(message.HasMentionPrefix(_client.CurrentUser, ref argPos) || message.HasCharPrefix(prefix, ref argPos))) return;

		//	////Check blacklist
		//	//List<Blacklist> blacklist = new List<Blacklist>();

		//	//using (var db = new NinjaBotEntities())
		//	//{
		//	//	blacklist = db.Blacklist.ToList();
		//	//}
		//	//if (blacklist != null)
		//	//{
		//	//	var matched = blacklist.Where(b => b.DiscordUserId == (long)context.User.Id).FirstOrDefault();
		//	//	if (matched != null)
		//	//	{
		//	//		return;
		//	//	}
		//	//}

		//	// Execute the Command, store the result            
		//	var result = await _commands.ExecuteAsync(context, argPos, _services);

		//	await LogCommandUsage(context, result);
		//	// If the command failed, notify the user
		//	if (!result.IsSuccess)
		//	{
		//		if (result.ErrorReason != "Unknown command.")
		//		{
		//			await message.Channel.SendMessageAsync($"**Error:** {result.ErrorReason}");
		//		}
		//	}
		//	//if (!(rawMessage is SocketUserMessage message)) return;
		//	//if (message.Source != MessageSource.User) return;

		//	//var argPos = 0;

		//	//char prefix = Char.Parse(_config["Prefix"]);
		//	////if (!message.HasCharPrefix('!', ref argPos)) return;
		//	////if (!message.HasMentionPrefix(_client.CurrentUser, ref argPos)) return;
		//	//if (!(message.HasMentionPrefix(_client.CurrentUser, ref argPos) || message.HasCharPrefix(prefix, ref argPos)))
		//	//{
		//	//	return;
		//	//}

		//	//var context = new SocketCommandContext(_client, message);

		//	//await _commands.ExecuteAsync(context, argPos, _services);


		//}

		//public async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
		//{
		//	if (!command.IsSpecified)
		//	{
		//		System.Console.WriteLine($"Command failed to execute for [{context.User.Username}] <-> [{result.ErrorReason}]!");
		//		return;
		//	}

		//	if (result.IsSuccess)
		//	{
		//		System.Console.WriteLine($"Command [{command.Value.Name}] executed for -> [{context.User.Username}]");
		//		return;
		//	}

		//	await context.Channel.SendMessageAsync($"Sorry, {context.User.Username}... something went wrong -> [{result}]!");
		//}

		//private async Task LogCommandUsage(SocketCommandContext context, IResult result)
		//{
		//	await Task.Run(async () =>
		//	{
		//		if (context.Channel is IGuildChannel)
		//		{
		//			var logTxt = $"User: [{context.User.Username}]<->[{context.User.Id}] Discord Server: [{context.Guild.Name}] -> [{context.Message.Content}]";
		//			//_logger.LogInformation(logTxt);
		//		}
		//		else
		//		{
		//			var logTxt = $"User: [{context.User.Username}]<->[{context.User.Id}] -> [{context.Message.Content}]";
		//			//_logger.LogInformation(logTxt);
		//		}
		//	});

		//	/*
  //          string commandIssued = string.Empty;
  //          if (!result.IsSuccess)
  //          {
  //              request.Success = false;
  //              request.FailureReason = result.ErrorReason;
  //          }
  //                    request.ChannelId = (long)context.Channel.Id;
  //          request.ChannelName = context.Channel.Name;
  //          request.UserId = (long)context.User.Id;
  //          request.Command = context.Message.Content;
  //          request.UserName = context.User.Username;
  //          request.Success = true;
  //          request.RequestTime = DateTime.Now;
  //          using (var db = new NinjaBotEntities())
  //          {
                
  //              db.Requests.Add(request);
  //              await db.SaveChangesAsync();
  //          }
  //           */
		//}
	}
}

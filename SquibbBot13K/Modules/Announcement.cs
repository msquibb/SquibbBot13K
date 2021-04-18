//using Discord;
//using Discord.Commands;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SquibbBot13K.Modules
{
	//[Group("announce")]
	public class Announcement : BaseCommandModule
	{

		[Command("ping")]
		[Aliases("pong", "hello")]
		public async Task PingAsync(CommandContext ctx)
		{
			//await ReplyAsync("polo!");
			//return Task.CompletedTask;
			await ctx.TriggerTypingAsync();
			await Task.Delay(2000);
			await ctx.RespondAsync("Polo!");
        }
		

		//[Command("say")]
		//[Summary("Echo test")]
		//public async Task SayAsync([Remainder][Summary("The text to echo")] string text) => await ReplyAsync(text);
		
	}
}

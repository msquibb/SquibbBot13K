using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SquibbBot13K.Modules
{
	//[Group("announce")]
	public class Announcement : ModuleBase
	{

		[Command("ping")]
		[Alias("pong", "hello")]
		public async Task PingAsync()
		{
			await ReplyAsync("polo!");
			//return Task.CompletedTask;
		}
		

		//[Command("say")]
		//[Summary("Echo test")]
		//public async Task SayAsync([Remainder][Summary("The text to echo")] string text) => await ReplyAsync(text);
		
	}
}

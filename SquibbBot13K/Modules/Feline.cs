//using Discord.Commands;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquibbBot13K.Modules
{
	public class Feline : BaseCommandModule //: ModuleBase
	{
		//[Command("feline")]
		//[Alias("tina")]
		[Command("cat")]
		[Aliases("")]
		public async Task HasBeenFed(CommandContext ctx)
		{
			var PetName = "Tina";
			await ctx.RespondAsync($"{PetName} has been fed. Don't fall for her bullshit!");
						//await ReplyAsync($"@Here {PetName} has been fed. Don't fall for her bullshit!");
		}
	}
}

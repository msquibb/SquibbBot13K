//using Discord.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
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
		[Aliases("Tina")]
		[RequireBotPermissions(Permissions.MentionEveryone)]
		public async Task HasBeenFed(CommandContext ctx)
		{
			var PetName = "Tina";
			var everyone = ctx.Guild.EveryoneRole;
			//var everyone = ctx.Member;
			//var role = ctx.Guild.Roles.First(w => w.Value.Name == "@everyone").Value;
			//await ctx.RespondAsync($"{PetName} has been fed. Don't fall for her bullshit!");
			var message = await new DiscordMessageBuilder()		
				.WithContent($"@everyone - {PetName} has been fed. Don't fall for her bullshit!")
				.WithAllowedMentions(new IMention[] { RoleMention.All, EveryoneMention.All })
				.SendAsync(ctx.Channel);
			//await ctx.RespondAsync($" {PetName} has been fed. Don't fall for her bullshit!");
		}

		[Command("feed")]
		public async Task DidAnyoneFeed(CommandContext ctx)
		{
			var message = await ctx.RespondAsync("Did anyone feed Tina this evening?");
			var yesEmoji = DiscordEmoji.FromName(ctx.Client, ":+1:");
			var noEmoji = DiscordEmoji.FromName(ctx.Client, ":poo:");

			await message.CreateReactionAsync(yesEmoji);
			await message.CreateReactionAsync(noEmoji);
			var reactons = await message.CollectReactionsAsync();


		}
	}
}

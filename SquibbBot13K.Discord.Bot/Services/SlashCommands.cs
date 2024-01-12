using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace SquibbBot13K.Discord.Bot.Services;

public class SlashCommands : ApplicationCommandModule
{

   internal ILogger<SlashCommands> _logger { private get; set; }


   [SlashCommand("ping", "Replies with pong!")]
   public async Task Ping(InteractionContext ctx)
   {
      await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Pong!"));
   }
}

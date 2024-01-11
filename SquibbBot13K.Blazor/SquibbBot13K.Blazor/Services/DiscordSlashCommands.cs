using DSharpPlus.Entities;
using DSharpPlus;
using DSharpPlus.SlashCommands;

namespace SquibbBot13K.Blazor.Services;

public class DiscordSlashCommands : ApplicationCommandModule
{

    public ILogger<DiscordSlashCommands> _logger { get; set; }

    [SlashCommand("snowmergency", "Current Snow Emergency Levels Outside")]
    public async Task SnowEmergency(InteractionContext ctx)
    {
        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                       .WithContent("Snow Emergency Test!"))
            .ConfigureAwait(false);
    }   
}

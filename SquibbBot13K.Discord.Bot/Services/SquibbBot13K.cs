using DSharpPlus;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.Options;

namespace SquibbBot13K.Discord.Bot.Services;

public class SquibbBot13K : BackgroundService
{

   private readonly ILogger<SquibbBot13K> _logger;
   private readonly DiscordClient _discord;
   private readonly Models.Options.Discord _discordOptions;
   private readonly ILoggerFactory _logggerFactory;
   private readonly IServiceProvider _serviceProvider;
   private readonly IConfiguration _config;

   public SquibbBot13K(ILogger<SquibbBot13K> logger,
                       IOptions<Models.Options.Discord> discordOptions,
                       ILoggerFactory logggerFactory,
                       IServiceProvider serviceProvider,
                       IConfiguration config)
   {
      _logger = logger;
      _config = config;
      _discordOptions = discordOptions.Value;
      _logggerFactory = logggerFactory;
      _serviceProvider = serviceProvider;

      _discord = new DiscordClient(new DiscordConfiguration()
      {
         Token = _discordOptions.Token,
         TokenType = TokenType.Bot,
         Intents = DiscordIntents.AllUnprivileged,
         LoggerFactory = _logggerFactory
      });
   }

   protected override async Task ExecuteAsync(CancellationToken stoppingToken)
   {
      _logger.LogInformation("SquibbBot13K is starting...");
      await _discord.ConnectAsync();
      var slash = _discord.UseSlashCommands(new SlashCommandsConfiguration
      {
         Services = _serviceProvider
      });
      _logger.LogInformation("Registering slash commands.");
      slash.RegisterCommands<SlashCommands>();

      int timer = 0;
      while (!stoppingToken.IsCancellationRequested)
      {
         if (timer == 60)
         {
            _logger.LogInformation("Discord worker running at {time}", DateTimeOffset.Now);
            timer = 0;
         }
         else
         {
            timer++;
         }
         await Task.Delay(1000, stoppingToken);
      }
   }



   public override async Task StopAsync(CancellationToken cancellationToken)
   {
      _logger.LogInformation("SquibbBot13K is shutting down...");
      await base.StopAsync(cancellationToken);
   }

}

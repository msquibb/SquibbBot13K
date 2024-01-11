
using DSharpPlus;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.Options;

namespace SquibbBot13K.Blazor.Services;

public class Discord : BackgroundService
{
    private readonly Models.Options.Discord _discordOptions;
    private readonly ILogger<Discord> _logger;
    private readonly ILoggerFactory _loggerFactory;
    private readonly IServiceProvider _serviceProvider;

    private readonly DiscordClient _discordClient;

    public Discord(
        ILogger<Discord> logger,
        ILoggerFactory loggerFactory,
        IServiceProvider serviceProvider,
        IOptions<Models.Options.Discord> discordOptions)
    {
        _logger = logger;
        _loggerFactory = loggerFactory;
        _serviceProvider = serviceProvider;
        _discordOptions = discordOptions.Value;

        _discordClient = new DiscordClient(new DiscordConfiguration()
        {
            Token = _discordOptions.Token,
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.AllUnprivileged,
            LoggerFactory = _loggerFactory
        });

        _serviceProvider = serviceProvider;

        _discordClient.GuildDownloadCompleted += async (sender, e) =>
        {
            _logger.LogInformation($"Guild Download Completed: {e.Guilds.Count} guilds");
            e.Guilds.Values.ToList().ForEach(async guild =>
            {
                await guild.GetDefaultChannel().SendMessageAsync("SquibbBot13K is online!");
            });
        };
        
    }



    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var slash = _discordClient.UseSlashCommands(new SlashCommandsConfiguration
        {
            Services = _serviceProvider
        });
        slash.RegisterCommands<DiscordSlashCommands>();
        await _discordClient.ConnectAsync();
        var stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        _logger.LogInformation($"Discord Client Connected in {stopwatch.ElapsedMilliseconds}ms");
        var elapsedCounter = new TimeSpan();
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
            if (stopwatch.ElapsedMilliseconds > 600000)
            {
                elapsedCounter += stopwatch.Elapsed;
                stopwatch.Restart();
                _logger.LogInformation($"Discord Client has been connected for {elapsedCounter.TotalMinutes} minutes");
            }
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        foreach(var guild in _discordClient.Guilds)
        {
            await guild.Value.GetDefaultChannel().SendMessageAsync("SquibbBot13K is shutting down, goodbye!");
        }
        _logger.LogWarning("Application stop requested, shutting down Discord Client");
        _logger.LogInformation("Discord Client Disconnecting...");
        await _discordClient.DisconnectAsync();
        await base.StopAsync(cancellationToken);
    }
}

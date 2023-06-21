namespace SquibbBot13K.Discord.Bot.Services;

public class StartupService
{
	private readonly ILogger<StartupService> _logger;
	private readonly IConfiguration _config;

	public StartupService(ILogger<StartupService> logger, IConfiguration config)
	{
		_logger = logger;
		_config = config;
	}

	public async Task Run()
	{
		_logger.LogInformation("Starting SquibbBot13K... gurd yer loins!");
	}
}

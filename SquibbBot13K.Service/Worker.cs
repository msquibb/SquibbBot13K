namespace SquibbBot13K.Service
{
	public class BotServiceWorker : BackgroundService
	{
		private readonly ILogger<BotServiceWorker> _logger;

		public BotServiceWorker(ILogger<BotServiceWorker> logger)
		{
			_logger = logger;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				_logger.LogInformation("SquibbBot13K Service Worker running at: {time}", DateTimeOffset.Now);
				await Task.Delay(1000, stoppingToken);
			}
		}
	}
}
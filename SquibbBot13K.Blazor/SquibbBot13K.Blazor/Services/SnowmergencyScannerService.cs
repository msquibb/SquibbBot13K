using AngleSharp;
using AngleSharp.Dom;


namespace SquibbBot13K.Blazor.Services;

public class SnowmergencyScannerService : BackgroundService
{
	private readonly ILogger<SnowmergencyScannerService> _logger;
	private readonly IBotEventService _botEventService;
	private readonly Microsoft.Extensions.Configuration.IConfiguration _config;

	private string _snowmergencyLevelText = string.Empty;
	private readonly string scanUrl;

	public SnowmergencyScannerService(
		ILogger<SnowmergencyScannerService> logger,
		IBotEventService botEventService,
		Microsoft.Extensions.Configuration.IConfiguration config)
	{
		scanUrl = config["Snowmergency:ScanUrl"] ?? string.Empty;

		_logger = logger;
		_botEventService = botEventService;
		_config = config;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		var startupDelay = TimeSpan.FromSeconds(10);
		_logger.LogInformation("Snowmergency service starting in {count} seconds.", startupDelay.TotalSeconds);
		await Task.Delay(startupDelay, stoppingToken);
		_logger.LogInformation("Snowmergency scan service ready!");
		_botEventService.ScanForSnowmergency += _botEventService_InteractiveScanForSnowmergency;
		await SnowmerencyScan();
	}

	private async void _botEventService_InteractiveScanForSnowmergency(object sender, ScanForSnowmergencyEventArgs args)
	{
		var config = Configuration.Default.WithDefaultLoader();
		var document = await BrowsingContext.New(config).OpenAsync(scanUrl);
		var snowmergencySpanText = document.QuerySelectorAll(".breadcrumb-classic").FirstOrDefault()?.QuerySelector(".emergency-levels-sm")?.TextContent ?? "No snow emergency found";

		_logger.LogInformation("Interactive scan for snowmergency requested by {user}", args.RequestingUser);
		_logger.LogInformation("Scan complete. returned: {level}", snowmergencySpanText);
	}

	private async Task<bool> SnowmerencyScan()
	{
		var url = _config["Snowmergency:ScanUrl"] ?? string.Empty;
		var config = Configuration.Default.WithDefaultLoader();
		IDocument document;
		string snowmergencySpanText;
		var didChange = false;

		try
		{
			document = await BrowsingContext.New(config).OpenAsync(url);
			snowmergencySpanText = document.QuerySelectorAll(".breadcrumb-classic").FirstOrDefault()?.QuerySelector(".emergency-levels-sm")?.TextContent ?? "0";
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "This fucking thing...");
			return false;
		}


		if (snowmergencySpanText != _snowmergencyLevelText && int.TryParse(snowmergencySpanText, out var snowmergencyLevel))
		{
			_logger.LogInformation("Snowmergency level changed to {level}", snowmergencyLevel);
			var args = new SnowEmergencyChangeEventArgs { CurrentEmergencyLevel = snowmergencyLevel, AffectedCounty = County.Muskingum };
			_botEventService.UpdateSnowEmergencyLevel(this, args);
			_snowmergencyLevelText = snowmergencySpanText;
			didChange = true;
		}

		return didChange;
	}


	public override async Task StopAsync(CancellationToken cancellationToken)
	{
		_botEventService.ScanForSnowmergency -= _botEventService_InteractiveScanForSnowmergency;

		await base.StopAsync(cancellationToken);
	}

	private class ScrapedSnowmergency
	{
		public string DateColumn { get; set; }
		public string Level { get; set; }
		public string Start { get; set; }
		public string End { get; set; }
	}
}

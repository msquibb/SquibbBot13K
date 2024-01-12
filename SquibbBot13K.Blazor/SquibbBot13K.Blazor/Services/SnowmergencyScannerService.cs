using AngleSharp;
using AngleSharp.Dom;


namespace SquibbBot13K.Blazor.Services;

public class SnowmergencyScannerService : BackgroundService
{
   private readonly ILogger<SnowmergencyScannerService> _logger;
   private readonly IBotEventService _botEventService;
   private readonly Microsoft.Extensions.Configuration.IConfiguration _config;


   public SnowmergencyScannerService(
      ILogger<SnowmergencyScannerService> logger,
      IBotEventService botEventService,
      Microsoft.Extensions.Configuration.IConfiguration config)
   {
      _logger = logger;
      _botEventService = botEventService;
      _config = config;
   }

   protected override Task ExecuteAsync(CancellationToken stoppingToken)
   {
      
   }

   private async Task SnowmerencyScan()
   {
      var url = _config["Snowmergency:ScanUrl"] ?? string.Empty;
      var config = Configuration.Default.WithDefaultLoader();
      var document = await BrowsingContext.New(config).OpenAsync(url);
     
      var table = document.QuerySelector("table.js-datatable tr");
     if (table != null)
      {
         var rows = table.QuerySelectorAll("tr:nth-child(2)");
         if (rows != null)
         {
            foreach (var row in rows.Take(2))
            {

            }
         }
      }

   }


   public override async Task StopAsync(CancellationToken cancellationToken)
   {
      

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

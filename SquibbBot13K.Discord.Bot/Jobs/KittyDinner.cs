using Quartz;

namespace SquibbBot13K.Discord.Bot.Jobs;

internal class KittyDinner : IJob
{
   private readonly ILogger<KittyDinner> _logger;

   public KittyDinner(ILogger<KittyDinner> logger)
   {
      _logger = logger;
   }

   public async Task Execute(IJobExecutionContext context)
   {
      _logger.LogInformation("Sending kitty dinner notification");
      await Task.Delay(0);
      _logger.LogInformation("This job will run next time at {time}", context.NextFireTimeUtc!.Value.ToLocalTime().ToString("F"));
   }
}

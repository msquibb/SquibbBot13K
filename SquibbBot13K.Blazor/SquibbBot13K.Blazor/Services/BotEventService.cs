namespace SquibbBot13K.Blazor.Services;

public interface IBotEventService
{
   //public delegate void SnowEmergencyChangeEvent(int level, County county);
   //public delegate void ScanForSnowmergencyEvent();

   event Action<object, SnowEmergencyChangeEventArgs> SnowEmergencyChange;
   event Action<object, ScanForSnowmergencyEventArgs> ScanForSnowmergency;

   public void InvokeTestEvent(object sender, SnowEmergencyChangeEventArgs args);
   void RequestSnowmergencyScan(object sender, ScanForSnowmergencyEventArgs args);
   void UpdateSnowEmergencyLevel(object sender, SnowEmergencyChangeEventArgs args);
}

public class BotEventService : IBotEventService
{
   private readonly ILogger<BotEventService> _logger;

   public event Action<object, SnowEmergencyChangeEventArgs> SnowEmergencyChange;

	public event Action<object, ScanForSnowmergencyEventArgs> ScanForSnowmergency;

   public BotEventService(ILogger<BotEventService> logger)
   {
      _logger = logger;
   }

	public void InvokeTestEvent(object? sender, SnowEmergencyChangeEventArgs? args)
   {
      SnowEmergencyChange.Invoke(this, new SnowEmergencyChangeEventArgs { CurrentEmergencyLevel = 10, AffectedCounty = County.Muskingum });
      _logger.LogDebug("Event from {from}", nameof(BotEventService));
   }

   public void UpdateSnowEmergencyLevel(object sender, SnowEmergencyChangeEventArgs args)
   {
      SnowEmergencyChange.Invoke(sender, args);
      _logger.LogDebug("Event from {from}", nameof(BotEventService));
   }

   public void RequestSnowmergencyScan(object sender, ScanForSnowmergencyEventArgs args)
   {
      
      ScanForSnowmergency.Invoke(sender, args);

   }

}

public class SnowEmergencyChangeEventArgs : EventArgs
{
    public int CurrentEmergencyLevel { get; set; }
    public County AffectedCounty { get; set; }
}

public class ScanForSnowmergencyEventArgs : EventArgs
{
    public string RequestingUser { get; set; } 
}

public enum County
{
   Muskingum = 0
}

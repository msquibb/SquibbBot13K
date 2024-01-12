namespace SquibbBot13K.Blazor.Services;

public interface IBotEventService
{
    public delegate void SnowEmergencyChangeEvent(int level, County county);

    public event SnowEmergencyChangeEvent SnowEmergencyChange;
    public void InvokeEvent();
}

public class BotEventService : IBotEventService
{
    private readonly ILogger<BotEventService> _logger;

    public event IBotEventService.SnowEmergencyChangeEvent SnowEmergencyChange;

    public BotEventService(ILogger<BotEventService> logger)
    {
        _logger = logger;
    }

    public void InvokeEvent()
    {
        SnowEmergencyChange.Invoke(1, County.Muskingum);
        _logger.LogDebug("Event from {from}", nameof(BotEventService));
    }
}

public enum County
{
    Muskingum = 0
}

using DSharpPlus;
using Microsoft.Extensions.Hosting;

namespace SquibbBot13K.Discord.Bot;

public class SquibbBot13K :IHostedService, IDisposable
{

	private readonly ILogger<SquibbBot13K> _logger;
	private bool disposedValue;

	public SquibbBot13K(ILogger<SquibbBot13K> logger)
	{
		_logger = logger;
	}


	public Task StartAsync(CancellationToken cancellationToken)
	{
		_logger.LogInformation("SquibbBot13K is starting...");

		return Task.CompletedTask;
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		_logger.LogInformation("SquibbBot13K is shutting down...");
		return Task.CompletedTask;
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			if (disposing)
			{
				// TODO: dispose managed state (managed objects)
			}

			// TODO: free unmanaged resources (unmanaged objects) and override finalizer
			// TODO: set large fields to null
			disposedValue = true;
		}
	}

	// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
	// ~SquibbBot13K()
	// {
	//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
	//     Dispose(disposing: false);
	// }

	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

}

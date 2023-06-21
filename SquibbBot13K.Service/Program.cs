using SquibbBot13K.Service;

IHost host = Host.CreateDefaultBuilder(args)
		.ConfigureServices(services =>
		{
			services.AddHostedService<BotServiceWorker>();
		})
		.Build();

await host.RunAsync();

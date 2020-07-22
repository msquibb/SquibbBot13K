using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using SquibbBot13K.Modules.Admin;
using SquibbBot13K.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SquibbBot13K
{
    public class SquibbBot13K
    {
        private DiscordShardedClient _client;
        //private DiscordSocketClient _client;
        private IConfigurationRoot _config;
        private string _environment;

        public SquibbBot13K()
        {

        }

        public async Task StartAsync()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty;
            _environment = environmentName;

            var _builder = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: true)
                    .AddUserSecrets("0522ca63-cbdc-4658-922d-14d332b29535")
                    .AddEnvironmentVariables();

            _config = _builder.Build();
            var secureSettings = _config.GetSection(nameof(SquibbBot13KSecrets)).Get<SquibbBot13KSecrets>();

            var services = new ServiceCollection()
            .AddSingleton(new DiscordShardedClient(new DiscordSocketConfig
            {
                //LogLevel = LogSeverity.Debug,
                LogLevel = LogSeverity.Verbose,
                MessageCacheSize = 1000
            }))
                .AddSingleton(_config)
                .AddSingleton(secureSettings)
                .AddSingleton(new CommandService(new CommandServiceConfig
                {
                    DefaultRunMode = RunMode.Async,
                    LogLevel = LogSeverity.Verbose,
                    CaseSensitiveCommands = false,
                    ThrowOnError = false
                }))
            .AddHttpClient()
            .AddSingleton<DiscordSocketClient>()
            .AddSingleton<CommandHandler>()
            .AddSingleton<StartupService>()
            .AddSingleton<UserInteraction>()
            .AddSingleton<LoggingService>();

            ConfigureServices(services);
            
            //Build services
            var serviceProvider = services.BuildServiceProvider();

            //Instantiate logger/tie-in logging
            serviceProvider.GetRequiredService<LoggingService>();

            //Start the bot
            await serviceProvider.GetRequiredService<StartupService>().StartAsync();

            //Load up services
            serviceProvider.GetRequiredService<CommandHandler>();
            serviceProvider.GetRequiredService<UserInteraction>();
            _client = serviceProvider.GetRequiredService<DiscordShardedClient>();

            _client.Log += LogAsync;
            _client.ShardReady += _client_ShardReady;
            //_client.Ready += ReadyAsync;
            serviceProvider.GetRequiredService<CommandService>().Log += LogAsync;

            await serviceProvider.GetRequiredService<CommandHandler>().InitializeAsync();

            //Block this program until it is closed.
            await Task.Delay(-1);
        }

        private Task _client_ShardReady(DiscordSocketClient arg)
        {
            Console.WriteLine($"Connected as -> [{arg.CurrentUser}] =)");
            return Task.CompletedTask;
        }

        private Task ReadyAsync()
        {
            Console.WriteLine($"Connected as -> [{_client.CurrentUser}] :)");
            return Task.CompletedTask;
        }

        private Task LogAsync(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private void ConfigureServices(IServiceCollection services)
        {
            
        }
    }
}

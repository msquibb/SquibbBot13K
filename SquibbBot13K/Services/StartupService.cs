using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SquibbBot13K.Services
{
    public class StartupService
    {
        private readonly DiscordShardedClient _discord;
        private readonly CommandService _commands;
        private readonly IConfigurationRoot _config;
        private readonly IServiceProvider _services;
        private readonly SquibbBot13KSecrets _secrets;

        public StartupService(IServiceProvider services)
        {
            _services = services;
            _config = _services.GetRequiredService<IConfigurationRoot>();
            _discord = _services.GetRequiredService<DiscordShardedClient>();
            _commands = _services.GetRequiredService<CommandService>();
            _secrets = _services.GetRequiredService<SquibbBot13KSecrets>();
        }

        public async Task StartAsync()
        {
            string discordToken = _secrets.Token;
            //string discordToken = _config["Token"];
            if (string.IsNullOrWhiteSpace(discordToken))
            {
                throw new Exception("Token missing from config.json! Please enter your token there (root directory)");
            }

            await _discord.LoginAsync(TokenType.Bot, discordToken);
            await _discord.StartAsync();
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }
    }
}

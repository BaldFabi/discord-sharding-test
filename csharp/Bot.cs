using ShardingBot.Handlers;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace ShardingBot
{
    public class Bot
    {
        private DiscordShardedClient _botClient;
        private CommandHandler? _commandHandler;

        public Bot()
        {
            _botClient = new DiscordShardedClient();
        }

        public async Task StartBot()
        {
            await _botClient.LoginAsync(TokenType.Bot, "MY_BOT_TOKEN");
            await _botClient.StartAsync();

            _commandHandler = new CommandHandler(_botClient, new CommandService());
            await _commandHandler.InitializeCommands();

            await Task.Delay(-1);
        }
    }
}
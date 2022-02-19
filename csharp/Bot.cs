using ShardingBot.Handlers;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace ShardingBot
{
    public class Bot
    {
        #region Fields

        private DiscordShardedClient _botClient;
        private CommandHandler? _commandHandler;

        #endregion

        #region Constructors

        public Bot()
        {
            _botClient = new DiscordShardedClient();
        }

        #endregion

        #region Methods

        public async Task StartBot()
        {
            RegisterClientEvents();

            await _botClient.LoginAsync(TokenType.Bot, "MY_BOT_TOKEN");
            await _botClient.StartAsync();

            _commandHandler = new CommandHandler(_botClient, new CommandService());
            await _commandHandler.InitializeCommands();

            await Task.Delay(-1);
        }

        private void RegisterClientEvents()
        {
            _botClient.ShardReady += OnShardReady;
            _botClient.ShardConnected += OnShardConnected;
            _botClient.Log += OnClientLog;
        }


        #endregion

        #region Events

        private Task OnShardReady(DiscordSocketClient arg)
        {
            throw new NotImplementedException();
        }

        private Task OnShardConnected(DiscordSocketClient arg)
        {
            throw new NotImplementedException();
        }

        private Task OnClientLog(LogMessage arg)
        {
            Console.WriteLine(arg.ToString());
            return Task.CompletedTask;
        }

        #endregion
    }
}